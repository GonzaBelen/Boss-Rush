using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private PlayerControls playerControls;
    [SerializeField] private GameObject weapon;
    private WeaponParent weaponParent;
    private InputSystemHelper inputSystemHelper;
    private float gravity;
    public bool knockbackCheck = false;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector3 boxDimensions;
    [SerializeField] private bool isGrounded;

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private float movementHor = 0;
    private bool canMove = true;
    private bool isMoving = false;

    [Header("Dash")]    
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashForce;
    [SerializeField] private float coolDown;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash = true;

    [Header("Parry")]
    [SerializeField] private float parryCoolDown;
    private bool isParry = false;
    private bool canParry = true;
    private float parryTime = 0.5f;

    [Header("Weapon")]
    private Vector2 pointerPosition;
    private Vector2 pointerContext;
    private Vector2 positionTransform;
    [SerializeField] private Transform playerTransform;

    [Header("Vigor")]
    private Vigor vigor;
    [SerializeField] private float jumpCost;
    [SerializeField] private float dashCost;
    [SerializeField] private float attackCost;

    [Header("Health")]
    private Health health;
    private bool inmunity = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        health = GetComponent<Health>();
        inputSystemHelper = GetComponent<InputSystemHelper>();
        vigor = GetComponent<Vigor>();
        playerControls = new PlayerControls();
        gravity = rb2D.gravityScale;
        canDash = true;
    }

    private void Update()
    {
        if (knockbackCheck)
        {
            Debug.Log("Knockback");
            animator.SetTrigger("Damage");
            // StartCoroutine(Knockback());
            return;   
        }
        
        if (isParry)
        {
            animator.SetTrigger("Parry");
        }
        
        if (inmunity)
        {
            health.canHurt = false;
        }
        else
        {
            health.canHurt = true;
        }

        if (isDashing || isParry)
        {
            return;
        }

        pointerPosition = GetPointerInput();
        weaponParent.PointerPosition = pointerPosition;
        
        if (isGrounded)
        {
            canMove = true;
        } else
        {
            canMove = false;
            isMoving = false;
        }

        if (canMove)
        {
            rb2D.velocity = new Vector2(movementHor * movementSpeed, rb2D.velocity.y);
        }

        if (movementHor == 0)
        {
            isMoving = false;
        } else if (isGrounded)
        {
            isMoving = true;
        }

        if (isMoving && isGrounded)
        {
            animator.SetTrigger("Run");
        } else 
        if (!isMoving && isGrounded)
        {
            animator.SetTrigger("Idle");
        }
        else if (!isMoving && !isGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }

    private void FixedUpdate()
    {
        if (isDashing || isParry)
        {
            return;
        }
        isGrounded = Physics2D.OverlapBox(groundController.position, boxDimensions, 0f, whatIsGround);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            vigor.VigorController(15);
            if (vigor.canMakeIt)
            {
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementHor = context.ReadValue<Vector2>().x;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && canDash && !isParry)
        {
            vigor.VigorController(20);
            if (vigor.canMakeIt)
            {
                StartCoroutine(Dash());
            }            
        }
    }

    public void Parry(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && canMove && canParry)
        {
            isParry = true;
            StartCoroutine (ParryCoroutine());
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && !weaponParent.attackBlocked && canMove && !isParry)
        {
            vigor.VigorController(10);
            if (vigor.canMakeIt)
            {
                weaponParent.Attack();
            } 
        }
    }

    public void UpdatePointerContext(InputAction.CallbackContext context)
    {
        pointerContext = context.ReadValue<Vector2>();
    }

    
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerContext;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void Flip (Vector2 lookDirection)
    {
        Vector3 scale = transform.localScale;
        if (lookDirection.x > 0.5f)
        {
            scale.x = 3;
        }
        else if (lookDirection.x < -0.5f)
        {
            scale.x = -3;
        }
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundController.position, boxDimensions);
    }

    public IEnumerator Dash()
    {
        isDashing = true;
        inmunity = true;
        canDash = false;
        canMove = false;
        gravity = 0f;
        if (movementHor > 0)
        {
            if (transform.localScale.x > 0)
            {
                rb2D.velocity = new Vector2(transform.localScale.x * dashForce, 0f); 
            }
            else 
            {
                rb2D.velocity = new Vector2(transform.localScale.x * dashForce * -1, 0f); 
            }
        }
        else if (movementHor < 0)
        {
            if (transform.localScale.x > 0)
            {
                rb2D.velocity = new Vector2(transform.localScale.x * dashForce * -1, 0f); 
            }
            else 
            {
                rb2D.velocity = new Vector2(transform.localScale.x * dashForce, 0f); 
            }
        }
        else
        {
            rb2D.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        }
        yield return new WaitForSeconds(dashingTime);
        rb2D.gravityScale = 1.75f;
        isDashing = false;
        inmunity = false;
        yield return new WaitForSeconds(coolDown);
        canDash = true;
    }

    private IEnumerator ParryCoroutine()
    {
        canParry = false;
        yield return new WaitForSeconds(parryTime);
        isParry = false;
        yield return new WaitForSeconds(parryCoolDown);
        canParry = true;
        isParry = false;
    }

    public void Death()
    {
        animator.SetTrigger("Death");
        weapon.SetActive(false);
        inputSystemHelper.enabled = true;
    }
}

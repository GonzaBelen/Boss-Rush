using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;
public class SlimeController : MonoBehaviour
{
    private Animator animator;
    private Jump jump;
    private Knockback knockback;
    private Attack attack;
    private Health health;
    private CooldownController CooldownController;
    private ParryController parryController;
    private PolygonCollider2D polygonCollider2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject player;
    [SerializeField] private float movementSpeed;
    [SerializeField] public float distance;
    [SerializeField] private float collisionDamage;
    private float halfHealth;
    public bool canMove;
    public bool waitToMove = false;
    public bool isAlreadyInSecondFase = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        jump = GetComponent<Jump>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        knockback = GetComponent<Knockback>();
        attack = GetComponentInChildren<Attack>();
        health = GetComponent<Health>();
        parryController = GetComponent<ParryController>();;
        CooldownController = GetComponent<CooldownController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        halfHealth = health.maxHealth * 0.5f;
    }
    
    private void Update()
    {
        if (attack.isAtacking)
        {
            return;
        }

        if (health.currentHealth < halfHealth && !isAlreadyInSecondFase)
        {
           StartCoroutine(SecondFaseCoroutine());
        }

        if (canMove)
        {
            Movement();
        }

        if (waitToMove || !jump.isGrounded)
        {
            canMove = false;
        } else if (!waitToMove && jump.isGrounded)
        {
            canMove = true;
        }

        if (canMove && jump.isGrounded)
        {
            animator.SetTrigger("Move");
        } else if (!canMove && jump.isGrounded)
        {
            animator.SetTrigger("Idle");
        } else if (!jump.isGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }

    private void Movement()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        Flip(direction);   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                if (parryController.isInWeakPoint)
                {
                    return;
                }
                health.GetHit(collisionDamage, gameObject);
                knockback.PlayFeedback(gameObject);
            }
        }
    }

    private void Flip (Vector2 lookDirection)
    {
        Vector3 scale = transform.localScale;
        if (lookDirection.x < 0.5f)
        {
            scale.x = 7;
        }
        else if (lookDirection.x > -0.5f)
        {
            scale.x = -7;
        }
        transform.localScale = scale;
    }

    private IEnumerator SecondFaseCoroutine()
    {
        isAlreadyInSecondFase = true;
        SecondFase();
        yield return new WaitForSeconds(0);
    }

    private void SecondFase()
    {
        health.armor *= 1.5f;
        movementSpeed *= 1.5f;
        collisionDamage *= 1.5f;
        CooldownController.delayBetweenAttaks *= 0.5f;
        CooldownController.attackCooldown *= 0.5f;
        CooldownController.jumpCooldown *= 0.5f;
        attack.damage *= 1.5f;
        spriteRenderer.material.SetColor("_Color", Color.red);;
        CooldownController.StartMiniSlimeCoroutine();
    }

    public void CanMove()
    {
        canMove = true;
    }

    public void CantMove()
    {
        canMove = false;
    }
}
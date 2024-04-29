using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jump : MonoBehaviour
{
    private CooldownController cooldownController;
    private SlimeController slimeController;
    private Health health;
    private ParryController parryController;
    private Rigidbody2D rb2D;
    private Animator animator;
    [SerializeField] private float jumpForceVertical;
    private float jumpForceHorizontal;
    [SerializeField] private float jumpForceHorizontalPositive;
    private float jumpForceHorizontalNegative;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector3 boxDimensions;
    public bool isGrounded;
    public UnityEvent OnEndJump;
    public bool stopJumping;

    private void Start()
    {
        cooldownController = GetComponent<CooldownController>();
        slimeController = GetComponent<SlimeController>();
        health = GetComponent<Health>();
        parryController = GetComponent<ParryController>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpForceHorizontalNegative = jumpForceHorizontalPositive * -1;
        stopJumping = false;
    }

    private void Update()
    {
        if (parryController.isInWeakPoint)
        {
            return;
        }

        if (health.isDead)
        {
            stopJumping = true;
        }

        if (transform.localScale.x < 0)
        {
            jumpForceHorizontal = jumpForceHorizontalPositive;
        } else
        {
            jumpForceHorizontal = jumpForceHorizontalNegative;
        }
    }
    
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundController.position, boxDimensions, 0f, whatIsGround);
    }
    
    public void JumpAction()
    {
        if (stopJumping || parryController.isInWeakPoint)
        {
            return;
        }
        StartCoroutine(TimeToMove());
        rb2D.AddForce(transform.up * jumpForceVertical, ForceMode2D.Impulse);
        rb2D.AddForce(transform.right * jumpForceHorizontal, ForceMode2D.Impulse);
        cooldownController.Randomize();
        StartCoroutine(cooldownController.JumpCooldown());
    }

    private IEnumerator TimeToMove()
    {
        slimeController.waitToMove = true;
        yield return new WaitForSeconds(3);
        OnEndJump?.Invoke();
        slimeController.waitToMove = false;
        cooldownController.stopJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundController.position, boxDimensions);
    }

    public void StopJump()
    {
        stopJumping = true;
    }

    public void StartJump()
    {
        stopJumping = false;
    }
}
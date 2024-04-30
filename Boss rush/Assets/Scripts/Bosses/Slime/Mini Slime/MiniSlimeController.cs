using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniSlimeController : MonoBehaviour
{
    private Animator animator;
    private Knockback knockback;
    [SerializeField] private GameObject player;
    [SerializeField] private float movementSpeed;
    [SerializeField] public float distance;
    [SerializeField] private float collisionDamage;
    private bool canMove = false;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector3 boxDimensions;
    private bool isGrounded;

    private void Start()
    {
        knockback = GetComponent<Knockback>();
        animator = GetComponent<Animator>();
        StartCoroutine(WaitToMove());
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Minion"), LayerMask.NameToLayer("Enemie"), true);
    }

    private void Update()
    {
        if (isGrounded && !canMove)
        {
            animator.SetTrigger("Idle");
        }
        
        if (canMove)
        {
            animator.SetTrigger("Move");
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;        
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
            Flip(direction);
        }        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundController.position, boxDimensions, 0f, whatIsGround);
    }

    private void Flip (Vector2 lookDirection)
    {
        Vector3 scale = transform.localScale;
        if (lookDirection.x < 0.5f)
        {
            scale.x = 2.5f;
        }
        else if (lookDirection.x > -0.5f)
        {
            scale.x = -2.5f;
        }
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.GetHit(collisionDamage, gameObject);
                knockback.PlayFeedback(gameObject);
            }
        }
    }

    private IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(2);
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundController.position, boxDimensions);
    }
}

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
    private PolygonCollider2D polygonCollider2D;
    [SerializeField] private GameObject player;
    [SerializeField] private float movementSpeed;
    [SerializeField] public float distance;
    [SerializeField] private float collisionDamage;
    public bool canMove;
    public bool waitToMove = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        jump = GetComponent<Jump>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        knockback = GetComponent<Knockback>();
        attack = GetComponentInChildren<Attack>();
    }
    
    private void Update()
    {
        if (attack.isAtacking)
        {
            return;
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
}
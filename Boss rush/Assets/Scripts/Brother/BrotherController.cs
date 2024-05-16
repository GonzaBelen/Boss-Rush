using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject movementBarrier;
    [SerializeField] private GameObject checkPoint;
    [SerializeField] private GameObject dialogueGenerator;
    [SerializeField] private float movementSpeed = 2;
    public bool canMove = false;
    public float distance;
    public bool canAttack = false;
    public bool isAttacking = false;
    public bool inParried = false;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Brother"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Barrier"), LayerMask.NameToLayer("Brother"), true);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
        {
            dialogueGenerator.SetActive(false);
            Movement();
            animator.SetTrigger("BrotherRun");
        } else if (!canMove && !isAttacking)
        {
            animator.SetTrigger("BrotherIdle");
        } else if (isAttacking && !inParried)
        {
            animator.SetTrigger("BrotherAttack");
        } else if (inParried)
        {
            animator.SetTrigger("BrotherParried");
        }
    }

    private void Movement()
    {
        distance = Vector2.Distance(transform.position, checkPoint.transform.position);
        Vector2 direction = checkPoint.transform.position - transform.position;        
        transform.position = Vector2.MoveTowards(transform.position, checkPoint.transform.position, movementSpeed * Time.deltaTime);
        Flip(direction);    
    }

    public void Flip (Vector2 lookDirection)
    {
        Vector3 scale = transform.localScale;
        if (lookDirection.x < 0.5f)
        {
            scale.x = -3;
        }
        else if (lookDirection.x > -0.5f)
        {
            scale.x = 3;
        }
        transform.localScale = scale;
    }

    public void IsInAttack()
    {
        isAttacking = true;
    }

    public void FinishAttack()
    {
        isAttacking = false;
    }

    public void FinishParried()
    {
        inParried = false;
    }
}

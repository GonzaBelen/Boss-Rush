using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeBrother : MonoBehaviour
{
    private Animator animator;
    private BrotherController brotherController;
    private CoolDownBrother coolDownBrother;
    [SerializeField] private CircleCollider2D circleCollider2D;

    private void Start()
    {
        brotherController = GetComponentInParent<BrotherController>();
        coolDownBrother = GetComponentInParent<CoolDownBrother>();
        animator = GetComponentInParent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.ResetTrigger("BrotherIdle");
            brotherController.isAttacking = true;
            circleCollider2D.enabled = false;
            coolDownBrother.Attack();
        }
    }

    public void TurnOnCollider()
    {
        circleCollider2D.enabled = true;
    }

    public void TurnOffCollider()
    {
        circleCollider2D.enabled = false;
    }
}

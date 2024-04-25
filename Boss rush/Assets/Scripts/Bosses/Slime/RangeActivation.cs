using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeActivation : MonoBehaviour
{
    private Animator animator;
    public Collider2D collider2D;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            collider2D.enabled = false;
        }
    }

    public void TurnOnCollider()
    {
        collider2D.enabled = true;
    }
}

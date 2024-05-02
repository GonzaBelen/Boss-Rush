using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RangeActivation : MonoBehaviour
{
    private Animator animator;
    private SlimeController slimeController;
    private SecondFaseController secondFaseController;
    public Collider2D collider2D;

    private void Start()
    {
        slimeController = GetComponentInParent<SlimeController>();
        animator = GetComponentInParent<Animator>();
        secondFaseController = GetComponentInParent<SecondFaseController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!slimeController.isAlreadyInSecondFase)
            {
                animator.SetTrigger("Attack");
            } else 
            {
                animator.SetTrigger("AttackAngry");
            }
            collider2D.enabled = false;
        }
    }

    public void TurnOnCollider()
    {
        collider2D.enabled = true;
    }

    public void TurnOffCollider()
    {
        collider2D.enabled = false;
    }
}

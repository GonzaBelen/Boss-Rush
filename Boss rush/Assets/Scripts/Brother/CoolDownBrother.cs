using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolDownBrother : MonoBehaviour
{
    private BrotherController brotherController;
    [SerializeField] public float attackCooldown;
    public float delayBetweenAttacks = 1;
    public UnityEvent OnBeginCooldownAttack;
    public UnityEvent OnDoneCooldownAttack;

    private void Start()
    {
        brotherController = GetComponentInParent<BrotherController>();

    }

    public void Attack()
    {
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        OnBeginCooldownAttack?.Invoke();
        yield return new WaitForSeconds(attackCooldown);
        brotherController.canAttack = true;
        OnDoneCooldownAttack?.Invoke();
    }
}

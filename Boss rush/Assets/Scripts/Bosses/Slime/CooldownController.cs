using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CooldownController : MonoBehaviour
{
    private ParryController parryController;
    
    [Header("Jump")]
    private Jump jump;
    [SerializeField] private float maxJumpCooldown;
    [SerializeField] private float minJumpCooldown;
    public float jumpCooldown;
    public bool canJump;
    public bool stopJump = false;

    [Header("Attack")]
    private Attack attack;
    [SerializeField] public float attackCooldown;
    public float delayBetweenAttaks = 1;
    public UnityEvent OnBeginCooldownAttack;
    public UnityEvent OnDoneCooldownAttack;
    public UnityEvent OnBeginJump;
    
    private void Start()
    {
        StartCoroutine(JumpCooldown());
        attack = GetComponentInChildren<Attack>();
        jump = GetComponent<Jump>();
        parryController = GetComponent<ParryController>();
    }

    private void Update()
    {
        if (parryController.isInWeakPoint)
        {
            return;
        }

        if (attack.isAtacking)
        {
            StartCoroutine(AttackCooldown());
        }
        
        if (jump.isGrounded && canJump && !stopJump)
        {
            StartCoroutine(DelayJump());
            stopJump = true;
        }
    }
    
    public void Randomize()
    {
        jumpCooldown = Random.Range(minJumpCooldown, maxJumpCooldown);
    }

    public IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    public IEnumerator AttackCooldown()
    {
        OnBeginCooldownAttack?.Invoke();
        attack.isAtacking = false;
        attack.ActiveCoroutine();
        yield return new WaitForSeconds(attackCooldown);
        OnDoneCooldownAttack?.Invoke();
    }

    private IEnumerator DelayJump()
    {
        OnBeginJump?.Invoke();
        yield return new WaitForSeconds(delayBetweenAttaks);
        jump.JumpAction();
    }
}
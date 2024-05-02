using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CooldownController : MonoBehaviour
{
    private ParryController parryController;
    private MiniSlimeGenerator miniSlimeGenerator;
    private SlimeController slimeController;
    private SecondFaseController secondFaseController;
    
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
    public bool isInAttack;

    [Header("Mini Slime")]
    [SerializeField] private float miniSlimeCooldown;
    private bool canInstantiate = false;
    
    private void Start()
    {
        StartCoroutine(JumpCooldown());
        attack = GetComponentInChildren<Attack>();
        jump = GetComponent<Jump>();
        parryController = GetComponent<ParryController>();
        miniSlimeGenerator = GetComponent<MiniSlimeGenerator>();
        slimeController = GetComponent<SlimeController>();
        secondFaseController = GetComponent<SecondFaseController>();
    }

    private void Update()
    {
        if(isInAttack && secondFaseController.animationControl)
        {
            OnDoneCooldownAttack?.Invoke();
        }
        
        if (parryController.isInWeakPoint || secondFaseController.animationControl)
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

        if (canInstantiate && slimeController.isAlreadyInSecondFase)
        {
            miniSlimeGenerator.InstantiateMiniSlime();
            StartCoroutine(TimeToMove());
            StartCoroutine(MiniSlimeInstantiateCooldown());
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
        isInAttack = false;
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

    private IEnumerator MiniSlimeInstantiateCooldown()
    {
        canInstantiate = false;
        yield return new WaitForSeconds(miniSlimeCooldown);
        canInstantiate = true;
    }

    public void StartMiniSlimeCoroutine()
    {
        canInstantiate = true;
    }

    public void StartCoutoutineTimeToMove()
    {
        StartCoroutine(TimeToMove());
    }

    private IEnumerator TimeToMove()
    {
        slimeController.waitToMove = true;
        yield return new WaitForSeconds(3);
        slimeController.waitToMove = false;
    }

    public void IsInAttack()
    {
        isInAttack = true;
    }
}
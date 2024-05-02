using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecondFaseController : MonoBehaviour
{
    private SlimeController slimeController;
    private Animator animator;
    public UnityEvent OnBegin;
    public UnityEvent OnDone;
    public bool animationControl;

    private void Start()
    {
        animator = GetComponent<Animator>();
        slimeController = GetComponent<SlimeController>();
        Animation();
    }

    private void Update()
    {
        if (animationControl)
        {
            animator.SetTrigger("Damaged");
        }
    }

    private void Animation()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("IdleAngry");
        animator.ResetTrigger("Move");
        animator.ResetTrigger("MoveAngry");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("JumpAngry");
        animator.ResetTrigger("Attack");
        animationControl = true;
        OnBegin?.Invoke();
    }

    public void OnDoneAnimation()
    {
        slimeController.animationHelper = false;
        animationControl = false;
        OnDone?.Invoke();
    }
}

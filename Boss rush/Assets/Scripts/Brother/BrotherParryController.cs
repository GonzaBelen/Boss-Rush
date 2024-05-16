using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrotherParryController : MonoBehaviour
{
    private BrotherController brotherController;
    public UnityEvent Event;
    private Animator animator;

    private void Start()
    {
        brotherController = GetComponent<BrotherController>();
        animator = GetComponent<Animator>();
    }

    public void InParried()
    {
        animator.ResetTrigger("BrotherAttack");
        brotherController.inParried = true;
        Event?.Invoke();
    }
}

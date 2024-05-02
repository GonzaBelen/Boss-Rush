using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParryController : MonoBehaviour
{
    private SlimeController slimeController;
    private Animator animator;
    [SerializeField] private float durationTime;
    public bool isInWeakPoint;
    public UnityEvent OnBeginWeakPoint;
    public UnityEvent OnEndWeakPoint;
    
    private void Start()
    {
        slimeController = GetComponent<SlimeController>();
        animator = GetComponent<Animator>();
    }

    public void Parry()
    {
        StartCoroutine(WeakPoint());
        if (!slimeController.isAlreadyInSecondFase)
        {
            animator.SetTrigger("Parried");
        } else
        {
            animator.SetTrigger("ParriedAngry");
        }        
    }

    private IEnumerator WeakPoint()
    {
        isInWeakPoint = true;
        OnBeginWeakPoint?.Invoke();
        yield return new WaitForSeconds(durationTime);
        isInWeakPoint = false;
        OnEndWeakPoint?.Invoke();
    }
}

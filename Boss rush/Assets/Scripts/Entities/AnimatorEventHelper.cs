using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered;
    public UnityEvent OnAttackPerformed;


    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }
}

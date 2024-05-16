using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered;
    public UnityEvent OnAttackPerformed;
<<<<<<< Updated upstream

=======
    public UnityEvent OnSlimeAttackTriggered;
    public UnityEvent OnBrotherAttackTriggered;
>>>>>>> Stashed changes

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }
<<<<<<< Updated upstream
=======

    public void BrotherTriggerAttack()
    {
        OnBrotherAttackTriggered?.Invoke();
    }
>>>>>>> Stashed changes
}

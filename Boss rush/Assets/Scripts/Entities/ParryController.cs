using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParryController : MonoBehaviour
{
    [SerializeField] private float durationTime;
    public bool isInWeakPoint;
    public UnityEvent OnBeginWeakPoint;
    public UnityEvent OnEndWeakPoint;
    
    public void Parry()
    {
        StartCoroutine(WeakPoint());
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

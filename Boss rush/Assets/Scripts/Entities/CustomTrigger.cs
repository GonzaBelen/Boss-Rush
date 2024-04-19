using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomTrigger : MonoBehaviour
{
public UnityEvent<Collider2D> TriggerEnter;
public UnityEvent<Collider2D> TriggerExit;

    private void OnBeginTriggerEnter(Collider2D collision)
    {
        TriggerEnter?.Invoke(collision);
    }

    private void OnBeginTriggerExit(Collider2D collision)
    {
        TriggerExit?.Invoke(collision);
    }
}
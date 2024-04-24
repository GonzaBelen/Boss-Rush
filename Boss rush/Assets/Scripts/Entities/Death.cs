using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator animator;

    private void Start ()
    {
        animator = GetComponent<Animator>();
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

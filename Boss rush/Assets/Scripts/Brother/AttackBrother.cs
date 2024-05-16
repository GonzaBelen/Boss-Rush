using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackBrother : MonoBehaviour
{
    private BrotherController brotherController;
    private Knockback knockback;
    public float damage = 0;
    private PolygonCollider2D polygonCollider2D;
    public UnityEvent Event;

    private void Start ()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        brotherController = GetComponentInParent<BrotherController>();
        knockback = GetComponentInParent<Knockback>();
    }

    public void AttackActivation()
    {
        polygonCollider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.isParry)
            {
                Event?.Invoke();
            } else
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.GetHit(damage, gameObject);
                    knockback.PlayFeedback(gameObject);
                }
            }
        }
        polygonCollider2D.enabled = false;
        //brotherController.isAttacking = true;
    }

     public void TurnOffCollider()
    {
        polygonCollider2D.enabled = false;
    }
}

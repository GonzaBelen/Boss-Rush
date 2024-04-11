using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PeakController : MonoBehaviour
{
    [SerializeField] private Knockback knockback;
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private float peakDamage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.GetHit(peakDamage, gameObject);
                knockback.PlayFeedback(gameObject);
            }
        }
    }
}

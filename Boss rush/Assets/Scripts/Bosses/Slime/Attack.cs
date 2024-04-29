using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private SlimeController slimeController;
    private Knockback knockback;
    [SerializeField] public float damage;
    private PolygonCollider2D polygonCollider2D;
    public bool isAtacking = false;

    private void Start ()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        slimeController = GetComponentInParent<SlimeController>();
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
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.GetHit(damage, gameObject);
                knockback.PlayFeedback(gameObject);
            }
        }
        isAtacking = true;
    }

    public void TurnOffCollider()
    {
        polygonCollider2D.enabled = false;
    }

    public void ActiveCoroutine()
    {
        StartCoroutine(TimeToMove());
    }

    private IEnumerator TimeToMove()
    {
        slimeController.waitToMove = true;
        yield return new WaitForSeconds(3);
        slimeController.waitToMove = false;
    }
}

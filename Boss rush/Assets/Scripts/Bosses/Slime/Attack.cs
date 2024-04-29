using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    private SlimeController slimeController;
    private Knockback knockback;
    [SerializeField] public float damage;
    private PolygonCollider2D polygonCollider2D;
    public bool isAtacking = false;
    public UnityEvent Event;

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

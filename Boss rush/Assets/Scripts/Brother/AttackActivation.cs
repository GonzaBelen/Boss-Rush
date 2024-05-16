using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackActivation : MonoBehaviour
{
    [SerializeField] private GameObject brother;
    private BrotherController brotherController;
    private AttackRangeBrother attackRangeBrother;
    [SerializeField] private CircleCollider2D circleCollider2D;
    public UnityEvent OnCollisionEnter;

    private void Start()
    {
        brotherController = brother.GetComponent<BrotherController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            brotherController.canAttack = true;
            OnCollisionEnter?.Invoke();
            circleCollider2D.enabled = false;
        }
    }
}

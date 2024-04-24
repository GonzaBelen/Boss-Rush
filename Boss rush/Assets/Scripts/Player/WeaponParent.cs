using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponParent : MonoBehaviour
{
    private PlayerController playerController;
    public Vector2 PointerPosition { get; set; }
    private Vector2 positionTransform;
    [SerializeField] private Transform playerTransform;
    public float delay = 0.3f;
    public bool attackBlocked = false;
    public bool IsAttacking { get; private set; }
    public Animator animator;
    public Transform circleOrigin;
    public float radius;
    public Vector2 lookDirection;
    [SerializeField] private float damage;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if (IsAttacking)
            return;
        Vector2 direction = (PointerPosition-(Vector2)transform.position).normalized;
        transform.right = direction;

        lookDirection = PointerPosition - positionTransform;

        positionTransform = TransformToVector2(playerTransform);
        AnimateCharacter();

        Vector2 scale = transform.localScale;
        if (lookDirection.x < -0.5f)
        {
            scale.y = -1;
            scale.x = -1;
        }
        else if (lookDirection.x > 0.5f)
        {
            scale.y = 1;
            scale.x = 1;
        }
        transform.localScale = scale;
    }

    public Vector2 TransformToVector2(Transform transform)
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    private void AnimateCharacter()
    {
        playerController.Flip(lookDirection);
    }

    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            //Debug.Log(collider.name);
            Health health;
            if(health = collider.GetComponent<Health>())
            {
                health.GetHit(damage, transform.parent.gameObject);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;
public class SlimeController : MonoBehaviour
{
    [SerializeField] private Collider2D stopMovementCollider;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private GameObject player;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float distance;

    

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }
}

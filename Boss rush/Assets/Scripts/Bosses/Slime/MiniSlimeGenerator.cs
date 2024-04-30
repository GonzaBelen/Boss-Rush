using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniSlimeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject miniSlime;
    private float jumpForceVertical;
    private float jumpForceHorizontal;

    public void InstantiateMiniSlime()
    {
        GameObject prefabClone = Instantiate(miniSlime, transform.position, Quaternion.identity);
        Rigidbody2D cloneRigidBody = prefabClone.GetComponent<Rigidbody2D>();
        cloneRigidBody.AddForce(transform.up * (jumpForceVertical = Random.Range(70, 100)), ForceMode2D.Impulse);
        cloneRigidBody.AddForce(transform.right * (jumpForceHorizontal = Random.Range(20, 40)), ForceMode2D.Impulse);
        new WaitForSeconds(0.2f);
        GameObject prefabClone1 = Instantiate(miniSlime, transform.position, Quaternion.identity);
        Rigidbody2D cloneRigidBody1 = prefabClone1.GetComponent<Rigidbody2D>();
        cloneRigidBody1.AddForce(transform.up * (jumpForceVertical = Random.Range(70, 100)), ForceMode2D.Impulse);
        cloneRigidBody1.AddForce(transform.right * (jumpForceHorizontal = Random.Range(-40, -20)), ForceMode2D.Impulse);
        // new WaitForSeconds(0.2f);
        // GameObject prefabClone2 = Instantiate(miniSlime, transform.position, Quaternion.identity);
        // Rigidbody2D cloneRigidBody2 = prefabClone2.GetComponent<Rigidbody2D>();
        // cloneRigidBody2.AddForce(transform.up * (jumpForceVertical = Random.Range(70, 100)), ForceMode2D.Impulse);
        // cloneRigidBody2.AddForce(transform.right * (jumpForceHorizontal = Random.Range(Random.Range(-40, -20), Random.Range(20, 40))), ForceMode2D.Impulse);
        // new WaitForSeconds(0.2f);
        // GameObject prefabClone3 = Instantiate(miniSlime, transform.position, Quaternion.identity);
        // Rigidbody2D cloneRigidBody3 = prefabClone3.GetComponent<Rigidbody2D>();
        // cloneRigidBody3.AddForce(transform.up * (jumpForceVertical = Random.Range(70, 100)), ForceMode2D.Impulse);
        // cloneRigidBody3.AddForce(transform.right * (jumpForceHorizontal = Random.Range(Random.Range(-40, -20), Random.Range(20, 40))), ForceMode2D.Impulse);
    }
}
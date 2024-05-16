using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControllers : MonoBehaviour
{
    [SerializeField] private GameObject brother;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject brotherForScript;
    private BrotherController brotherController;

    private void Start()
    {
        brotherController = brotherForScript.GetComponent<BrotherController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null)
        {
            if (collision.gameObject.CompareTag("Brother"))
            {
                Destroy(gameObject);
                brotherController.canMove = false;
                Vector2 flipping = new Vector2(-1, -1);
                brotherController.Flip(flipping);
            }
        }

        if (brother == null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                brotherController.canMove = true;
            }
        } else 
        {
            Debug.Log("No se cargo ni player ni brother");
            return;
        }
    }    
}

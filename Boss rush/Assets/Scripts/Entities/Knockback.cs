using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform playerTransform;
    [SerializeField] PlayerController playerController;
    [SerializeField] private float strength;
    [SerializeField] private float delay;
    public UnityEvent OnBegin;
    public UnityEvent OnDone;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); 
        rb2D = playerObject.GetComponent<Rigidbody2D>();
        playerController = playerObject.GetComponent<PlayerController>();
        playerTransform = playerObject.GetComponent<Transform>();
    }

    public void PlayFeedback (GameObject sender)
    {
        StopAllCoroutines();
        playerController.knockbackCheck = true;
        OnBegin.Invoke();
        Vector2 direction = (sender.transform.position - playerTransform.position).normalized;
        rb2D.AddForce(transform.up * strength, ForceMode2D.Impulse);
        if (direction.x >= 0)
        {
            rb2D.AddForce(transform.right * -strength, ForceMode2D.Impulse);
        } else
        {
            rb2D.AddForce(transform.right * strength, ForceMode2D.Impulse);
        }
        StartCoroutine(Reset());
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        playerController.knockbackCheck = false;
        rb2D.velocity = Vector3.zero;
        OnDone?.Invoke();
    }

}

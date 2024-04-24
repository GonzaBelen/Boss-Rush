using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownController : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float maxJumpCooldown;
    [SerializeField] private float minJumpCooldown;
    private float jumpCooldown;
    public bool canJump;
    
    private void Start()
    {
        StartCoroutine(JumpCooldown());
    }
    
    public void Randomize()
    {
        jumpCooldown = Random.Range(minJumpCooldown, maxJumpCooldown);
    }

    public IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public float armor;
    public UnityEvent<GameObject> OnHitWithReference;
    public UnityEvent<GameObject> OnDeathWithReference;
    [SerializeField] public bool isDead = false;
    public bool canHurt = true;
    private bool weakPointDamage = false;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth); 
        }
    }
    
    private void Update()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }
    
    public void InitializeHealth(float healthValue)
    {
        if (canHurt)
        {
            currentHealth = healthValue;
            maxHealth = healthValue;
            isDead = false;
        }
        else
        {
            Debug.Log("Es inmune");
        }
    }

    public void GetHit(float amount, GameObject sender)
    {
        if (isDead || !canHurt)
        {
            return;
        }            
        if (sender.layer == gameObject.layer)
        {
            return;
        }
        if (weakPointDamage)
        {
            amount *= 1.25f;
        } else
        {
            amount *= (100 - armor) / 100;
        }
        currentHealth -= amount;
        Debug.Log(amount);
        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            StopAllCoroutines();
            if (gameObject.layer == LayerMask.NameToLayer("Player") || gameObject.layer == LayerMask.NameToLayer("Enemie"))
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemie"), true);
            }            
        }
    }

    public void Inmunity()
    {
        canHurt = true;
    }

    public void NotInmunity()
    {
        canHurt = false;
    }

    public void OnWeakPoint()
    {
        weakPointDamage = true;
    }

    public void OnWeakPointEnd()
    {
        weakPointDamage = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    public UnityEvent<GameObject> OnHitWithReference;
    public UnityEvent<GameObject> OnDeathWithReference;
    [SerializeField] private bool isDead = false;
    public bool canHurt = true;

    private void Start()
    {
        currentHealth = maxHealth;
        
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
<<<<<<< Updated upstream
            healthBar.SetMaxHealth(maxHealth);
=======
            healthBar.SetMaxHealth(maxHealth); 
        } else
        {
            return;
>>>>>>> Stashed changes
        }
    }
    
    private void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            healthBar.SetHealth(currentHealth);
        } else 
        {
            return;
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
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            StopAllCoroutines();
            if (gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                Destroy(gameObject);
            }            
        }
    }
}

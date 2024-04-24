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
        healthBar.SetMaxHealth(maxHealth);        
        // if (gameObject.layer == LayerMask.NameToLayer("Player"))
        // {
        //     healthBar.SetMaxHealth(maxHealth);
        // }
    }
    
    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        // if (gameObject.layer == LayerMask.NameToLayer("Player"))
        // {
        //     healthBar.SetHealth(currentHealth);
        // }
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
            //Debug.Log("Aca se llama a la funcion obteniendo el script y ejecutando el danio");
            return;
        }            
        if (sender.layer == gameObject.layer)
        {
            //Debug.Log("Si paso de aqui es porque detecto que se lo puede daniar");
            return;
        }
        currentHealth -= amount;

        if (currentHealth > 0)
        {
            //Debug.Log("Si paso aqui entonces se esta ejecutando el danio");
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            StopAllCoroutines();
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemie"), true);
            } 
            // if (gameObject.layer != LayerMask.NameToLayer("Player"))
            // {
            //     Destroy(gameObject);
            // }            
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
}

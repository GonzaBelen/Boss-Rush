using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigor : MonoBehaviour
{
    [SerializeField] private VigorBar vigorBar;
    [SerializeField] private float maxVigor;
    public float currentVigor;
    public bool canMakeIt;
    [SerializeField] private float vigorRegeneration;
    
    private void Start()
    {
        currentVigor = maxVigor;
        if (vigorBar != null)
        {
            vigorBar.SetMaxVigor(maxVigor);
        } else
        {
            return;
        }  
    }
    
    private void FixedUpdate()
    {
        if (currentVigor < maxVigor)
        {
            currentVigor += vigorRegeneration;
        }

        if (currentVigor < 0)
        {
            currentVigor = 0;
        }

        if (vigorBar != null)
        {
            vigorBar.SetVigor(currentVigor);
        } else
        {
            return;
        }
    }

    public void VigorController(float cost)
    {
        if (currentVigor >= cost)
        {
            canMakeIt = true;
            currentVigor -= cost;
        }
        else
        {
            canMakeIt = false;
        }
    }
}

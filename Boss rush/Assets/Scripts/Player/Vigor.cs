using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigor : MonoBehaviour
{
    [SerializeField] private float maxVigor;
    public float currentVigor;
    public bool canMakeIt;
    [SerializeField] private float vigorRegeneration;
    
    private void Start()
    {
        currentVigor = maxVigor;
    }
    
    private void Update()
        {
            if (currentVigor < maxVigor)
            {
                currentVigor += vigorRegeneration;
            }

            if (currentVigor < 0)
            {
                currentVigor = 0;
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

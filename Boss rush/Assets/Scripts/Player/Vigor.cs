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
        vigorBar.SetMaxVigor(maxVigor);
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
            
            vigorBar.SetVigor(currentVigor);
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

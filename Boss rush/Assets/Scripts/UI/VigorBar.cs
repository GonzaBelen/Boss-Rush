using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VigorBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxVigor(float vigor)
    {
        slider.maxValue = vigor;
        slider.value = vigor;
    }
    
    public void SetVigor(float vigor)
    {
        slider.value = vigor;
    }
}

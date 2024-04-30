using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public float jumpForceVertical;
    public float jumpForceHorizontal;

    public void Randomize()
    {
        jumpForceVertical = Random.Range(70, 100);
        jumpForceHorizontal = Random.Range(Random.Range(-40, -20), Random.Range(20, 40));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemHelper : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private void Update()
    {
        playerInput.enabled = false;
    }
}

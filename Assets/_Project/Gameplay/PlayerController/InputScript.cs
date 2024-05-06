using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private Vector2 _locomotionInput;

    //when this function is called it will take in the context of the action and make that the locomotionInput the engine takes in.
    public void OnMove(InputAction.CallbackContext context)
    {
        _locomotionInput = context.action.ReadValue<Vector2>();
    }
}

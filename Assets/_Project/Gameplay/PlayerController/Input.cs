using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private Vector2 _locomotionInput;

    
    [SerializeField]
    private float _speed;
    private PlayerControls _playerActions;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerActions = new PlayerControls();

        _playerRigidbody = GetComponent<Rigidbody>();
        if (_playerRigidbody == null)
            Debug.LogError("Rigidbody is NULL");
    }

    private void OnEnable()
    {
        _playerActions.Locomotion.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Locomotion.Disable();
    }
    //when this function is called it will take in the context of the action and make that the locomotionInput the engine takes in.
 //   public void OnMove(InputAction.CallbackContext context)
 //   {
    //    _locomotionInput = context.action.ReadValue<Vector2>();
     //   print(_locomotionInput);
   //     _playerRigidbody.velocity = _locomotionInput * _speed;
   // }

    private void FixedUpdate()
    {

        _locomotionInput = _playerActions.Locomotion.Move.ReadValue<Vector2>();
        _playerRigidbody.velocity = _locomotionInput * _speed;
    }

}

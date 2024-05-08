using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    //the main vector 2 for movement
    private Vector2 _locomotionInput;

    
    //how many units a character moves per second of input in a direction
    [SerializeField]
    private float _speed;

    //the Action map used for input.cs
    private PlayerControls _playerActions;

    //the player's rigidbody
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        //defines _playerActions before start is called.
        _playerActions = new PlayerControls();

        //defines _playerRigidbody before start is called. if the rigidbody component is null then the debug log will alert of it.
        _playerRigidbody = GetComponent<Rigidbody>();
        if (_playerRigidbody == null)
            Debug.LogError("Rigidbody is NULL");
    }

    private void OnEnable()
    {
        //when OnEnable is called it enables this action map
        _playerActions.Locomotion.Enable();
    }

    private void OnDisable()
    {
        //when OnDisable is called it disables this action map
        _playerActions.Locomotion.Disable();
    }
   
    private void FixedUpdate()
    {
        //makes locomotion input = to whatever direction the action map says it is based on a 2D X,Y axis
        _locomotionInput = _playerActions.Locomotion.Move.ReadValue<Vector2>();

        //takes that Vector 2 we created previously and uses that information to make a vector 3 for our input direction
        Vector3 move = new Vector3(_locomotionInput.x, 0, _locomotionInput.y);

        //makes the player's velocity = to the vector 3 move direction * our speed value.
        _playerRigidbody.velocity = move * _speed;
    }

}

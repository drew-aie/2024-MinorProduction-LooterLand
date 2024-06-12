using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using Unity.VisualScripting;
using UnityEngine.Events;

public class Input : MonoBehaviour
{
    //the main vector 2 for movement
    private Vector2 _locomotionInput;

    //how many units a character moves per second of input in a direction
    [SerializeField, Tooltip("The speed of the player.")]
    [Range(1, 100)]
    private float _speed;

    //property for _speed.
    public float MaxSpeed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    //the Action map used for input.cs
    private PlayerControls _playerActions;

    //the player's rigidbody
    private Rigidbody _playerRigidbody;

    public UnityEvent OnStartMoving;

    public UnityEvent OnStopMoving;

    private bool _isMoving;

    private void Awake()
    {
        //defines _playerActions before start is called.
        _playerActions = new PlayerControls();

        //defines _playerRigidbody before start is called. if the rigidbody component is null then the debug log will alert of it.
        _playerRigidbody = GetComponent<Rigidbody>();
        if (_playerRigidbody == null)
            Debug.LogError("Rigidbody is NULL");

        _isMoving = false;
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

    private void Update()
    {

        //makes locomotion input = to whatever direction the action map says it is based on a 2D X,Y axis
        _locomotionInput = _playerActions.Locomotion.Move.ReadValue<Vector2>();

    }


    private void FixedUpdate()
    {


        if (_playerRigidbody.velocity.y > 0.5f || _playerRigidbody.velocity.y < -0.5f)
            return;

        _playerRigidbody.angularVelocity = Vector3.zero;

        float speedOffset = _speed * 2;

        //takes that Vector 2 we created previously and uses that information to make a vector 3 for our input direction. keeps y velocity from rigidbody.
        Vector3 move = new Vector3(_locomotionInput.x, 0, _locomotionInput.y).normalized;

        Vector3 force = move * speedOffset * Time.deltaTime;

        _playerRigidbody.AddForce(force, ForceMode.VelocityChange);


        //dont update rotation if we arent moving.
        if (move.magnitude < 0.1f)
        {
            if (_isMoving)
            {
                _isMoving = false;

                OnStopMoving.Invoke();
            }

            return;
        }
        //else if we just began moving
        else if(!_isMoving)
        {
            _isMoving = true;

            OnStartMoving.Invoke();
        }


        //Find the direction the player should look towards, y is 0 because player should always look level.
        Vector3 lookDir = new Vector3(move.x, 0, move.z);
        //Create a rotation from the player's forward to the look direction
        Quaternion rotation = Quaternion.LookRotation(lookDir);
        //Set the rotation to be the new rotation found
        _playerRigidbody.MoveRotation(rotation);

    }

        

    }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CopSeekBehavior : MonoBehaviour
{
    [SerializeField, Tooltip("What the cop will be chasing. (The Player)")]
    private GameObject _target;

    private NavMeshAgent _cop;
    private float _accelerationSpeed;
    private float _maxSpeed;
    private float _currentAngularVelocity;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _lastFacing;

    private float _moveSuppressionDuration;

    private float _moveSuppressionScalar;

    private Vector3 _slipDirection;

    // Start is called before the first frame update
    void Awake()
    {
        _cop = GetComponent<NavMeshAgent>();
        _cop.updateRotation = false;
        _accelerationSpeed = _cop.acceleration;
        _maxSpeed = _cop.speed;
    }

    public void SuppressInput(float scalar, float duration)
    {
        _moveSuppressionDuration = duration;

        _moveSuppressionScalar = scalar;

        _slipDirection = gameObject.transform.forward.normalized;
    }


    // FixedUpdate is called once per fixed framerate frame
    void Update()
    {
        if (!_cop.enabled)
            return;


        if (_moveSuppressionDuration > 0.0f)
        {
            Rigidbody rig = gameObject.GetComponent<Rigidbody>();

            //moves the cop in its last direction
            rig.AddForce(_slipDirection * 20, ForceMode.Force);

            _moveSuppressionDuration -= Time.deltaTime;
        }
        else
            _cop.destination = _target.transform.position;

        //Making agent face the direction it's travelling using it's position and velocity
        _cop.transform.LookAt(_cop.transform.position + _cop.velocity);

        //Smoothing agent movement to prevent jittering
        _cop.transform.position = Vector3.SmoothDamp(_cop.transform.position, _cop.nextPosition, ref _velocity, 0.05f);

        SpeedScale();
    }

    private void FixedUpdate()
    {
        //Checking if agent is rotating
        if (RotationCheck())
            //Scaling agent's acceleration down by it's speed value if so
            _cop.acceleration = MapValue(0, _cop.speed, _cop.acceleration, 20, 1);
        else
            //Resetting acceleration if not
            _cop.acceleration = _accelerationSpeed;
    }

    //Scales the agent's speed with it's distance from the player (Fast when far, slower when close)
    private void SpeedScale()
    {
        float seekMagnitude = (_target.transform.position - _cop.transform.position).magnitude;

        //Scaling agent's speed using distance from player and it's max speed times 1.5
        _cop.speed = MapValue(0, seekMagnitude, _maxSpeed * 1.5f, 3, 1);
    }

    //Checks if the agent is currently rotating
    private bool RotationCheck()
    {
        Vector3 currentFacing = _cop.transform.forward;

        //Storing how many degrees the agent is rotating per second
        _currentAngularVelocity = Vector3.Angle(currentFacing, _lastFacing) / Time.deltaTime;

        _lastFacing = currentFacing;

        //Checking if degrees rotated per second is greater than 1
        bool agentRotating = _currentAngularVelocity > 1f;

        //Returning true or false if so
        if (agentRotating)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Returns a number between two values/ranges.
    /// </summary>
    /// <param name="inputMin">The lowest number of the range input.</param>
    /// <param name="inputMax">The largest number of the range input.</param>
    /// <param name="outputMin">The lowest number of the range output</param>
    /// <param name="outputMax">The largest number of the range output</param>
    /// <param name="value">Input value.</param>
    /// <returns>The number between the entered ranges.</returns>
    private float MapValue(float inputMin, float inputMax, float outputMin, float outputMax, float value)
    {
        return outputMin + ((outputMax - outputMin) / (inputMax - inputMin)) * (value - inputMin);
    }
}

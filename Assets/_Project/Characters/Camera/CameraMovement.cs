using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    Vector3 cam1;
    Vector3 cam2;
    Vector3 lerp;


    //  [SerializeField]
    // private Transform _lookAtTarget;

    [SerializeField, Range(0,0.1f)]
    public float _smoothTime = 0.1f;

    private Vector3 _offset;

    public Vector3 Offset
    { 
        get { return _offset; }
        set { _offset = value; } 
    }

    private void Start()
    {
        _offset = _target.position - transform.position;
    }

    private void LateUpdate()
    {
        
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, _target.position - _offset, ref velocity, _smoothTime);

        ///this adjusts the camera always keep the target of the camera in view.
      //  transform.LookAt(_lookAtTarget);
    }


}


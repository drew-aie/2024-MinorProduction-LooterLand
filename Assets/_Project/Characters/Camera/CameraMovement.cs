using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    public float _smoothTime = 0.1f;

    private Vector3 _offset;

    private void Start()
    {
        _offset = _target.position - transform.position;
    }

    private void Update()
    {
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, _target.position - _offset, ref velocity, _smoothTime);

        ///this adjusts the camera always keep the target of the camera in view.
       // transform.LookAt(_target);
    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventRotation : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehaviour : MonoBehaviour
{
    public float rotationSpeed;
    public bool isRotating = false;
    void FixedUpdate()
    {
        if(isRotating)
            transform.Rotate(new Vector3(0.0f, 0.0f, rotationSpeed) * Time.deltaTime * 50);
    }
}

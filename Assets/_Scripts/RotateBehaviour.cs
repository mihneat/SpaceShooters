using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehaviour : MonoBehaviour
{
    public Transform obj;
    public float rotationSpeed;
    public bool isRotating = false, isRotatingAround = false;

    void FixedUpdate()
    {
        if (isRotating) transform.Rotate(new Vector3(0.0f, 0.0f, rotationSpeed) * Time.deltaTime * 50);
        else if (isRotatingAround)
        {
            if (obj != null)
            {
                transform.RotateAround(obj.transform.position, Vector3.forward, rotationSpeed);
            }
        }
    }
}

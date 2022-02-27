using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{

    // Update is called once per frame

    public Transform PlayerTransform;
    public float RotationSpeed = 1.0f;

    public float MaxYRotation = 10f;
    public float MinYRotation = -10f;

    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Mouse X");
        float vertical = Input.GetAxisRaw("Mouse Y");

        if (Mathf.Abs(horizontal) >= 0.1f)
        {
            transform.RotateAround(PlayerTransform.position, Vector3.up * horizontal, RotationSpeed);
        }

        //if (Mathf.Abs(vertical) >= 0.1f && !ExceedingYLimit())
        //{
        //    Quaternion originalRotation = transform.rotation;

        //    transform.RotateAround(PlayerTransform.position, Vector3.right * vertical, RotationSpeed);
            
        //    if (ExceedingYLimit()) {
        //        transform.rotation = originalRotation;
        //    }
        //}

    }

    private bool ExceedingYLimit() {

        if (transform.rotation.y >= MaxYRotation || transform.rotation.y <= MinYRotation) {
            return true;
        }

        return false;
    }
}

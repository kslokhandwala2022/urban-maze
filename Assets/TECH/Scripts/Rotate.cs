using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;

    // User Inputs
    public float degreesPerSecond;
    public float amplitude;
    public float frequency;

    // Position Storage Variables
    Vector3 posOffset;
    Vector3 tempPos;

    void Start()
    {
        tempPos = gameObject.transform.position;
        posOffset = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);

        //Oscillate
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}

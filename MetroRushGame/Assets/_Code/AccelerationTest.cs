using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class AccelerationTest : MonoBehaviour
{
    public float acceleration;
    private float speed = 0f;
    private void FixedUpdate()
    {
        Accelerate();
    }

    private void Accelerate()
    {
        speed += acceleration * Time.deltaTime;
        Debug.Log(speed);
    }

}

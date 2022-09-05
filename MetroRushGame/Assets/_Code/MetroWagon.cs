using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class MetroWagon : MonoBehaviour
{
    public float Speed;
    public float Acceleration;
    public float Breaking;
    public float MaxSpeed;
    public float mass;
    public float TargetSpeed;
    public bool changing = false;

    public void ChangeSpeed(float currentSpeed, float targetSpeed)
    {
        if(!changing)
        {
            changing = true;
            if (currentSpeed >= targetSpeed && currentSpeed <= MaxSpeed)
            {
                Break(targetSpeed);
            }
            else if (currentSpeed < targetSpeed)
            {
                Accelerate(targetSpeed);
            }
           Speed = Mathf.Clamp(Speed, 0, MaxSpeed);
        }
       
    }

    public void Accelerate(float targetSpeed)
    {
        while (Speed < targetSpeed)
        {
            Speed = Speed + (Acceleration * Time.deltaTime)/mass;
        }
        changing = false;
    }

    public void Break(float targetSpeed)
    {
        
        while (Speed > targetSpeed)
        {
            Speed = Speed - (Breaking * Time.deltaTime)/mass;
        }
        changing = false;
    }
}

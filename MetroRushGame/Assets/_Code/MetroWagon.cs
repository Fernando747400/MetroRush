using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class MetroWagon : MonoBehaviour
{
    [Header("Dependenices")]
    public MapManager mapManager;

    [Header("Settings")]
    public float Acceleration;
    public float Breaking;
    public float MaxSpeed;
    public float mass;

    [HideInInspector] public float Speed;
    [HideInInspector] public float TargetSpeed;

    public void ChangeSpeed(float targetSpeed)
    {
        if (Speed > targetSpeed)
        {
            Break(targetSpeed);
        }
        else if (Speed < targetSpeed)
        {
            Accelerate(targetSpeed);
        }
        Speed = Mathf.Clamp(Speed, 0, MaxSpeed);
    }

    public void Accelerate(float targetSpeed)
    {
        Speed += (Acceleration * Time.deltaTime)/mass;
        mapManager.ChangeSpeed(Speed);
    }

    public void Break(float targetSpeed)
    {
        Speed -= (Breaking * Time.deltaTime)/mass;
        mapManager.ChangeSpeed(Speed); 
    }
}

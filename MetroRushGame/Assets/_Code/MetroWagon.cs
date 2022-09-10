using System.Collections;
using System.Collections.Generic;
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
        if (Speed < 0) Speed = 0f;
        mapManager.ChangeSpeed(Speed); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Station"))
        {
            Debug.Log("Arrived at " + other.GetComponentInParent<MetroStation>().StationData.StationName);
            Debug.Log("You must arrive at " + MaxSpeed * 0.1f + "And your current speed is " + Speed);
        } else if (other.CompareTag("BreakZone"))
        {
            if (Speed <= MaxSpeed * 0.1)
            {
                TargetSpeed = 0f;
                Speed = 0f;
                mapManager.ChangeSpeed(Speed);
                GameManager.Instance.ChangeState(GameManager.GameState.Arrival);
            }
            Debug.Log("Arrived at break point with speed " + Speed);
        }
    }
}

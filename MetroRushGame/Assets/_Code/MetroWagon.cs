using System.Collections.Generic;
using UnityEngine;

public class MetroWagon : MonoBehaviour
{
    [Header("Dependenices")]
    public MapManager MapManagerInstance;
    public PeopleManager PeopleManagerInstance;

    [Header("Settings")]
    public float Acceleration;
    public float Breaking;
    public float MaxSpeed;
    public float Mass;

    private List<People> _passengerList = new List<People>();

    [HideInInspector] public float Speed;
    [HideInInspector] public float TargetSpeed;

    public List<People> PassengerList { get => _passengerList; set => _passengerList = value; }

    public void ChangeSpeed(float targetSpeed)
    {
        Debug.Log("Target speed " + targetSpeed);
        if (Speed > targetSpeed)
        {
            Break(targetSpeed);
        }
        else if (Speed < targetSpeed)
        {
            Accelerate(targetSpeed);
        }
        Speed = Mathf.Clamp(Speed, 0, MaxSpeed);
        MapManagerInstance.ChangeSpeed(Speed);
        Debug.Log("Speed sent from Metro " + Speed);
    }

    public void Accelerate(float targetSpeed)
    {
        Speed = Speed + (Acceleration * Time.deltaTime)/Mass;
        Debug.Log("Acelleration speed " + Speed);
    }

    public void Break(float targetSpeed)
    {
        Speed = Speed - (Breaking * Time.deltaTime)/Mass;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Station"))
        {
            Debug.Log("Arrived at " + other.GetComponentInParent<MetroStation>().StationData.StationName);
            GameManager.Instance.ChangeState(GameManager.GameState.Arrival);
            GameManager.Instance.CurrentStation = other.GetComponentInParent<MetroStation>();
        }
        else if (other.CompareTag("BreakZone") && GameManager.Instance._gameState == GameManager.GameState.Arrival)
        {
            if (Speed <= MaxSpeed * 0.1)
            {
                TargetSpeed = 0f;
                Speed = 0f;
                MapManagerInstance.ChangeSpeed(Speed);
                GameManager.Instance.ChangeState(GameManager.GameState.OnStation);
                RemovePassenger();
            }
            Debug.Log("Arrived at break point with speed " + Speed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BreakZone"))
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Transit);
        }
    }

    public void AddPassenger(People passenger)
    {
        _passengerList.Add(passenger);
    }

    public void RemovePassenger()
    {
        _passengerList = PeopleManagerInstance.RemovePassengers(_passengerList);
    }
}

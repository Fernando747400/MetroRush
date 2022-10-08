using System;
using System.Collections.Generic;
using UnityEngine;

public class MetroWagon : MonoBehaviour
{
    public static MetroWagon Instance;

    [Header("Dependenices")]
    public MapManager MapManagerInstance;
    public PeopleManager PeopleManagerInstance;

    [Header("Settings")]
    public float Acceleration;
    public float Breaking;
    public float MaxSpeed;
    public float Mass;
    
    public int MaxCapacity;
    [HideInInspector] public int CurrentCapacity = 0;

    [HideInInspector] public float Speed;
    [HideInInspector] public float TargetSpeed;

    private List<People> _passengerList = new List<People>();

    public event Action OnFullCapacity;
    public event Action OnPassengerChanged;

    public List<People> PassengerList { get => _passengerList; set => _passengerList = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Prepare();
    }

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
        MapManagerInstance.ChangeSpeed(Speed);
    }

    public void Accelerate(float targetSpeed)
    {
        Speed = Speed + (Acceleration * Time.deltaTime)/Mass;
    }

    public void Break(float targetSpeed)
    {
        Speed = Speed - (Breaking * Time.deltaTime)/Mass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Station"))
        {
            Debug.Log("Arrived at " + other.GetComponentInParent<MetroStation>().StationData.StationName);
            GameManager.Instance.CurrentStation = other.GetComponentInParent<MetroStation>();
            GameManager.Instance.ChangeState(GameManager.GameState.Arrival);
        } else if (other.CompareTag("Finish"))
        {
            GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
        }
    }

    private void OnTriggerStay(Collider other)
    {     
        if (other.CompareTag("BreakZone") && GameManager.Instance._gameState == GameManager.GameState.Arrival)
        {
            if (Speed <= MaxSpeed * 0.1)
            {
                TargetSpeed = 0f;
                Speed = 0f;
                MapManagerInstance.ChangeSpeed(Speed);
                GameManager.Instance.ChangeState(GameManager.GameState.OnStation);
                RemovePassengers();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BreakZone"))
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Transit);
        }
    }
    
    public bool TryAddPassenger(People passenger)
    {
        {
            if (CurrentCapacity < MaxCapacity)
            {
                CurrentCapacity++;
                AddPassenger(passenger);
                return true;
            }
            else
            {
                OnFullCapacity?.Invoke();
                return false;
            }
        }
    }

    private void AddPassenger(People passenger)
    {
        _passengerList.Add(passenger);
        InvokePassengerChanged();
    }

    public void RemovePassengers()
    {
        int passengerCount = _passengerList.Count;
        _passengerList = PeopleManagerInstance.RemovePassengers(_passengerList);
        CurrentCapacity -= passengerCount - _passengerList.Count;
        InvokePassengerChanged();
    }

    private void ExpandCapacity()
    {      
        MaxCapacity += 10;   
    }

    private void Prepare()
    {
        UIManager.Instance.ExpandCapacity.AddListener(ExpandCapacity);   
    }

    private void InvokePassengerChanged()
    {
        OnPassengerChanged?.Invoke();
    }
}

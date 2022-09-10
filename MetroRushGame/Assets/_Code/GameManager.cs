using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [Header("Dependencies")]
    [SerializeField] private MetroWagon _metroWagon;
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _actualSpeed;

    public UnityEvent Arrived;
    public UnityEvent Transit;


    public enum GameState
    {
        Start,
        Transit,
        Arrival,
        OnStation,
    }

     public GameState _gameState;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        _gameState = GameState.Transit;
    }

    public void ChangeState(GameState state)
    {
        _gameState = state;
        InvokeChangeState();
    }

    public void Continue()
    {
        _gameState = GameState.Transit;
        InvokeChangeState();
    }

    public void InvokeChangeState()
    {
        switch (_gameState)
        {
            case GameState.Arrival:
                Arrived?.Invoke();
                break;

            case GameState.Transit:
                Transit?.Invoke();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (_gameState == GameState.Transit) ChangeSpeed(_slider.value);
        else if (_gameState == GameState.Arrival) _slider.value = _metroWagon.Speed;
    }

    public void ChangeSpeed(float targetSpeed)
    {
        if (_gameState == GameState.Transit)
        {
            _metroWagon.ChangeSpeed(targetSpeed);
        }
        _actualSpeed.value = _metroWagon.Speed;
    }
}

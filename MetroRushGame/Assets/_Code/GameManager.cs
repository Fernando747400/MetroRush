using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [Header("Dependencies")]
    [SerializeField] private MetroWagon _metroWagon;
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private Slider _slider;
    


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
    }

    private void FixedUpdate()
    {     
        ChangeSpeed(_slider.value);
    }

    public void ChangeSpeed(float targetSpeed)
    {
        if(_gameState == GameState.Transit)
        {
            _metroWagon.ChangeSpeed(targetSpeed);
        }      
    }
}

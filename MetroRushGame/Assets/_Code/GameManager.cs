using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState _gameState;

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

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        _gameState = GameState.Transit;
    }

    private void FixedUpdate()
    {
        if (_gameState == GameState.Transit) ChangeSpeed(_slider.value);
        else if (_gameState == GameState.Arrival) _slider.value = _metroWagon.Speed;
    }

    public void ChangeSpeed(float targetSpeed) //Move to proper script 
    {
        if (_gameState == GameState.Transit)
        {
            _metroWagon.ChangeSpeed(targetSpeed);
        }
        _actualSpeed.value = _metroWagon.Speed;
    }

    public void ChangeState(GameState state)
    {
        _gameState = state;
        InvokeGameState();
    }

    public void Continue()
    {
        _gameState = GameState.Transit;
        InvokeGameState();
    }

    public void InvokeGameState()
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
}

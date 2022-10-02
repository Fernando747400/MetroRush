using Unity.VisualScripting.Dependencies.NCalc;
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
    [SerializeField] private GameObject _metroDoorsEntrance;
    [SerializeField] private GameObject _metroDoorsExit;
    [SerializeField] private GameObject _metroInside;

    public UnityEvent Arrived;
    public UnityEvent Transit;
    public UnityEvent OnStation;
    public UnityEvent GameOver;

    private MetroStation _currentStation;
    public enum GameState
    {
        Start,
        Transit,
        Arrival,
        OnStation,
    }
    public MetroStation CurrentStation { get => _currentStation; set => _currentStation = value; }
    public GameObject MetroDoorsEntrance { get => _metroDoorsEntrance;}
    public GameObject MetroDoorsExit { get => _metroDoorsExit; }
    public GameObject MetroInside { get => _metroInside;}

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
        if (_gameState == GameState.Transit || _gameState == GameState.Arrival) ChangeSpeed(_slider.value);
        else if (_gameState == GameState.OnStation) _slider.value = _metroWagon.Speed;
    }

    public void ChangeSpeed(float targetSpeed) //Move to proper script 
    {
         _metroWagon.ChangeSpeed(targetSpeed);
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
            case GameState.OnStation:
                OnStation?.Invoke();
                break;
        }
    }

    public bool CheckStationPassed(GameObject station)
    {
        if (_mapManager.StationList.IndexOf(station) < _mapManager.StationList.IndexOf(_currentStation.gameObject)) return true;
        else return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Xml.Serialization;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Dependencies")]
    [SerializeField] private GameObject _buyCapacityCanvas;
    [SerializeField] private TextMeshProUGUI _stationLabel;
    [SerializeField] private TextMeshProUGUI _stationText;
    [SerializeField] private TextMeshProUGUI _capacityText;
    [SerializeField] private TextMeshProUGUI _currencyText;
    [SerializeField] private Slider _distanceSlider;

    private float _currentMaxDistance;
    private float _currentDistance;
    private GameObject _metroWagon;
    private GameObject _currentStation;

    public UnityEvent ExpandCapacity;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Prepare();
    }

    private void Update()
    {
        UpdateSlider();
    }

    public void InvokeExpandCapacity()
    {    
        ExpandCapacity?.Invoke();
        HideBuyCapacityCanvas(); // Remove this once AD are implemented
    }

    private void Prepare()
    {
        MetroWagon.Instance.OnFullCapacity += ShowBuyCapacityCanvas;
        MetroWagon.Instance.OnPassengerChanged += UpdateMainUI;
        GameManager.Instance.Transit.AddListener(UpdateNextStationText);
        GameManager.Instance.Transit.AddListener(CalculateMaxDistance);
        GameManager.Instance.Transit.AddListener(UpdateCurrentStation);
        GameManager.Instance.Arrived.AddListener(UpdateArrivingAtText);
        GameManager.Instance.OnStation.AddListener(UpdateCurrentStopText);
        CurrencyManager.Instance.UpdatedCurrency += UpdateCurrency;
        this.ExpandCapacity.AddListener(UpdateCapacityText);
        UpdateMainUI();
        _metroWagon = MetroWagon.Instance.gameObject;
        _currentStation = MapManager.Instance.StationList[0];
    }

    private void ShowBuyCapacityCanvas()
    {
        _buyCapacityCanvas.SetActive(true);
    }

    public void HideBuyCapacityCanvas()
    {
        _buyCapacityCanvas.SetActive(false);
    }
    
    private void UpdateMainUI()
    {
        UpdateCapacityText();
    }

    private void UpdateCapacityText() //TODO call after ad is done
    {
        if (MetroWagon.Instance.CurrentCapacity == MetroWagon.Instance.MaxCapacity)
        {
            _capacityText.text = $"<color=red>{MetroWagon.Instance.CurrentCapacity}/{MetroWagon.Instance.MaxCapacity}";
        }
        else
        {
            _capacityText.text = MetroWagon.Instance.CurrentCapacity + "/" + MetroWagon.Instance.MaxCapacity;
        }
    }

    private void UpdateArrivingAtText()
    {
        _stationLabel.text = "Llegando a la estación:";
        _stationText.text = GameManager.Instance.CurrentStation.StationData.StationName;
        _distanceSlider.gameObject.SetActive(false);
    }

    private void UpdateCurrentStopText()
    {
        _stationLabel.text = "Estación actual:";
        _stationText.text = GameManager.Instance.CurrentStation.StationData.StationName;
    }

    private void UpdateNextStationText()
    {
        _stationLabel.text = "Próxima estación:";
        _stationText.text = MapManager.Instance.StationList[MapManager.Instance.StationList.IndexOf(GameManager.Instance.CurrentStation.gameObject)+1].name;
        _distanceSlider.gameObject.SetActive(true);
    }

    private void UpdateCurrency()
    {
        _currencyText.text = CurrencyManager.Instance.RunCurrency.ToString();
    }

    private void UpdateSlider()
    {
        CalculateDistance();
        _distanceSlider.value = Mathf.InverseLerp(15f,_currentMaxDistance-35f, _currentDistance);
    }

    private void CalculateDistance()
    {
        _currentDistance = Vector3.Distance(_currentStation.transform.position, _metroWagon.transform.position);
    }

    private void UpdateCurrentStation()
    {
        _currentStation = GameManager.Instance.CurrentStation.gameObject;
    }

    private void CalculateMaxDistance()
    {
        GameObject nextStation = MapManager.Instance.StationList[MapManager.Instance.StationList.IndexOf(GameManager.Instance.CurrentStation.gameObject) + 1];
        _currentMaxDistance = Vector3.Distance(GameManager.Instance.CurrentStation.gameObject.transform.position, nextStation.transform.position);
    }

    private void StopTimmers()
    {
        //TODO
    }
}

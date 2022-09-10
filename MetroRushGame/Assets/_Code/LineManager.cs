using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private MapManager _mapManager;

    public UnityEvent InStation;

    private MetroLineScriptable _metroLine;
    private List<GameObject> _stationList;

    private void Start()
    {
        _metroLine = _mapManager.MetroLine;
        _stationList = _mapManager.StationList;
    }

    public void ArrivedStation()
    {
        InStation?.Invoke();
    }
}

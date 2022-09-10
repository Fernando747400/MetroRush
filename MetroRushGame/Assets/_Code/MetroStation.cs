using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroStation : MonoBehaviour
{
    private MetroStationScriptable _stationData;

    public MetroStationScriptable StationData { get => _stationData; set => _stationData = value; }
}

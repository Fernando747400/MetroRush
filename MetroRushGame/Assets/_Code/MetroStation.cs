using System.Collections.Generic;
using UnityEngine;

public class MetroStation : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public List<GameObject> DeSpawnPoints;
    public List<GameObject> PeopleWaiting;

    private MetroStationScriptable _stationData;

    public MetroStationScriptable StationData { get => _stationData; set => _stationData = value; }

    public GameObject GetRandomSpawn()
    {
        return SpawnPoints[Random.Range(0,SpawnPoints.Count)];
    }
}

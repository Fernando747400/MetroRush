using System.Collections.Generic;
using UnityEngine;

public class MetroStation : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public List<GameObject> DeSpawnPoints;
    public List<GameObject> PeopleWaiting;

    private MetroStationScriptable _stationData;
    private Queue<GameObject> _deSpawnQueue = new Queue<GameObject>();
    private Queue<GameObject> _SpawnQueue = new Queue<GameObject>();

    public MetroStationScriptable StationData { get => _stationData; set => _stationData = value; }

    private void Start()
    {
        PoblateQueue();
    }

    public GameObject GetSpawn()
    {
        if (_SpawnQueue.Count > 0)
        {
            return _SpawnQueue.Dequeue();
        }
        else
        {
            return GetRandomSpawn();
        }
    }

    public GameObject GetDespawn()
    {
        if (_deSpawnQueue.Count > 0)
        {
            return _deSpawnQueue.Dequeue();
        }
        else
        {
            return GetRandomDespawn();
        }
    }
    
    private GameObject GetRandomSpawn()
    {
        return SpawnPoints[Random.Range(0,SpawnPoints.Count)];
    }

    private GameObject GetRandomDespawn()
    {
        return DeSpawnPoints[Random.Range(0, DeSpawnPoints.Count)];
    }


    private void PoblateQueue()
    {
        foreach (var spawn in SpawnPoints)
        {
            _SpawnQueue.Enqueue(spawn);
        }

        foreach (var deSpawn in DeSpawnPoints)
        {
            _deSpawnQueue.Enqueue(deSpawn);
        }
    }
}

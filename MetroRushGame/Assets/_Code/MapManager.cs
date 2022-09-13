using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _tunelPiece;
    [SerializeField] private GameObject _stationPiece;
    [SerializeField] private MetroLineScriptable _metroLine;
    [SerializeField] private PeopleManager _peopleManager;

    [Header("Settings")]
    [SerializeField] private float _mapOffset;
    [SerializeField] private float _timeMultiplier;

    private List<GameObject> _mapPiecesList = new List<GameObject>();
    private List<GameObject> _stationObjectList = new List<GameObject>();
    private List<MovingPiece> _movablePieceList = new List<MovingPiece>();

    public MetroLineScriptable MetroLine { get => _metroLine; }
    public List<GameObject> StationList { get => _stationObjectList; } 

    public void Start()
    {
        Initilization();
        BuildTunnel();
        AddPeople();
        BuildMovingPieceList();
    }

    public void ChangeSpeed(float Speed)
    {
        foreach (var movingPiece in _movablePieceList)
        {
            movingPiece.Speed = Speed;
        }
    }

    private void BuildTunnel()
    {
        Vector3 pos = Vector3.zero;
        foreach (var MapPiece in _mapPiecesList)
        {
            MapPiece.transform.position = pos;
            pos.z = pos.z + _mapOffset;
        }
    }

    private void Initilization()
    {
        if(_mapPiecesList.Any()) _mapPiecesList.Clear();

        foreach (var station in _metroLine.Stations)
        {
            for (int i = 0; i < station.StationDistance; i++)
            {
                GameObject tunnelPiece = GameObject.Instantiate(_tunelPiece);
                _mapPiecesList.Add(tunnelPiece);
            }

            GameObject stationPiece = GameObject.Instantiate(_stationPiece);
            stationPiece.GetComponent<MetroStation>().StationData = station;
            stationPiece.name = station.StationName;
            _mapPiecesList.Add(stationPiece);
            _stationObjectList.Add(stationPiece);
        }  
    }

    private void AddPeople()
    {
        foreach (var station in _stationObjectList)
        {
            if (station != _stationObjectList[_stationObjectList.Count-1]) //Checks to see if not the last piece
            {
                MetroStation metroStation = station.GetComponent<MetroStation>();
                GameObject desiredStation;
               
                foreach (var spawn in metroStation.SpawnPoints)
                {
                    do
                    {
                        desiredStation = _stationObjectList[Random.Range(_stationObjectList.IndexOf(station.gameObject), _stationObjectList.Count)]; //Select a station only from the current position forwards. AKA: can't get a lower station
                    } while (desiredStation == station.gameObject);

                    _peopleManager.InstantiatePeople(spawn, desiredStation, _stationObjectList.IndexOf(desiredStation) * _timeMultiplier, metroStation.PeopleWaiting);
                }
                
            }         
        }
    }

    private void BuildMovingPieceList()
    {
        foreach (var mapPiece in _mapPiecesList)
        {
            _movablePieceList.Add(mapPiece.gameObject.GetComponent<MovingPiece>());
        }
    }
}

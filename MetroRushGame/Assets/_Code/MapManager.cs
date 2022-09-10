using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

public class MapManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform _SpawnPoint;
    [SerializeField] private Transform _PoolingPoint;
    [SerializeField] private GameObject _TunelPiece;
    [SerializeField] private GameObject _StationPiece;
    [SerializeField] private MetroLineScriptable _metroLine;

    [Header("Settings")]
    [SerializeField] private float _PoolingOffset;

    private List<GameObject> _MapPieces = new List<GameObject>();
    private List<GameObject> _stationObject = new List<GameObject>();
    private List<MovingPiece> _MovingPiece = new List<MovingPiece>();

    public MetroLineScriptable MetroLine { get => _metroLine; }
    public List<GameObject> StationList { get => _stationObject; } 


    public void Awake()
    {
        PooligInitilization();
    }
    public void Start()
    {
        BuildTunnel();
        BuildMovingPieceList();
    }

    public void ChangeSpeed(float Speed)
    {
        foreach (var movingPiece in _MovingPiece)
        {
            movingPiece.speed = Speed;
        }
    }

    private void BuildTunnel()
    {
        Vector3 pos = Vector3.zero;
        foreach (var MapPiece in _MapPieces)
        {
            MapPiece.transform.position = pos;
            MapPiece.gameObject.GetComponent<MovingPiece>().PoolPosition = _PoolingPoint;
            pos.z = pos.z + _PoolingOffset;
        }
    }

    private void PooligInitilization()
    {
        _MapPieces.Clear();

        foreach (var station in _metroLine.Stations)
        {
            int i = 0;
            do
            {
                GameObject tunnelPiece = GameObject.Instantiate(_TunelPiece, _SpawnPoint);
                _MapPieces.Add(tunnelPiece);
                i++;
            } while (i < station.StationDistance);
            GameObject stationPiece = GameObject.Instantiate(_StationPiece, _SpawnPoint);
            stationPiece.AddComponent<MetroStation>();
            stationPiece.GetComponent<MetroStation>().StationData = station;
            _MapPieces.Add(stationPiece);
            _stationObject.Add(stationPiece);
        }  
    }

    private void BuildMovingPieceList()
    {
        foreach (var mapPiece in _MapPieces)
        {
            _MovingPiece.Add(mapPiece.gameObject.GetComponent<MovingPiece>());
        }
    }
}

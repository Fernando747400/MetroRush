using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void Awake()
    {
        PooligInitilization();
    }
    public void Start()
    {
        BuildTunnel();
    }

    public void ChangeSpeed(float Speed)
    {
        foreach (var mapPiece in _MapPieces)
        {
            mapPiece.gameObject.GetComponent<MovingPiece>().speed = Speed;
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
                _MapPieces.Add(GameObject.Instantiate(_TunelPiece, _SpawnPoint));
                i++;
            } while (i < station.StationDistance);
            _MapPieces.Add(GameObject.Instantiate(_StationPiece,_SpawnPoint));
        }
        //int i = 0;
        //do
        //{
        //    _MapPieces.Add(GameObject.Instantiate(_TunelPiece, _SpawnPoint));
        //    _MapPieces.Add(GameObject.Instantiate(_StationPiece, _SpawnPoint));
        //    i++;
        //} while (i < 10);    
    }
}

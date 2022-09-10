using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    [SerializeField] private GameObject _basePrefab;
    [SerializeField] private List<GameObject> _peoplePrefabs;

    public void InstantiatePeople(GameObject spawnPlace, GameObject desiredStation, float timeToDestination)
    {
        GameObject baseObject = GameObject.Instantiate(_basePrefab, spawnPlace.transform.position, Quaternion.identity, spawnPlace.transform);
        GameObject people = GameObject.Instantiate(RandomPeople(),baseObject.transform.position, Quaternion.identity, baseObject.transform);
        people.transform.rotation = Quaternion.Euler(0,-90f,0);
        baseObject.GetComponent<People>().DesiredStation = desiredStation;
        baseObject.GetComponent<People>().TimeLeft = timeToDestination;
        baseObject.GetComponent<People>().StationText.text = desiredStation.GetComponent<MetroStation>().StationData.StationName;
    }

    private GameObject RandomPeople()
    {
        return _peoplePrefabs[Random.Range(0,_peoplePrefabs.Count)];
    }
}

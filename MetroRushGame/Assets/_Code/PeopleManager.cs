using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _basePrefab;
    [SerializeField] private List<GameObject> _peoplePrefabs;

    private List<People> _peopleList = new List<People>();

    public List<People> PeopleList { get => _peopleList; set => _peopleList = value; }

    public void InstantiatePeople(GameObject spawnPlace, GameObject desiredStation, float timeToDestination, List<GameObject> waitingList)
    {
        GameObject baseObject = GameObject.Instantiate(_basePrefab, spawnPlace.transform.position, Quaternion.identity, spawnPlace.transform);
        GameObject peopleModel = GameObject.Instantiate(GetRandomPeople(),baseObject.transform.position, Quaternion.identity, baseObject.transform);
        peopleModel.transform.rotation = Quaternion.Euler(0,-90f,0);

        People basePeople = baseObject.GetComponent<People>();
        basePeople.DesiredStation = desiredStation;
        basePeople.TimeLeft = timeToDestination;
        basePeople.StationText.text = desiredStation.GetComponent<MetroStation>().StationData.StationName;

        AddPeopleToList(basePeople);
        waitingList.Add(baseObject);
    }

    public void AddPeopleToList(People people)
    {
        _peopleList.Add(people);
    }

    public List<People> RemovePassengers(List<People> peopleList)
    {
        List<People> peopleToRemove = new List<People>();
        foreach (var people in peopleList)
        {
            if (people.DesiredStation == GameManager.Instance.CurrentStation.gameObject || GameManager.Instance.CheckStationPassed(people.DesiredStation))
            {
                people.ExitMetro(GameManager.Instance.CurrentStation.GetDespawn());
                peopleToRemove.Add(people);
            }
        }

        foreach (var people in peopleToRemove)
        {
            peopleList.Remove(people);
        }
        
        return peopleList;
    }

    private GameObject GetRandomPeople()
    {
        return _peoplePrefabs[Random.Range(0,_peoplePrefabs.Count)];
    }  
}

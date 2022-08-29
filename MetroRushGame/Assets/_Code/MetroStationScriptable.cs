using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMetroStation", menuName = "Metro Scriptables/Station Scriptable")]
public class MetroStationScriptable : ScriptableObject
{
    public string StationName;
    public int StationDistance; 
}

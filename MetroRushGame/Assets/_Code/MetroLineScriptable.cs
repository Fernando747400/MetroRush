using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMetroLine", menuName = "Metro Scriptables/Line Scriptable")]
public class MetroLineScriptable : ScriptableObject
{
    public List<MetroStationScriptable> Stations;
}



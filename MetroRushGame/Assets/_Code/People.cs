using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class People : MonoBehaviour
{
    public float AnoyedLevel;
    public TextMeshPro StationText;
    public GameObject DesiredStation;
    public float TimeLeft;
    public bool TimeRunning;

    private void Start()
    {
        AnoyedLevel = 0f;
        TimeLeft = 20f;
        TimeRunning = false;
    }

    private void FixedUpdate()
    {
        if (TimeRunning && TimeLeft > 0)
        {
            TimeLeft = TimeLeft - Time.deltaTime;
            Mathf.Clamp(TimeLeft, 0, Mathf.Infinity);
        }
    }

    public void ChangeParent(GameObject newParent)
    {
        this.transform.parent = newParent.transform;
    }
}

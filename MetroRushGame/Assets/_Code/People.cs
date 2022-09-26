using TMPro;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class People : MonoBehaviour
{
    [Header("Dependencies")]
    public TextMeshPro StationText;
    public GameObject DesiredStation;

    [Header("Settings")]
    public float AnoyedLevel;
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

    private void OnMouseUpAsButton()
    {
        if (GameManager.Instance._gameState == GameManager.GameState.OnStation)
        {
            EnterMetro();
            ChangeParent(GameManager.Instance.MetroInside);
        }
    }

    public void EnterMetro()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameManager.Instance.MetroDoorsEntrance.transform.position, "time", 1f, "oncomplete", "MoveInside"));
    }

    public void MoveInside()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameManager.Instance.MetroInside.transform.position, "time", 1f));
    }

    public void ChangeParent(GameObject newParent)
    {
        this.transform.parent = newParent.transform;
        //this.transform.position = newParent.transform.position;
        //this.transform.rotation = newParent.transform.rotation;
    }
}

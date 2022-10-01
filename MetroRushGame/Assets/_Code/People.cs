using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
        RemoveColliders();
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
        TurnOffText();
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameManager.Instance.MetroDoorsEntrance.transform.position, "time", 1f, "oncomplete", "MoveInside"));
    }

    public void MoveInside()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameManager.Instance.MetroInside.transform.position, "time", 1f));
    }

    public void ExitMetro(GameObject exitPosition)
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameManager.Instance.MetroDoorsExit.transform.position, "time", 1f, "oncomplete", "MoveToDespawn", "oncompleteparams", exitPosition));
    }

    private void MoveToDespawn(GameObject position)
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", position.transform.position, "time", 1.5f));
        ChangeParent(position);

    }

    public void ChangeParent(GameObject newParent)
    {
        this.transform.parent = newParent.transform;
    }

    private void RemoveColliders (){
        List<Collider> colliders = new List<Collider>(this.gameObject.GetComponentsInChildren<Collider>());
        colliders.Remove(colliders[0]);
        
        foreach (Collider collider in colliders)
        {
            Destroy(collider);
        }
    }

    private void TurnOffText()
    {
        StationText.gameObject.SetActive(false);
    }

}

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
        if (GameManager.Instance._gameState == GameManager.GameState.Arrival)
        {
            this.gameObject.SetActive(false);
        }
        
    }

    public void ChangeParent(GameObject newParent)
    {
        this.transform.parent = newParent.transform;
        this.transform.position = newParent.transform.position;
        this.transform.rotation = newParent.transform.rotation;
    }
}

using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    private float _payout;

    private void Start()
    {
        TimeRunning = false;
        RemoveColliders();
        CalculateCurrency();
        StationText.text = "$" + $"<color=green>{_payout}</color>" + "\n" + StationText.text;
    }

    private void FixedUpdate()
    {
        if (TimeRunning && TimeLeft > 0)
        {
            TimeLeft = TimeLeft - 1 * Time.deltaTime;
            Mathf.Clamp(TimeLeft, 0, Mathf.Infinity);
        }
        else if (TimeLeft <= 0 && TimeRunning)
        {
            _payout = (_payout - (2 * Time.deltaTime));
        }
    }

    private void OnMouseUpAsButton()
    {
        if (GameManager.Instance._gameState == GameManager.GameState.OnStation)
        {
            if (!MetroWagon.Instance.TryAddPassenger(this)) return;
            EnterMetro();
            ChangeParent(GameManager.Instance.MetroInside);
            TurnOffText();          
        }
    }

    public void EnterMetro()
    {
        Destroy(this.gameObject.GetComponent<Collider>());
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameManager.Instance.MetroDoorsEntrance.transform.position, "time", 1f, "oncomplete", "MoveInside"));
    }

    public void MoveInside()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameManager.Instance.MetroInside.transform.position, "time", 1f));
        StartTimer();
    }

    public void ExitMetro(GameObject exitPosition)
    {
        PauseTimer();
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameManager.Instance.MetroDoorsExit.transform.position, "time", 1f, "oncomplete", "MoveToDespawn", "oncompleteparams", exitPosition));
    }

    private void MoveToDespawn(GameObject position)
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", position.transform.position, "time", 1.5f, "oncomplete", "ShowPayout"));
        ChangeParent(position);
        GivePayout();
    }

    private void ShowPayout()
    {
        StationText.gameObject.SetActive(true);
        StationText.text = "$" + $"<color=green>{Mathf.RoundToInt(_payout)}</color>";
        iTween.PunchScale(StationText.gameObject, new Vector3(0.1f, 0.1f, 0.1f), 1f);
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

    private void PauseTimer()
    {
        TimeRunning = false;
    }

    private void StartTimer()
    {
        TimeRunning = true;
    }

    private void CalculateCurrency()
    {
        _payout = (TimeLeft * 2);
    }

    private void GivePayout()
    {
        CurrencyManager.Instance.AddCurrency(Mathf.RoundToInt(_payout));       
    }

}

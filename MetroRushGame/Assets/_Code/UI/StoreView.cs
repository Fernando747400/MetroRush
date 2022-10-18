using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreView : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _basePosition;
    [SerializeField] private Canvas _baseCanvas;

    [Header("Motores")]
    [SerializeField] private GameObject _motorsPosition;
    [SerializeField] private Canvas _motorCanvas;

    [Header("Frenos")]
    [SerializeField] private GameObject _breaksPosition;
    [SerializeField] private Canvas _breakCanvas;

    [Header("Chasis")]
    [SerializeField] private GameObject _chasisPosition;
    [SerializeField] private Canvas _chasisCanvas;

    [Header("Capacidad")]
    [SerializeField] private GameObject _capacityPosition;
    [SerializeField] private Canvas _capacityCanvas;

    [Header("AC")]
    [SerializeField] private GameObject _acPosition;
    [SerializeField] private Canvas _acCanvas;

    public void ChangeCanvasAndCamera(string name)
    {

    }

    private void ChangeTo(GameObject positionObject)
    {
        iTween.MoveTo(_camera.gameObject, positionObject.transform.position, 2f);
        iTween.RotateTo(_camera.gameObject, positionObject.transform.eulerAngles, 2f);
    }

    private void ActivateCanvas(Canvas activeCanvas)
    {
        CloseAllCanvas();
        activeCanvas.gameObject.SetActive(true);
    }

    private void CloseAllCanvas()
    {
        _motorCanvas.gameObject.SetActive(false);
        _chasisCanvas.gameObject.SetActive(false);
        _breakCanvas.gameObject.SetActive(false);
        _capacityCanvas.gameObject.SetActive(false);
        _acCanvas.gameObject.SetActive(false);
    }
}

public struct StoreValues
{
    public GameObject PositionObject;
    public Canvas CanvasObject;
    public string Description;
}

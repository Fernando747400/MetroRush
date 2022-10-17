using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreView : MonoBehaviour
{
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

}

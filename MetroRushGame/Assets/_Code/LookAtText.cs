using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtText : MonoBehaviour
{
    private Camera _cameraToLook;

    private void Start()
    {
        _cameraToLook = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(_cameraToLook.transform);
        transform.rotation = Quaternion.LookRotation(_cameraToLook.transform.forward);
    }
}

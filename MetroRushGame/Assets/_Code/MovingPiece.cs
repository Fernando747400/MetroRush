using UnityEngine;

public class MovingPiece : MonoBehaviour
{
    [Header("Settings")]
    public float Speed;

    [SerializeField] private float _speedMultiplier = 10f;

    void Update()
    {
        this.transform.Translate(-this.transform.up * Speed * _speedMultiplier * Time.deltaTime, Space.Self);
    }
}

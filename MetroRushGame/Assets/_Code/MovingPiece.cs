using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPiece : MonoBehaviour
{
    private Rigidbody _rb;

    public float speed;
    public Transform PoolPosition;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        this.transform.Translate(-this.transform.up * speed * 10 * Time.deltaTime, Space.Self);
        //_rb.velocity = new Vector3(0,0, -(speed * 1000 * Time.deltaTime));
        //CheckPosition();
    }

    private void CheckPosition()
    {
        if(this.transform.position.z <= -0.5)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        this.transform.position = PoolPosition.position;
    }
}

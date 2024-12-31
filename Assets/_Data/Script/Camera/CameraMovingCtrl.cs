using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovingCtrl : PMono
{
    [SerializeField] protected float moveSpeed = 12f;

    protected virtual void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.parent.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontal);
        transform.parent.Translate(Vector3.forward * Time.deltaTime * moveSpeed * vertical);

    }
}

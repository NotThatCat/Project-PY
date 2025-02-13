using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovingCtrl : PMono
{
    [SerializeField] protected float moveSpeed = 12f;
    [SerializeField] protected float maxX = 30f;
    [SerializeField] protected float minX = -55f;
    [SerializeField] protected float maxZ = 41f;
    [SerializeField] protected float minZ = -40f;

    protected virtual void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.parent.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontal);
        transform.parent.Translate(Vector3.forward * Time.deltaTime * moveSpeed * vertical);
        this.FixPosition();

    }

    protected virtual void FixPosition()
    {
        Vector3 pos = transform.parent.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.parent.position = pos;
    }
}

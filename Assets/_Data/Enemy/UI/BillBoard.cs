using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BillBoard : PMono
{
    [SerializeField] protected Transform cam;

    protected virtual void FixedUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}

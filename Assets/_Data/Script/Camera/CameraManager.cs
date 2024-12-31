using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SaiSingleton<CameraManager>
{

    [SerializeField] protected CameraMovingCtrl camMoving;
    public CameraMovingCtrl CamMoving => camMoving;

    [SerializeField] protected bool toogleCamMoving = true;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCameraMoving();
    }

    protected virtual void LoadCameraMoving()
    {
        if (this.camMoving != null) return;
        this.camMoving = GetComponent<CameraMovingCtrl>();
        Debug.Log(transform.name + ": CameraMovingCtrl", gameObject);
    }

    public virtual void DisableMoving()
    {
        if(this.toogleCamMoving)
        {
            this.toogleCamMoving = false;
        }
        this.camMoving.gameObject.SetActive(false);
    }

    public virtual void EnableMoving()
    {
        if(!this.toogleCamMoving)
        {
            this.toogleCamMoving = true;
        }
        this.camMoving.gameObject.SetActive(false);
    }
}

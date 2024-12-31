using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCtrl : SaiSingleton<PlayerCtrl>
{
    [SerializeField] protected CrosshairPointer crosshairPointer;
    public CrosshairPointer CrosshairPointer => crosshairPointer;

    protected override void LoadComponents()
    {

        this.LoadCrosshairPointer();
    }

    protected virtual void LoadCrosshairPointer()
    {
        if (this.crosshairPointer != null) return;
        this.crosshairPointer = GetComponentInChildren<CrosshairPointer>();
        Debug.Log(transform.name + ": LoadCrosshairPointer", gameObject);
    }
}

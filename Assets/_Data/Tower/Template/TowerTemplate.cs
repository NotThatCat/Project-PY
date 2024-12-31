using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class TowerTemplate : PMono
{
    public TowerCode towerCode = TowerCode.NoTower;
    public int price = 50;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTower();
    }

    protected virtual void LoadTower()
    {
        if (Enum.TryParse(transform.name, out TowerCode code))
        {
            this.towerCode = code;
        }
        else
        {
            Debug.Log("Tower not found, please update transform name");
        }
    }

    public virtual int GetPrice()
    {
        return this.price;
    }
}

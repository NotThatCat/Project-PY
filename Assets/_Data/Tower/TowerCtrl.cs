using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerCtrl : PoolObj
{
    [SerializeField] protected TowerCode code;
    public TowerCode Code => code;
    [SerializeField] protected TowerRadar radar;
    public TowerRadar Radar => radar;

    [SerializeField] protected Transform rotator;
    public Transform Rotator => rotator;

    [SerializeField] protected TowerShooting towerShooting;
    public TowerShooting TowerShooting => towerShooting;

    [SerializeField] protected LevelAbstract level;
    public LevelAbstract Level => level;

    [SerializeField] protected int salePrice;
    public int SalePrice => salePrice;

    [SerializeField] protected TowerPlaceAble towerPlaceAble;
    public TowerPlaceAble TowerPlaceAble => towerPlaceAble;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRadar();
        this.LoadRotator();
        this.LoadTowerShootings();
        this.LoadLevel();
        this.LoadCode();
    }

    private void LoadCode()
    {
        Enum.TryParse(this.GetName(), out TowerCode code);
        this.code = code;
    }

    protected virtual void LoadRadar()
    {
        if (this.radar != null) return;
        this.radar = GetComponentInChildren<TowerRadar>();
        //Debug.Log(transform.name + ": LoadRadar", gameObject);
    }

    protected virtual void LoadRotator()
    {
        if (this.rotator != null) return;
        this.rotator = transform.Find("Model").Find("Rotator");
        //Debug.Log(transform.name + ": LoadRotator", gameObject);
    }

    protected virtual void LoadTowerShootings()
    {
        if (this.towerShooting != null) return;
        this.towerShooting = GetComponentInChildren<TowerShooting>();
        //Debug.Log(transform.name + ": LoadTowerShootings", gameObject);
    }
    
    protected virtual void LoadLevel()
    {
        if (this.level != null) return;
        this.level = GetComponentInChildren<LevelAbstract>();
        //Debug.Log(transform.name + ": LoadLevel", gameObject);
    }

    public virtual void SetPlaceAble(TowerPlaceAble placeAble)
    {
        this.towerPlaceAble = placeAble;
    }
}

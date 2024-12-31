using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlaceAble : PlayerInteractAble
{

    [SerializeField] protected TowerCtrl towerCtrl;
    public TowerCtrl TowerCtrl => towerCtrl;

    public bool canPlaceTower = true;

    public virtual void SetTower(TowerCtrl towerCtrl)
    {
        if (!this.canPlaceTower) return;
        this.canPlaceTower = false;
        this.towerCtrl = towerCtrl;
    }

    public virtual void RemoveTower()
    {
        if (this.canPlaceTower) return;
        this.canPlaceTower = true;
        int gold = towerCtrl.SalePrice;
        this.towerCtrl.Despawn.DoDespawn();
    }

    public virtual bool CanPlace()
    {
        return canPlaceTower;
    }

    public override void MouseInteract()
    {
        if(this.towerCtrl != null)
        {
            this.Interact();
        }
    }

    public override void Interact()
    {
        if (this.towerCtrl != null)
        {
            UIUpgradeTower.Instance.ShowUI();
        }
    }

    public override void UnInteract()
    {
        //if (this.towerCtrl != null)
        //{
        //    UIUpgradeTower.Instance.HideUI();
        //}
        //UITowerPlaceAble.Instance.HideUITowerInteract(this.CanPlace, this.towerCtrl, this.transform.position);
    }
}

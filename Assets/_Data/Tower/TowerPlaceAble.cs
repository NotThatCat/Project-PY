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
        this.canPlaceTower = false;
        this.towerCtrl = towerCtrl;
    }

    public virtual void RemoveTower()
    {
        if (this.canPlaceTower) return;
        this.canPlaceTower = true;
        this.towerCtrl.Despawn.DoDespawn();
        this.towerCtrl = null;
    }

    public virtual bool CanPlace()
    {
        return canPlaceTower;
    }

    public override void MouseInteract()
    {
        this.Interact();
    }

    public override void Interact()
    {
        if (this.towerCtrl != null)
        {
            UIUpgradeTower.Instance.ShowUI();
        }
        else
        {
            TowerManager.Instance.TryPlaceTower();
        }
    }

    public override void UnInteract()
    {
        //if (this.towerCtrl != null && !UIUpgradeTower.Instance.IsActive)
        //{
        //    UIUpgradeTower.Instance.HideUI();
        //}
    }
}

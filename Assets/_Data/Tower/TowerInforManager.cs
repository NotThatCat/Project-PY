using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Linq;

public class TowerInforManager : PMono
{
    [SerializeField] protected List<TowerInfor> towerInfors = new();

    public virtual int GetTowerPrice(TowerCode code)
    {
        foreach (TowerInfor info in towerInfors)
        {
            if (info.towerCode == code) return info.price;
        }
        return 0;
    }

    public virtual int GetUpgradePrice(TowerCode code)
    {
        TowerCode upgradeCode = this.GetUpgrade(code);
        if (upgradeCode != TowerCode.NoTower)
        {
            return this.GetTowerPrice(upgradeCode);
        }

        return -1;
    }


    public virtual int GetTowerSalePrice(TowerCode code)
    {
        int price = 0;
        foreach (TowerInfor info in towerInfors)
        {
            if (info.towerCode == code)
            {
                price = info.price * 2 / 3;
            }
        }
        return price;
    }

    public virtual TowerCode GetUpgrade(TowerCode code)
    {
        foreach (TowerInfor info in towerInfors)
        {
            if (info.towerCode == code)
            {
                return info.nextUpgrade;
            }
        }
        return TowerCode.NoTower;
    }

    public List<TowerCode> GetAllTowerCodes()
    {
        return Enum.GetValues(typeof(TowerCode)).Cast<TowerCode>().ToList();
    }
}

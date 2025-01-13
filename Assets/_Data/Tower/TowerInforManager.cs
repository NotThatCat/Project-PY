using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
}

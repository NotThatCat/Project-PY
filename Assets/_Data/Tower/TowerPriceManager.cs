using System.Collections.Generic;
using UnityEngine;

public class TowerPriceManager : PMono
{
    [SerializeField] protected List<TowerPrice> priceHolder = new();

    public virtual int GetTowerPrice(TowerCode code)
    {
        foreach(TowerPrice towerPrices in priceHolder)
        {
            if (towerPrices.towerCode == code) return towerPrices.price;
        }
        return 0;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ButtonDeleteTower : ButtonInteractTower
{
    [SerializeField] protected int SalePrice = 0;

    public override void OnClick()
    {
        this.placeAble.RemoveTower();
        InventoriesManager.Instance.AddItem(ItemCode.Gold, this.SalePrice);
        UIUpgradeTower.Instance.HideUI();
    }

    public override void UpdateInfo()
    {
        PlayerInteractAble playerInteractAble = InputManager.Instance.CurrentInteractAble;
        this.placeAble = playerInteractAble.transform.GetComponent<TowerPlaceAble>();
        if (placeAble != null)
        {
            this.towerCtrl = placeAble.TowerCtrl;
        }

        this.SalePrice = TowerManager.Instance.TowerPriceManager.GetTowerSalePrice(this.towerCtrl.Code);
        this.textPrice.LoadPrice(this.SalePrice.ToString());
    }
}

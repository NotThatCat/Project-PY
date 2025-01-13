using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ButtonUpgradeTower : ButtonInteractTower
{
    [SerializeField] protected int upgradePrice = 0;
    [SerializeField] protected bool canBuyTower = false;

    public override void OnClick()
    {
        //Debug.Log(transform.name + "OnClick");
        if (!this.canBuyTower) return;
        TowerManager.Instance.UpgradeTower(this.towerCtrl, this.placeAble);

        InventoriesManager.Instance.RemoveItem(ItemCode.Gold, upgradePrice);
        UIUpgradeTower.Instance.HideUI();
    }

    public override void UpdateInfo()
    {
        PlayerInteractAble playerInteractAble = InputManager.Instance.CurrentInteractAble;
        this.placeAble = playerInteractAble.transform.GetComponent<TowerPlaceAble>();
        //Debug.Log(transform.name + "UpdateInfo");
        if (this.placeAble != null)
        {
            this.towerCtrl = this.placeAble.TowerCtrl;
            this.upgradePrice = TowerManager.Instance.TowerPriceManager.GetTowerPrice(this.towerCtrl.Code);
            this.textPrice.LoadPrice(this.upgradePrice.ToString());
        }

    }

    protected virtual void FixedUpdate()
    {
        this.UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        if (canBuyTower != TowerManager.Instance.CanBuyTower(this.towerCtrl.Code))
        {
            this.ToogleBG();
        }
    }

    protected virtual void ToogleBG()
    {
        this.canBuyTower = !this.canBuyTower;
        this.BGRed.gameObject.SetActive(!canBuyTower);
        this.BGBlue.gameObject.SetActive(canBuyTower);
    }
}

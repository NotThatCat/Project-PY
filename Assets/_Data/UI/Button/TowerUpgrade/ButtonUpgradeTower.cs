using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ButtonUpgradeTower : ButtonInteractTower
{
    [SerializeField] protected int upgradePrice = 0;
    [SerializeField] protected bool canBuyTower = false;
    [SerializeField] protected bool isMaxed = false;

    public override void OnClick()
    {
        //Debug.Log(transform.name + "OnClick");
        if (!this.canBuyTower && this.isMaxed) return;
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
            //TowerCode upgradeCode = TowerManager.Instance.TowerPriceManager.GetUpgrade(this.placeAble.TowerCtrl.Code);
            this.upgradePrice = TowerManager.Instance.TowerPriceManager.GetUpgradePrice(this.placeAble.TowerCtrl.Code);
            if (this.upgradePrice < 0)
            {
                this.isMaxed = true;
                this.textPrice.LoadPrice("Maxed");
            }
            else
            {
                this.textPrice.LoadPrice(this.upgradePrice.ToString());
                this.isMaxed = false;
            }
        }

    }

    protected virtual void FixedUpdate()
    {
        this.UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        if (canBuyTower != TowerManager.Instance.CanBuyUpgrade(this.towerCtrl.Code))
        {
            this.ToogleBG();
        }
    }

    protected virtual void ToogleBG()
    {
        if (this.isMaxed)
        {
            this.BGBlue.gameObject.SetActive(this.isMaxed);
            this.BGRed.gameObject.SetActive(!this.isMaxed);
        }
        else
        {
            this.canBuyTower = !this.canBuyTower;
            this.BGRed.gameObject.SetActive(!canBuyTower);
            this.BGBlue.gameObject.SetActive(canBuyTower);
        }
    }
}

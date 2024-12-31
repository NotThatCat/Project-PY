using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShopItem : ButtonAbstact
{
    [SerializeField] protected UIShopCtrl shopCtrl;

    [ProButton]
    public virtual void AddTest()
    {
        this.button.onClick.AddListener(this.OnClick);
    }

    protected override void LoadComponents()
    {
        this.LoadShopCtrl();
    }

    protected virtual void LoadShopCtrl()
    {
        if (this.shopCtrl != null) return;
        this.shopCtrl = transform.GetComponent<UIShopCtrl>();
    }

    public override void OnClick()
    {
        //Debug.Log("ButtonInventory");
        //UIInventoryManager uIShopManager = GameObject.FindObjectOfType<UIInventoryManager>();
        ItemCode priceType = this.shopCtrl.priceType;
        int shopPrice = this.shopCtrl.ItemInventory.ItemProfile.basePrice;
        int playerOwn = InventoriesManager.Instance.GetItem(priceType).itemCount;
        if (playerOwn > shopPrice)
        {
            InventoriesManager.Instance.RemoveItem(priceType, shopPrice);
            InventoriesManager.Instance.AddItem(this.shopCtrl.ItemInventory.ItemProfile.itemCode, this.shopCtrl.ItemInventory.itemCount);
            ShopManager.Instance.RemoveItem(this.shopCtrl.ItemInventory);
        }
        else
        {
            Debug.Log("Not enough money");
        }

    }
}

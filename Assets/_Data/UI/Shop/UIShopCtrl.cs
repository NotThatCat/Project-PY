using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIShopCtrl : UIInventoryPoolObj
{
    [SerializeField] protected TextItemPrice _itemPrice;
    public TextItemPrice ItemPrice => _itemPrice;
    public ItemCode priceType = ItemCode.Gold;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemPrice();
    }

    public override void Init(ItemInventory itemInventory)
    {
        base.Init(itemInventory);
        this._itemPrice.LoadItemPrice(itemInventory.ItemProfile.basePrice.ToString());
        if(itemInventory.itemCount <= 1 )
        {
            this._itemCount.LoadItemCount("");
        }
    }

    private void LoadItemPrice()
    {
        this._itemPrice = transform.Find("Slot_Item").GetComponentInChildren<TextItemPrice>();
    }

    public override string GetName()
    {
        return "UIShopCtrl";
    }
}

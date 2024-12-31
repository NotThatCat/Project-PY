using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : SaiSingleton<ShopManager>
{
    [SerializeField] protected InventoryCtrl shopInventory;
    [SerializeField] protected List<ItemProfileSO> itemProfiles;

    //[SerializeField]
    protected override void LoadComponents()
    {
        this.LoadInventory();
        this.LoadItemProfiles();
    }

    public virtual void GenerateShopItem(ItemCode availableItems, int shopMaxStock, bool canDuplicate)
    {

    }

    protected virtual void LoadInventory()
    {

        this.shopInventory = transform.GetComponentInChildren<InventoryCtrl>();
    }

    protected virtual void LoadItemProfiles()
    {
        if (this.itemProfiles.Count > 0) return;
        ItemProfileSO[] itemProfileSOs = Resources.LoadAll<ItemProfileSO>("/");
        this.itemProfiles = new List<ItemProfileSO>(itemProfileSOs);
        //Debug.Log(transform.name + ": LoadItemProfiles", gameObject);
    }

    public virtual ItemProfileSO GetProfileByCode(ItemCode itemCodeName)
    {
        foreach (ItemProfileSO itemProfile in this.itemProfiles)
        {
            if (itemProfile.itemCode == itemCodeName) return itemProfile;
        }

        return null;
    }

    public virtual InventoryCtrl GetByCodeName(InventoryType inventoryType)
    {
        return shopInventory;
    }

    public virtual void AddItem(ItemInventory itemInventory)
    {
        InventoryType invCodeName = itemInventory.ItemProfile.inventoryType;
        InventoryCtrl inventoryCtrl = this.GetByCodeName(invCodeName);
        inventoryCtrl.AddItem(itemInventory);
    }

    public virtual void AddItem(ItemCode itemCode, int itemCount)
    {
        ItemProfileSO itemProfile = this.GetProfileByCode(itemCode);
        ItemInventory item = new(itemProfile, itemCount);
        this.AddItem(item);
    }

    public virtual void RemoveItem(ItemCode itemCode, int itemCount)
    {
        ItemProfileSO itemProfile = this.GetProfileByCode(itemCode);
        ItemInventory item = new(itemProfile, itemCount);
        this.RemoveItem(item);
    }

    public virtual void RemoveItem(ItemInventory itemInventory)
    {
        InventoryType inventoryType = itemInventory.ItemProfile.inventoryType;
        InventoryCtrl inventoryCtrl = this.GetByCodeName(inventoryType);
        inventoryCtrl.RemoveItem(itemInventory);
    }

    internal InventoryCtrl GetInventory()
    {
        return this.shopInventory;
    }

    //public virtual void AddStock(ItemCode itemCode, int stock)
    //{
    //    InventoryShopCtrl stockExist = this.FindItem(itemCode);
    //    ItemProfileSO itemProfileSO = this.GetProfileByCode(itemCode);
    //    if (!itemProfileSO.isStackable || stockExist == null)
    //    {
    //        stockExist = new InventoryShopCtrl(itemProfileSO, stock);
    //        stockExist.SetId(Random.Range(0, 999999999));
    //        this.shopInventory.Add(stockExist);
    //        return;
    //    }

    //    stockExist.stock += stock;
    //}

    //public virtual bool RemoveStock(ItemCode itemCode, int stock)
    //{
    //    InventoryShopCtrl stockExist = this.FindItemNotEmpty(itemCode);
    //    if (stockExist == null) return false;
    //    if (!stockExist.CanDeduct(stock)) return false;
    //    stockExist.Deduct(stock);
    //    if (stockExist.stock == 0) this.shopInventory.Remove(stockExist);
    //    return true;
    //}

    //public virtual InventoryShopCtrl FindItemNotEmpty(ItemCode itemCode)
    //{
    //    foreach (InventoryShopCtrl shopInventory in this.shopInventory)
    //    {
    //        if (shopInventory.ItemProfile.itemCode != itemCode) continue;
    //        if (shopInventory.stock > 0) return shopInventory;
    //    }

    //    return null;
    //}

    //public virtual InventoryShopCtrl FindItem(ItemCode itemCode)
    //{
    //    foreach (InventoryShopCtrl shopInventory in this.shopInventory)
    //    {
    //        if (shopInventory.ItemProfile.itemCode == itemCode) return shopInventory;
    //    }

    //    return null;
    //}

}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InventoriesManager : SaiSingleton<InventoriesManager>
{
    [SerializeField] protected List<InventoryCtrl> inventories;
    [SerializeField] protected List<ItemProfileSO> itemProfiles;

    [SerializeField] protected int startUpGold = 100;

    public event Action<int> OnGoldChanged;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Init()
    {
        InventoriesManager.Instance.AddItem(ItemCode.Gold, this.startUpGold);
    }

    protected virtual void Start()
    {

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventories();
        this.LoadItemProfiles();
    }

    protected virtual void LoadInventories()
    {
        if (this.inventories.Count > 0) return;
        foreach (Transform child in transform)
        {
            InventoryCtrl inventoryCtrl = child.GetComponent<InventoryCtrl>();
            if (inventoryCtrl == null) continue;
            this.inventories.Add(inventoryCtrl);
        }
        //Debug.Log(transform.name + ": LoadInventories", gameObject);
    }

    protected virtual void LoadItemProfiles()
    {
        if (this.itemProfiles.Count > 0) return;
        ItemProfileSO[] itemProfileSOs = Resources.LoadAll<ItemProfileSO>("/");
        this.itemProfiles = new List<ItemProfileSO>(itemProfileSOs);
        //Debug.Log(transform.name + ": LoadItemProfiles", gameObject);
    }

    public virtual InventoryCtrl GetByCodeName(InventoryType inventoryType)
    {
        foreach (InventoryCtrl inventory in this.inventories)
        {
            if (inventory.GetName() == inventoryType) return inventory;
        }

        return null;
    }

    public virtual ItemProfileSO GetProfileByCode(ItemCode itemCodeName)
    {
        foreach (ItemProfileSO itemProfile in this.itemProfiles)
        {
            if (itemProfile.itemCode == itemCodeName) return itemProfile;
        }

        return null;
    }

    public virtual InventoryCtrl Currency()
    {
        return this.GetByCodeName(InventoryType.Currency);
    }

    public virtual InventoryCtrl Item()
    {
        return this.GetByCodeName(InventoryType.Item);
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
        if(itemProfile.itemCode == ItemCode.Gold)
        {
            OnGoldChanged?.Invoke(this.GetPlayerGold());
        }
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

    public List<InventoryCtrl> GetInventories()
    {
        return this.inventories;
    }

    public virtual ItemInventory GetItem(ItemCode itemCode)
    {
        ItemProfileSO itemProfile = this.GetProfileByCode(itemCode);
        InventoryType invCodeName = itemProfile.inventoryType;
        InventoryCtrl inventoryCtrl = this.GetByCodeName(invCodeName);
        return inventoryCtrl.FindItem(itemCode);
    }

    public virtual int GetPlayerGold()
    {
        ItemInventory item = this.Currency().FindItem(ItemCode.Gold);
        int goldCount = item == null ? 0 : item.itemCount;
        return goldCount;
    }
}

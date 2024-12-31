using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryManager : PMono
{
    [SerializeField] protected UIInventorySpawner spawner;
    [SerializeField] protected UIInventoryCtrl uiInvetory;
    [SerializeField] protected Transform spawnPoint;

    protected override void Awake()
    {
        base.Awake();
        this.UpdateInventory();
    }

    protected override void LoadComponents()
    {
        this.LoadSpawner();
        this.LoadSpawnPoint();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.GetComponentInChildren<UIInventorySpawner>();
        //Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void LoadSpawnPoint()
    {
        if (this.spawnPoint != null) return;
        this.spawnPoint = transform.Find("InventoryScroll").Find("Holder");
        //Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void LoadHolder()
    {
        if (this.spawnPoint != null) return;
        this.spawnPoint = transform.Find("InventoryScroll").Find("Holder");
        //Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }

    public virtual void UpdateInventory()
    {
        this.ClearInventory();
        List<InventoryCtrl> inventoryCtrls = InventoriesManager.Instance.GetInventories();
        foreach (InventoryCtrl inventoryCtrl in inventoryCtrls)
        {
            Debug.Log("inventoryCtrl " + inventoryCtrl);
            if (inventoryCtrl != null)
            {
                foreach(ItemInventory itemInventory in inventoryCtrl.Items)
                {
                    this.Spawn(itemInventory);
                }
            }
        }
    }
    
    protected virtual void Spawn(ItemInventory itemInventory)
    {
        UIInventoryCtrl newUIInventoryCtrl = spawner.Spawn(this.uiInvetory, spawnPoint.position);
        newUIInventoryCtrl.Init(itemInventory);
        newUIInventoryCtrl.gameObject.SetActive(true);
    }

    public virtual void OnDisable()
    {
        this.ClearInventory();
    }

    protected virtual void ClearInventory()
    {
        UIInventoryCtrl[] uIInventoryCtrls = transform.GetComponentsInChildren<UIInventoryCtrl>();
        foreach (UIInventoryCtrl uIInventoryCtrl in uIInventoryCtrls)
        {
            uIInventoryCtrl.DoDespawn();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class TowerManager : SaiSingleton<TowerManager>
{
    [SerializeField] protected TowerCode selectedTower = TowerCode.NoTower;
    [SerializeField] protected TowerCtrl towerPrefab;
    [SerializeField] protected LayerMask layerMask = 6;
    [SerializeField] protected float maxDistance = 100f;
    [SerializeField] protected bool towerPlaced = false;
    [SerializeField] protected TowerPlaceAble selectedPlaceAble;
    [SerializeField] protected Transform pointer;
    [SerializeField] protected List<TowerTemplate> towerTemplates = new();
    [SerializeField] protected TowerPriceManager towerPriceManager;

    protected override void Awake()
    {
        base.Awake();
        this.HideTemplate();
    }

    protected virtual void HideTemplate()
    {
        foreach (TowerTemplate template in this.towerTemplates)
        {
            template.gameObject.SetActive(false);
        }
    }

    protected virtual void LoadPointer()
    {
        if (this.pointer != null) return;
        this.pointer = transform.Find("Pointer");
    }

    protected virtual void ShowTemplate(TowerCode towerCode)
    {
        foreach (TowerTemplate template in this.towerTemplates)
        {
            if (template.transform.name == towerCode.ToString())
            {
                template.transform.position = selectedPlaceAble.GetPosition();
                template.gameObject.SetActive(true);
            }
            else
            {
                template.gameObject.SetActive(false);
            }
        }
    }

    protected virtual TowerTemplate GetTowerByCode(TowerCode towerCode)
    {
        foreach (TowerTemplate template in this.towerTemplates)
        {
            if (template.towerCode == towerCode) return template;
        }
        return null;
    }

    public virtual int GetTowerPrice(TowerCode towerCode)
    {
        return (this.towerPriceManager.GetTowerPrice(towerCode));
    }

    protected virtual bool CanBuyTower()
    {
        return this.CanBuyTower(this.selectedTower);
    }

    public virtual bool CanBuyTower(TowerCode towerCode)
    {
        ItemInventory item = InventoriesManager.Instance.Currency().FindItem(ItemCode.Gold);
        int goldCount = item == null ? 0 : item.itemCount;
        return goldCount >= this.GetTowerPrice(towerCode) ? true : false;
    }

    protected virtual void FixedUpdate()
    {
        this.GetCurrentPlaceAble();
        this.ShowTowerToPlace();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTowerTemplates();
        this.LoadPointer();
        this.LoadTowerPriceManager();
    }

    private void LoadTowerTemplates()
    {
        if (this.towerTemplates.Count > 0) return;
        this.towerTemplates = transform.Find("Templates").GetComponentsInChildren<TowerTemplate>().ToList<TowerTemplate>();
    }

    private void LoadTowerPriceManager()
    {
        if (this.towerPriceManager != null) return;
        this.towerPriceManager = transform.GetComponent<TowerPriceManager>();
    }

    protected virtual void GetCurrentPlaceAble()
    {
        if (this.towerPlaced) return;
        this.selectedTower = MapKeyCodeToTowerCode(InputHotkeys.Instance.KeyCode);
        if (InputHotkeys.Instance.PlayerInteractAble)
        {
            this.selectedPlaceAble = InputHotkeys.Instance.PlayerInteractAble.transform.GetComponent<TowerPlaceAble>();
        }
        else
        {
            this.selectedPlaceAble = null;
        }

        //Vector3 mousePosition = Input.mousePosition;
        //Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        //if (Physics.Raycast(ray, out RaycastHit hitInfo, this.maxDistance, layerMask))
        //{
        //    TowerPlaceAble placeAble = hitInfo.collider.transform.parent.GetComponent<TowerPlaceAble>();

        //    this.selectedPlaceAble = placeAble;

        //    Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
        //}
        //else
        //{
        //    this.selectedPlaceAble = null;
        //}
        this.ShowTowerToPlace();
    }

    public virtual void PlaceTower(TowerCode towerCode)
    {
        this.selectedTower = towerCode;
        this.towerPlaced = false;
    }

    protected virtual void BuyTower()
    {
        InventoriesManager.Instance.RemoveItem(ItemCode.Gold, this.GetTowerPrice(this.selectedTower));
    }

    protected virtual void ShowTowerToPlace()
    {
        if (this.towerPlaced) return;
        this.selectedTower = MapKeyCodeToTowerCode(InputHotkeys.Instance.KeyCode);
        if (
            this.selectedPlaceAble == null ||
            !this.selectedPlaceAble.CanPlace() ||
            this.selectedTower == TowerCode.NoTower ||
            !this.CanBuyTower())
        {
            //Hide template
            this.HideTemplate();
            return;
        }

        this.pointer.position = this.selectedPlaceAble.GetPosition();
        this.ShowTemplate(this.selectedTower);
        if (InputHotkeys.Instance.IsPlaceTower) this.PlaceTower();
    }

    protected virtual void PlaceTower()
    {
        this.towerPlaced = true;
        this.HideTemplate();
        Invoke(nameof(this.PlaceFinish), 0.3f);
    }

    protected virtual void PlaceFinish()
    {
        if (!this.selectedPlaceAble.CanPlace()) return;
        if (!this.CanBuyTower())
        {
            EffectCtrl prefabRed = EffectSpawnerCtrl.Instance.Prefabs.GetByName(EffectCode.Place2.ToString());
            EffectCtrl newEfffectRed = EffectSpawnerCtrl.Instance.Spawner.Spawn(prefabRed, this.selectedPlaceAble.GetPosition(), transform.rotation);
            newEfffectRed.gameObject.SetActive(true);
            this.towerPlaced = false;
            return;
        }

        TowerCtrl newTower = this.Spawn(this.GetSelectedTower());
        this.selectedPlaceAble.SetTower(newTower);
        newTower.TowerShooting.Active();
        newTower.SetActive(true);

        EffectCtrl prefab = EffectSpawnerCtrl.Instance.Prefabs.GetByName(EffectCode.Place1.ToString());
        EffectCtrl newEfffect = EffectSpawnerCtrl.Instance.Spawner.Spawn(prefab, this.selectedPlaceAble.GetPosition(), transform.rotation);
        newEfffect.gameObject.SetActive(true);
        this.towerPlaced = false;

        this.BuyTower();
    }

    protected virtual TowerCtrl GetTowerPrefab(TowerCode towerCode)
    {
        return TowerSpawnerCtrl.Instance.Prefabs.GetByName(towerCode.ToString());
    }

    protected virtual TowerCtrl Spawn(TowerCtrl prefab)
    {
        return TowerSpawnerCtrl.Instance.Spawner.Spawn(prefab, this.selectedPlaceAble.GetPosition());
    }

    protected virtual TowerCode MapKeyCodeToTowerCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Alpha1: return TowerCode.MachineGun_1;
            case KeyCode.Alpha2: return TowerCode.CanonGun_1;
            default: return TowerCode.NoTower;
        }
    }

    protected virtual TowerCtrl GetSelectedTower()
    {
        return TowerSpawnerCtrl.Instance.Prefabs.GetByName(this.selectedTower.ToString());
    }

    public virtual void UpgradeTower(TowerCtrl towerCtrl)
    {
        throw new NotImplementedException();
    }
}

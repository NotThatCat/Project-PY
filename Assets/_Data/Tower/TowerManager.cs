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
    [SerializeField] protected bool towerPlaced = false;
    [SerializeField] protected TowerPlaceAble selectedPlaceAble;
    [SerializeField] protected Transform pointer;
    [SerializeField] protected List<TowerTemplate> towerTemplates = new();
    [SerializeField] protected TowerInforManager towerInfoManager;
    [SerializeField] protected Dictionary<TowerCode, bool> CanBuyTower = new Dictionary<TowerCode, bool>();
    public TowerInforManager TowerPriceManager => towerInfoManager;

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
        return (this.towerInfoManager.GetTowerPrice(towerCode));
    }

    protected virtual bool CanBuySelectedTower()
    {
        return this.CanBuyTowerByCode(this.selectedTower);
    }

    public virtual bool CanBuyTowerByCode(TowerCode towerCode)
    {
        this.UpdateCanBuyTower();
        if (towerCode == TowerCode.NoTower) return true;
        return CanBuyTower[towerCode];
    }

    public virtual bool CanBuyUpgrade(TowerCode towerCode)
    {
        TowerCode upgradeCode = TowerManager.Instance.TowerPriceManager.GetUpgrade(towerCode);
        return this.CanBuyTowerByCode(upgradeCode);
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
        if (this.towerInfoManager != null) return;
        this.towerInfoManager = transform.GetComponent<TowerInforManager>();
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

        //this.ShowTowerToPlace();
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
        if (!this.CanPlaceTower()) return;

        this.pointer.position = this.selectedPlaceAble.GetPosition();
        this.ShowTemplate(this.selectedTower);
        if (InputHotkeys.Instance.IsPlaceTower) this.PlaceTower();
    }

    protected virtual bool CanPlaceTower()
    {
        if (this.towerPlaced) return false;
        this.selectedTower = MapKeyCodeToTowerCode(InputHotkeys.Instance.KeyCode);
        if (
            this.selectedPlaceAble == null ||
            !this.selectedPlaceAble.CanPlace() ||
            this.selectedTower == TowerCode.NoTower ||
            !this.CanBuySelectedTower())
        {
            //Hide template
            this.HideTemplate();
            return false;
        }
        return true;
    }

    protected virtual void PlaceTower()
    {
        this.towerPlaced = true;
        this.HideTemplate();
        Invoke(nameof(this.PlaceFinish), 0.3f);
    }

    public virtual bool TryPlaceTower()
    {
        if (!this.CanPlaceTower()) return false;
        this.PlaceTower();
        return true;
    }

    protected virtual void PlaceFinish()
    {
        if (!this.selectedPlaceAble.CanPlace()) return;
        if (!this.CanBuySelectedTower())
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

    protected virtual TowerCtrl Spawn(TowerCtrl prefab, TowerPlaceAble placeAble)
    {
        return TowerSpawnerCtrl.Instance.Spawner.Spawn(prefab, placeAble.GetPosition());
    }

    protected virtual TowerCode MapKeyCodeToTowerCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Alpha1: return TowerCode.Archer_1;
            case KeyCode.Alpha2: return TowerCode.Canon_1;
            case KeyCode.Alpha3: return TowerCode.Mage_1;
            case KeyCode.Alpha4: return TowerCode.Barrack_1;
            default: return TowerCode.NoTower;
        }
    }

    protected virtual TowerCtrl GetSelectedTower()
    {
        return TowerSpawnerCtrl.Instance.Prefabs.GetByName(this.selectedTower.ToString());
    }

    public virtual void UpgradeTower(TowerCtrl ctrl, TowerPlaceAble placeAble)
    {
        //Debug.Log(transform.name + "UpgradeTower");
        TowerCode targetTower = ctrl.Code;

        TowerCode upgradeCode = towerInfoManager.GetUpgrade(targetTower);

        if (upgradeCode != TowerCode.NoTower)
        {
            ctrl.Despawn.DoDespawn();

            TowerCtrl newTower = this.Spawn(TowerSpawnerCtrl.Instance.Prefabs.GetByName(upgradeCode.ToString()), placeAble);
            placeAble.SetTower(newTower);
            newTower.TowerShooting.Active();
            newTower.SetActive(true);

            EffectCtrl prefab = EffectSpawnerCtrl.Instance.Prefabs.GetByName(EffectCode.Place1.ToString());
            EffectCtrl newEfffect = EffectSpawnerCtrl.Instance.Spawner.Spawn(prefab, placeAble.GetPosition(), transform.rotation);
            newEfffect.gameObject.SetActive(true);
        }

    }

    public override void Init()
    {
        InventoriesManager.Instance.OnGoldChanged += OnGoldChanged;
    }

    protected void OnGoldChanged(int newGold = -1)
    {
        if (newGold == -1)
        {
            newGold = InventoriesManager.Instance.GetPlayerGold();
        }

        var allTowerCodes = towerInfoManager.GetAllTowerCodes();
        foreach (var towerCode in allTowerCodes)
        {
            int towerPrice = GetTowerPrice(towerCode);
            bool canBuy = (newGold >= towerPrice);

            if (!CanBuyTower.ContainsKey(towerCode))
            {
                CanBuyTower.Add(towerCode, canBuy);
            }
            else
            {
                CanBuyTower[towerCode] = canBuy;
            }
        }
    }

    protected virtual void UpdateCanBuyTower()
    {
        this.OnGoldChanged();
    }
}

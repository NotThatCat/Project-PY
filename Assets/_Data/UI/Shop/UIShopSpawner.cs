using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopSpawner : Spawner<UIShopCtrl>
{
    [SerializeField] protected UIShopInvPrefabs prefabs;
    public UIShopInvPrefabs Prefabs => prefabs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs != null) return;
        this.prefabs = GetComponentInChildren<UIShopInvPrefabs>();
        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }
}

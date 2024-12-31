using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventorySpawner : Spawner<UIInventoryCtrl>
{

    [SerializeField] protected UIInventoryPrefabs prefabs;
    public UIInventoryPrefabs Prefabs => prefabs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs != null) return;
        this.prefabs = GetComponentInChildren<UIInventoryPrefabs>();
        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstract : PMono
{
    [SerializeField] protected EnemyCtrl ctrl;

    protected override void LoadComponents()
    {
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponentInParent<EnemyCtrl>();
    }
}

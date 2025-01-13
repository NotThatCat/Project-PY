using UnityEngine;

public class EnemyLevel : LevelAbstract
{
    [SerializeField] protected EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTowerCtrl();
    }

    protected virtual void LoadTowerCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = GetComponentInParent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadTowerCtrl", gameObject);
    }

    protected override void FixedUpdate()
    {

    }

    protected override bool DeductExp(int exp)
    {
        return false;
    }

    protected override int GetCurrentExp()
    {
        return 0;
    }

}

using UnityEngine;

public class EnemyLevel : LevelAbstract
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected float liveTime = 0f;

    protected virtual void OnEnable()
    {
        this.liveTime = 0f;
    }

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
        this.liveTime += Time.deltaTime;
        base.FixedUpdate();
    }

    protected override bool DeductExp(int exp)
    {
        if (this.liveTime > exp)
        {
            this.liveTime -= exp;
            return true;
        }
        return false;
    }

    protected override int GetCurrentExp()
    {
        return (int)this.liveTime;
    }

}

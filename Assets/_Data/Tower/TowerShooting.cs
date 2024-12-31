using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : TowerAbstract
{
    [Header("Shooting")]
    [SerializeField] protected bool isDisable = true;
    [SerializeField] protected EnemyCtrl target;
    [SerializeField] protected EffectCode bulletCode = EffectCode.Projectile1;
    [SerializeField] protected EffectCode muzzleCode = EffectCode.Hit1;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float delay = 1f;
    [SerializeField] protected int firePointIndex = 0;
    [SerializeField] protected List<FirePoint> firePoints = new();
    [SerializeField] protected SoundName shootSfxName = SoundName.LaserKickDrum;
    protected int firePointCurrent = 0;

    [SerializeField] protected int killCount = 0;
    public int KillCount => killCount;
    [SerializeField] protected int totalKill = 0;

    protected virtual void FixedUpdate()
    {
        this.GetTarget();
        this.LookAtTarget();
        this.Shooting();
        this.IsTargetDead();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFirePoints();
    }

    protected virtual void GetTarget()
    {
        this.target = this.ctrl.Radar.GetTarget();
    }

    protected virtual void LookAtTarget()
    {
        if (this.target == null) return;
        this.ctrl.Rotator.LookAt(this.target.transform.position);
    }

    protected virtual void Shooting()
    {
        this.timer += Time.deltaTime;
        if (this.target == null) return;
        if (this.timer < this.delay) return;
        this.timer = 0;

        FirePoint firePoint = this.GetFirePoint();

        this.SpawnBullet(firePoint);
        this.SpawnMuzzle(firePoint);
        this.SpawnSound(firePoint.transform.position);
    }

    protected virtual void SpawnSound(Vector3 position)
    {

        SoundManager.Instance.CreateSfx(this.shootSfxName, position);
    }

    protected virtual EffectCtrl SpawnBullet(FirePoint firePoint)
    {
        EffectCtrl prefab = EffectSpawnerCtrl.Instance.Prefabs.GetByName(this.bulletCode.ToString());
        EffectCtrl newEfffect = EffectSpawnerCtrl.Instance.Spawner.Spawn(prefab, firePoint.transform.position, firePoint.transform.rotation);
        newEfffect.gameObject.SetActive(true);

        return prefab;
    }

    protected virtual EffectCtrl SpawnMuzzle(FirePoint firePoint)
    {
        EffectCtrl hitPrefab = EffectSpawnerCtrl.Instance.Prefabs.GetByName(this.muzzleCode.ToString());
        EffectCtrl newHitEfffect = EffectSpawnerCtrl.Instance.Spawner.Spawn(hitPrefab, firePoint.transform.position, firePoint.transform.rotation);
        newHitEfffect.gameObject.SetActive(true);

        return hitPrefab;
    }

    protected virtual FirePoint GetFirePoint()
    {
        if (this.firePoints.Count > 1)
        {
            if (this.firePointCurrent >= this.firePoints.Count) this.firePointCurrent = 0;
            this.firePointCurrent++;
            return this.firePoints[this.firePointCurrent - 1];
        }
        return this.firePoints[0];
    }

    protected virtual void LoadFirePoints()
    {
        if (this.firePoints.Count > 0) return;
        FirePoint[] points = this.ctrl.GetComponentsInChildren<FirePoint>();
        this.firePoints = new List<FirePoint>(points);
        //Debug.LogWarning(transform.name + ": LoadFirePoints", gameObject);
    }

    protected virtual bool IsTargetDead()
    {
        if (this.target == null) return true;
        if (!this.target.EnemyDamageReceiver.IsDead()) return false;
        this.killCount++;
        this.totalKill++;
        this.target = null;
        return true;
    }

    public virtual bool DeductKillCount(int count)
    {
        if (this.killCount < count) return false;
        this.killCount -= count;
        return true;
    }

    public virtual void Active()
    {
        this.isDisable = false;
    }

    public virtual void Disable()
    {
        this.isDisable = true;
    }
}

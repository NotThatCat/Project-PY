using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWave : PMono
{
    [DoNotSerialize] public int id = 0;
    [SerializeField] protected string enemyName;
    [SerializeField] protected float spawnSpeed = 1f;
    [SerializeField] protected int count = 0;
    [SerializeField] protected int maxSpawn = 10;
    [SerializeField] protected float nextWave = 10f;
    [SerializeField] protected List<EnemyCtrl> spawnedEnemies = new();
    [SerializeField] protected PathMoving wavePath;

    protected override void Start()
    {

    }

    public virtual void StartWave()
    {
        Invoke(nameof(this.Spawning), this.spawnSpeed);
        Invoke(nameof(this.RemoveDeadOne), this.spawnSpeed);
    }

    protected virtual void FixedUpdate()
    {
        this.RemoveDeadOne();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    protected virtual void Spawning()
    {
        this.count++;
        if (this.count > this.maxSpawn)
        {
            this.SpawnComplete();
            return;
        }

        Invoke(nameof(this.Spawning), this.spawnSpeed);

        EnemyCtrl prefab = EnemySpawnerCtrl.Instance.Prefabs.GetByName(this.enemyName);
        EnemyCtrl newEnemy = EnemySpawnerCtrl.Instance.Spawner.Spawn(prefab, transform.position);

        this.SetEnemyPath(newEnemy);

        newEnemy.gameObject.SetActive(true);
        this.spawnedEnemies.Add(newEnemy);
    }

    protected virtual void SetEnemyPath(EnemyCtrl newEnemy)
    {
        if (this.wavePath != null)
        {
            newEnemy.Moving.SetPath(this.wavePath);
            newEnemy.transform.position = this.wavePath.GetPoint(0).transform.position;
        }
    }

    protected virtual void SpawnComplete()
    {
        EnemyWaveManager.Instance.WaveComplete(this.id, this.nextWave);
    }

    protected virtual void RemoveDeadOne()
    {
        if (this.spawnedEnemies.Count == 0 && this.count == this.maxSpawn)
        {
            EnemyWaveManager.Instance.WaveCleaned(this.id);
            return;
        }

        Invoke(nameof(this.RemoveDeadOne), this.spawnSpeed);

        foreach (EnemyCtrl enemyCtrl in this.spawnedEnemies)
        {
            if (enemyCtrl.EnemyDamageReceiver.IsDead())
            {
                this.spawnedEnemies.Remove(enemyCtrl);
                return;
            }
        }
    }
}

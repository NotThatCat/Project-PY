using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : PMono
{
    [SerializeField] protected EnemySpawner spawner;
    [SerializeField] protected EnemyCtrl enemy;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float delay = 1f;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawn();
    }

    protected virtual void LoadDespawn()
    {
        if (this.spawner != null) return;
        this.spawner = transform.GetComponent<EnemySpawner>();
        //Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void FixedUpdate()
    {
        this.Spawn();
    }

    protected virtual void Spawn()
    {
        this.timer += Time.deltaTime;
        if (this.timer < this.delay) return;
        this.timer = 0;
        EnemyCtrl newEnemy = spawner.Spawn(this.enemy, spawnPoint.position);
        newEnemy.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : PoolObj
{

    [SerializeField] protected NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;

    [SerializeField] protected Animator _animator;
    public Animator Animator => _animator;

    [SerializeField] protected EnemyDamageReceiver enemyDamageReceiver;
    public EnemyDamageReceiver EnemyDamageReceiver => enemyDamageReceiver;

    [SerializeField] protected LevelAbstract level;
    public LevelAbstract Level => level;

    [SerializeField] protected EnemyMoving moving;
    public EnemyMoving Moving => moving;

    protected override void LoadComponents()
    {
        this.LoadAgent();
        this.LoadAnimator();
        this.LoadEnemyDamageReceiver();
        this.LoadDespawn();
        this.LoadLevel();
        this.LoadMoving();
    }

    protected virtual void LoadAgent()
    {
        if (this._agent != null) return;
        this._agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void LoadAnimator()
    {
        if (this._animator != null) return;
        this._animator = transform.Find("Model").GetComponent<Animator>();
    }

    protected virtual void LoadEnemyDamageReceiver()
    {
        if (this.enemyDamageReceiver != null) return;
        this.enemyDamageReceiver = GetComponentInChildren<EnemyDamageReceiver>();
        Debug.Log(transform.name + ": LoadEnemyDamageReceiver", gameObject);
    }

    public override string GetName()
    {
        return "Enemy";
    }

    protected virtual void LoadLevel()
    {
        if (this.level != null) return;
        this.level = GetComponentInChildren<LevelAbstract>();
        Debug.Log(transform.name + ": LoadLevel", gameObject);
    }

    protected virtual void LoadMoving()
    {
        if (this.moving != null) return;
        this.moving = GetComponentInChildren<EnemyMoving>();
        Debug.Log(transform.name + ": LoadMoving", gameObject);
    }
}

using System;
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

    public Action<EnemyCtrl> OnEnemyDead { get; internal set; }

    [SerializeField] protected bool isStunned = false;
    [SerializeField] protected float stunTimer = 0;

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

    protected override void Start()
    {
        this.Agent.isStopped = false;
        this.isStunned = this.Agent.isStopped;
        this.stunTimer = 0;
    }

    protected virtual void OnDisable()
    {
        EnemyManager.Instance.RemoveEnemy(this);
    }

    public virtual void StunEnemy(float time)
    {
        this.Agent.isStopped = true;
        this.isStunned = this.Agent.isStopped;
        this.stunTimer += time;
        //TimerManager.Instance.StartTimer(time, this.Unstun);
    }

    public virtual void Unstun()
    {
        if (this.enemyDamageReceiver.IsDead()) return;
        this.Agent.isStopped = false;
        this.isStunned = this.Agent.isStopped;
        this.stunTimer = 0;
    }

    public void UpdateLogic(float deltaTime)
    {
        if (!isStunned) return;
        stunTimer -= deltaTime;
        if (stunTimer <= 0f)
        {
            stunTimer = 0f;
            this.Unstun();
        }
    }
}

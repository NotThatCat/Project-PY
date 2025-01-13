using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected float hpScaling = 1.1f;
    [SerializeField] protected EnemyCtrl ctrl;
    [SerializeField] protected CapsuleCollider capsuleCollider;
    [SerializeField] protected int baseGoldDrop = 1;
    [SerializeField] protected float goldScaling = 1.2f;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
        this.LoadCapsuleCollider();
    }
    protected virtual void LoadCapsuleCollider()
    {
        if (this.capsuleCollider != null) return;
        this.capsuleCollider = GetComponent<CapsuleCollider>();
        this.capsuleCollider.center = new Vector3(0, 1, 0);
        this.capsuleCollider.radius = 0.3f;
        this.capsuleCollider.height = 1.5f;
        this.capsuleCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadCapsuleCollider", gameObject);
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponentInParent<EnemyCtrl>();
    }

    protected virtual void DoDespawn()
    {
        //Debug.Log(transform.name + ": DoDespawn", gameObject);
        this.ctrl.Despawn.DoDespawn();
        //this.currentHP = this.maxHP;
    }

    protected override void OnDead()
    {
        //Debug.Log(transform.name + ": OnDead", gameObject);

        this.capsuleCollider.enabled = false;
        if (!this.ctrl.Animator.GetBool("isDying"))
        {
            this.ctrl.Animator.SetTrigger("isDying");
            int timerID = TimerManager.Instance.StartTimer(0.5f, this.DoDespawn);

            InventoriesManager.Instance.AddItem(ItemCode.Gold, this.GetGoldDropByLevel(this.ctrl.Level.CurrentLevel));
        }
    }

    protected override void OnHurt()
    {
        //Debug.Log(transform.name + ": OnHurt", gameObject);
        if (!this.ctrl.Animator.GetBool("isHit"))
        {
            this.ctrl.Animator.SetTrigger("isHit");
            int timerID = TimerManager.Instance.StartTimer(0.5f, this.ResetHurtTrigger);
        }
    }

    protected virtual void ResetHurtTrigger()
    {
        this.ctrl.Animator.ResetTrigger("isHit");
    }
    protected override void Reborn()
    {
        this.maxHP = this.GetHpByLevel(this.ctrl.Level.CurrentLevel);
        this.currentHP = this.maxHP;
        this.capsuleCollider.enabled = true;
    }

    protected virtual int GetHpByLevel(int level)
    {
        return (int)(this.baseHP * Mathf.Pow(hpScaling, level));
    }

    protected virtual int GetGoldDropByLevel(int level)
    {
        return (int)(this.baseGoldDrop * Mathf.Pow(goldScaling, level));
    }

    public override void EffectReceiver(StatusCode code, float time)
    {
        switch (code)
        {
            case StatusCode.Stun:
                this.Stun(time); break;
        }
    }

    public virtual void Stun(float time)
    {
        this.ctrl.StunEnemy(time);
    }
}

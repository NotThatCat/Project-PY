using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DamageReceiver : PMono
{
    [SerializeField] protected int currentHP = 10;
    public int CurrentHP => currentHP;

    [SerializeField] protected int maxHP = 10;
    public int MaxHP => maxHP;

    [SerializeField] protected int baseHP = 10;
    public int BaseHP => baseHP;

    [SerializeField] protected bool isDead = false;
    [SerializeField] protected bool isImmotal = false;

    protected abstract void OnDead();
    protected abstract void OnHurt();

    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    public virtual void Receive(int damage, DamageSender damageSender)
    {
        if (!this.isImmotal) this.currentHP -= damage;
        if (this.currentHP < 0) this.currentHP = 0;

        if (this.IsDead()) this.OnDead();
        else this.OnHurt();
    }

    public virtual bool IsDead()
    {
        return this.isDead = this.currentHP <= 0;
    }


    protected virtual void Reborn()
    {
        this.currentHP = this.maxHP;
    }

    public virtual void EffectReceiver(StatusCode code, float time)
    {

    }
}

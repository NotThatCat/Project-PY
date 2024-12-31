using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected EnemyCtrl ctrl;
    [SerializeField] protected CapsuleCollider capsuleCollider;


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
            int timerID = TimerManager.Instance.StartTimer(2f, this.DoDespawn);

            InventoriesManager.Instance.AddItem(ItemCode.Gold, 1);
            //Debug.Log(transform.name + ": timerID: " + timerID, gameObject);

            //ItemDropSpawnerCtrl.Instance.DropMany(ItemCode.Gold, transform.position, 10);
            //ItemDropSpawnerCtrl.Instance.DropMany(ItemCode.Crystal, transform.position, 3);
            //ItemDropSpawnerCtrl.Instance.Drop(ItemCode.Wand, transform.position, 1);
            //ItemDropSpawnerCtrl.Instance.Drop(ItemCode.WandEpic, transform.position, 1);
            //ItemDropSpawnerCtrl.Instance.Drop(ItemCode.Hammer, transform.position, 1);
            //ItemDropSpawnerCtrl.Instance.Drop(ItemCode.Axe, transform.position, 1);
            //InventoriesManager.Instance.AddItem(ItemCode.PlayerExp, 1);
        }
    }

    protected override void OnHurt()
    {
        //Debug.Log(transform.name + ": OnHurt", gameObject);
        if (!this.ctrl.Animator.GetBool("isHit"))
        {
            this.ctrl.Animator.SetTrigger("isHit");
            int timerID = TimerManager.Instance.StartTimer(2f, this.ResetHurtTrigger);
        }
    }

    protected virtual void ResetHurtTrigger()
    {
        this.ctrl.Animator.ResetTrigger("isHit");
    }
    protected override void Reborn()
    {
        base.Reborn();
        this.capsuleCollider.enabled = true;
    }
}

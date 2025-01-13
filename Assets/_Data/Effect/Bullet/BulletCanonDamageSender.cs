using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class BulletCanonDamageSender : BulletDamageSender
{
    [SerializeField] protected List<DamageReceiver> damageReceivers = new();
    [SerializeField] protected Collider explosionCollider;
    [SerializeField] protected EffectCode explosionCode;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadExploreCollider();
    }

    protected virtual void LoadExploreCollider()
    {
        if (this.explosionCollider != null) return;
        this.explosionCollider = transform.Find("ExplosionCollider").GetComponentInChildren<Collider>();
    }

    protected override DamageReceiver SendDamage(Collider collider)
    {
        DamageReceiver damageReceiver = base.SendDamage(collider);
        if (damageReceiver == null) return null;
        this.despawn.DoDespawn();
        return damageReceiver;
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
        if (damageReceiver == null && collider.transform.parent.name == "UIHide")
        {
            this.SendDamage();
            this.SpawnExplosion();
            this.despawn.DoDespawn();
            return;
        }

    }

    protected virtual void SendDamage()
    {
        Vector3 center = this.explosionCollider.bounds.center;
        Vector3 halfExtents = this.explosionCollider.bounds.extents;

        List<Collider> colliders = Physics.OverlapBox(center, halfExtents, Quaternion.identity).ToList<Collider>();

        this.damageReceivers = new List<DamageReceiver>();
        foreach (Collider collider in colliders)
        {
            DamageReceiver damageReceiver = this.SendDamage(collider);
            if (damageReceiver == null) continue;

            this.damageReceivers.Add(damageReceiver);
        }
    }

    protected virtual void SpawnExplosion()
    {
        EffectCtrl prefab = EffectSpawnerCtrl.Instance.Prefabs.GetByName(this.explosionCode.ToString());
        EffectCtrl newEfffect = EffectSpawnerCtrl.Instance.Spawner.Spawn(prefab, transform.position, EffectSpawnerCtrl.Instance.GetExplosionQuaternion());
        newEfffect.gameObject.SetActive(true);
    }
}

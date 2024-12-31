using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPicker : PlayerInteractAble
{
    [SerializeField] protected SphereCollider sphereCollider;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSphereCollider();
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.radius = 2.5f;
        this.sphereCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null) return;
        ItemDropCtrl itemDropCtrl = other.transform.parent.GetComponent<ItemDropCtrl>();
        if (itemDropCtrl == null) return;
        //Debug.Log(itemDropCtrl.GetItemCode());
        itemDropCtrl.Despawn.DoDespawn();
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override void UnInteract()
    {
        throw new System.NotImplementedException();
    }
}

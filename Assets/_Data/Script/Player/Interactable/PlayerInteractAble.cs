using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerInteractAble : PMono
{
    public bool canMouseInteract = false;
    public bool canCollect = false;

    public virtual Vector3 GetPosition()
    {
        return transform.position;
    }

    public virtual void Collect()
    {
        if (!this.canCollect) return;
        transform.parent.GetComponentInChildren<DespawnBase>().DoDespawn();
    }

    public virtual bool CanMouseInteract() { return canMouseInteract; }

    public virtual void MouseInteract()
    {
        if (!this.canMouseInteract) return;
        this.Interact();
    }

    public abstract void Interact();
    public abstract void UnInteract();
}

using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField] protected PlayerCtrl ctrl;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponentInParent<PlayerCtrl>();
    }

    protected override void OnDead()
    {
        GameManager.Instance.GameOver();
    }

    protected override void OnHurt()
    {

    }
}

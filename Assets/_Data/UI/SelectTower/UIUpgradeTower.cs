using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeTower : SaiSingleton<UIUpgradeTower>
{
    [SerializeField] protected Transform Scroll;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScroll();
    }

    protected virtual void LoadScroll()
    {
        if (this.Scroll != null) return;
        this.Scroll = transform.Find("Scroll");
    }

    public virtual void ShowUI()
    {
        this.Scroll.gameObject.SetActive(true);
    }

    public virtual void HideUI()
    {
        this.Scroll.gameObject.SetActive(false);
    }
}

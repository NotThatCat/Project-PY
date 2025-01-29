using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeTower : SaiSingleton<UIUpgradeTower>
{
    [SerializeField] protected Transform Scroll;
    [SerializeField] protected Transform UIBlock;
    [SerializeField] protected List<ButtonInteractTower> buttonList = new List<ButtonInteractTower>();
    [SerializeField] protected bool isActive = false;
    [SerializeField] public bool IsActive => isActive;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScroll();
        this.LoadButton();
    }

    protected override void Awake()
    {
        base.Awake();
        if (this.buttonList.Count > 0)
        {
            this.HideUI();
        }
        else
        {
            this.LoadComponents();
        }
    }

    protected virtual void LoadButton()
    {
        this.buttonList = transform.GetComponentsInChildren<ButtonInteractTower>().ToList<ButtonInteractTower>();
    }

    protected virtual void LoadScroll()
    {
        if (this.Scroll != null) return;
        this.Scroll = transform.Find("Scroll");
    }

    public virtual void ShowUI()
    {
        //PlayerInteractAble playerInteractAble = InputManager.Instance.CurrentInteractAble;
        //TowerPlaceAble placeAble = playerInteractAble.transform.GetComponent<TowerPlaceAble>();
        //Debug.Log(transform.name + "ShowUI");
        //if (placeAble != null && placeAble.CanPlace()) this.HideUI();
        //if (placeAble != null)
        //{
        //    if (placeAble.CanPlace())
        //    {
        //        this.HideUI();
        //        return;
        //    }
        //}
        //else
        //{
        //    this.HideUI();
        //    return;
        //}

        this.Scroll.gameObject.SetActive(true);
        this.UIBlock.gameObject.SetActive(true);
        this.isActive = true;
        foreach (ButtonInteractTower button in buttonList)
        {
            button.UpdateInfo();
        }
    }

    public virtual void HideUI()
    {
        this.Scroll.gameObject.SetActive(false);
        this.UIBlock.gameObject.SetActive(false);
        this.isActive = false;
    }
}

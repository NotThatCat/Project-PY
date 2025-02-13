using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeTowerCtrl : ButttonAbstract
{
    [SerializeField] protected TowerCode towerCode = TowerCode.NoTower;
    public TowerCode TowerCode => towerCode;
    [SerializeField] protected KeyCode keyCode = KeyCode.None;
    public KeyCode KeyCode => keyCode;
    [SerializeField] protected Transform BGRed;
    [SerializeField] protected Transform BGBlue;
    [SerializeField] protected bool canUpgradeTower = true;
    [SerializeField] protected TextPrice textPrice;
    [SerializeField] protected TextHotKeys textHotKeys;

    protected override void Start()
    {
        base.Start();
        this.UpdateText();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTower();
        this.LoadBackGround();
        this.LoadText();
    }

    protected virtual void LateUpdate()
    {
        this.UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        if (canUpgradeTower != TowerManager.Instance.CanBuyTowerByCode(this.towerCode))
        {
            this.ToogleBG();
        }
    }

    protected virtual void LoadBackGround()
    {
        this.BGRed = transform.Find("BGRed");
        this.BGBlue = transform.Find("BGBlue");
    }

    protected virtual void LoadText()
    {
        this.textPrice = transform.GetComponentInChildren<TextPrice>();
        this.textHotKeys = transform.GetComponentInChildren<TextHotKeys>();
    }

    protected virtual void ToogleBG()
    {
        this.BGRed.gameObject.SetActive(!canUpgradeTower);
        this.BGBlue.gameObject.SetActive(canUpgradeTower);
    }

    protected virtual void LoadTower()
    {
        if (Enum.TryParse(transform.name, out TowerCode code))
        {
            this.towerCode = code;
        }
        else
        {
            Debug.Log("Tower not found, please update transform name");
        }
    }

    protected virtual void UpdateText()
    {
        this.textHotKeys.LoadHotKeys((this.keyCode - KeyCode.Alpha0).ToString());
        this.textPrice.LoadPrice(TowerManager.Instance.GetTowerPrice(this.towerCode).ToString());
    }

    public virtual void Init(KeyCode keyCode, TowerCode towerCode = TowerCode.NoTower)
    {
        if (towerCode != TowerCode.NoTower) this.towerCode = towerCode;
        this.keyCode = keyCode;
    }

    public virtual int GetPrice()
    {
        return TowerManager.Instance.GetTowerPrice(this.towerCode);
    }

    public override void OnClick()
    {
        InputHotkeys.Instance.ToogleNumber(this.keyCode);
    }
}

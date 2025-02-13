using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BtnSelectTower : ButttonAbstract
{
    [SerializeField] protected TowerCode towerCode = TowerCode.NoTower;
    public TowerCode TowerCode => towerCode;
    [SerializeField] protected KeyCode keyCode = KeyCode.None;
    public KeyCode KeyCode => keyCode;
    [SerializeField] protected Transform BGRed;
    [SerializeField] protected Transform BGBlue;
    [SerializeField] protected bool canBuyTower = true;
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

    protected virtual void FixedUpdate()
    {
        this.UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        if (canBuyTower != TowerManager.Instance.CanBuyTowerByCode(this.towerCode))
        {
            this.ToogleBG();
        }
    }

    protected virtual bool IsSelected()
    {
        if (InputHotkeys.Instance.KeyCode == this.keyCode)
        {
            return true;
        }
        return false;
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
        this.canBuyTower = !this.canBuyTower;
        this.BGRed.gameObject.SetActive(!canBuyTower);
        this.BGBlue.gameObject.SetActive(canBuyTower);
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
        if (this.IsSelected())
        {
            UISelectTower.Instance.Select(this);
        }
        else
        {

            UISelectTower.Instance.DeSelect();
        }
    }
}

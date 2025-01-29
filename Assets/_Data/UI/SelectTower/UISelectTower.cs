using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISelectTower : SaiSingleton<UISelectTower>
{
    [SerializeField] protected List<BtnSelectTower> btnListSelect = new List<BtnSelectTower>();
    [SerializeField] protected BtnSelectTower selectedBtn;
    [SerializeField] protected Transform selectIcon;
    [SerializeField] protected bool isSelecting = true;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUITowerCtrl();
        this.LoadSelectIcon();
    }

    public virtual void Select(BtnSelectTower selectedBtn)
    {
        this.Selecting();
        this.selectedBtn = selectedBtn;
        this.selectIcon.transform.position = this.selectedBtn.transform.position;
    }

    public virtual void ToggleSelect()
    {
        this.isSelecting = !this.isSelecting;
        this.selectIcon.gameObject.SetActive(this.isSelecting);
    }

    protected virtual void Selecting()
    {
        this.isSelecting = true;
        this.selectIcon.gameObject.SetActive(this.isSelecting);
    }

    public virtual void DeSelect()
    {
        this.isSelecting = false;
        this.selectIcon.gameObject.SetActive(this.isSelecting);
    }

    protected override void Start()
    {
        base.Start();
        this.ToggleSelect();
    }

    private void LoadUITowerCtrl()
    {
        if (this.btnListSelect.Count > 0) return;
        int count = 1;
        Transform holder = transform.Find("Scroll")?.Find("Holder");
        if (holder != null)
        {
            foreach (Transform child in holder)
            {
                BtnSelectTower uITowerCtrl = child.GetComponent<BtnSelectTower>();
                if (uITowerCtrl != null)
                {
                    KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + count);
                    uITowerCtrl.Init(key);
                    count++;
                    this.btnListSelect.Add(uITowerCtrl);
                }
            }
        }
    }

    private void LoadSelectIcon()
    {
        this.selectIcon = transform.Find("SelectIcon");
    }
}

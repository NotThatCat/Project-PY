using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISelectTower : SaiSingleton<UISelectTower>
{
    [SerializeField] protected List<UITowerCtrl> towerCtrls = new List<UITowerCtrl>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUITowerCtrl();
    }

    private void LoadUITowerCtrl()
    {
        if (this.towerCtrls.Count > 0) return;
        int count = 1;
        Transform holder = transform.Find("Scroll")?.Find("Holder");
        if(holder != null)
        {
            foreach (Transform child in holder)
            {
                UITowerCtrl uITowerCtrl = child.GetComponent<UITowerCtrl>();
                if (uITowerCtrl != null)
                {
                    KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + count);
                    uITowerCtrl.Init(key);
                    count++;
                    this.towerCtrls.Add(uITowerCtrl);
                }
            }
        }
    }
}

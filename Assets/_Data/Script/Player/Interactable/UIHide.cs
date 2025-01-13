using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIHide : PlayerInteractAble
{
    public override void Interact()
    {
        UIUpgradeTower.Instance.HideUI();
    }

    public override void UnInteract()
    {

    }
}

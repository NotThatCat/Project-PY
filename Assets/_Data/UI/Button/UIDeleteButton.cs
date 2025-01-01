using UnityEngine;
using UnityEngine.UI;

public class UIDeleteButton : ButtonAbstact
{
    [SerializeField] TowerCtrl towerCtrl;

    protected virtual void OnEnable()
    {
        PlayerInteractAble playerInteractAble = InputManager.Instance.PlayerInteractAble;
        TowerPlaceAble towerPlaceAble = playerInteractAble.transform.GetComponent<TowerPlaceAble>();
        if(towerPlaceAble != null)
        {
            this.towerCtrl = towerPlaceAble.TowerCtrl;
        }
    }

    public override void OnClick()
    {
        this.towerCtrl.Despawn.DoDespawn();
        InventoriesManager.Instance.AddItem(ItemCode.Gold, 50);
        UIUpgradeTower.Instance.HideUI();
    }
}

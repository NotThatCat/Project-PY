using UnityEngine;
using UnityEngine.UI;

public class UIDeleteButton : ButtonAbstact
{
    [SerializeField] TowerCtrl towerCtrl;

    protected override void Start()
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
        UIUpgradeTower.Instance.HideUI();
    }
}

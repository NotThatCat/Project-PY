using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonInteractTower : ButtonAbstact
{
    [SerializeField] protected TowerCtrl towerCtrl;
    [SerializeField] protected TextPrice textPrice;
    [SerializeField] protected TowerPlaceAble placeAble;
    [SerializeField] protected Transform BGRed;
    [SerializeField] protected Transform BGBlue;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
        this.LoadBackGround();
    }

    protected virtual void LoadText()
    {
        this.textPrice = transform.GetComponentInChildren<TextPrice>();
    }

    protected virtual void LoadBackGround()
    {
        this.BGRed = transform.Find("BGRed");
        this.BGBlue = transform.Find("BGBlue");
    }

    public abstract void UpdateInfo();
}

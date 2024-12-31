using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonAbstact : PMono
{
    [SerializeField] protected Button button;

    protected override void Start()
    {
        base.Start();
        this.AddOnClickEvent();
    }

    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(this.OnClick);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBtn();
    }

    protected virtual void LoadBtn()
    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();
        Debug.Log(transform.name + ": LoadButton", gameObject);
    }

    public abstract void OnClick();

}

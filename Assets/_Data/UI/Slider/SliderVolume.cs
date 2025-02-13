using UnityEngine;
using UnityEngine.UI;

public abstract class SliderVolume : SliderAbstact
{
    [SerializeField] public Slider Slider => slider;
    [SerializeField] protected UISliderCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSliderCtrl();
    }

    protected virtual void LoadSliderCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponentInParent<UISliderCtrl>();
        Debug.Log(transform.name + ": LoadSliderCtrl", gameObject);
    }
}

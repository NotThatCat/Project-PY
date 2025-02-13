using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISliderCtrl : PMono
{
    [SerializeField] protected SliderVolume slider;
    [SerializeField] protected Transform iconOn;
    [SerializeField] protected Transform iconOff;
    [SerializeField] protected bool isActive;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSlider();
        this.LoadIcon();
    }

    protected override void Start()
    {
        this.OnSliderChange(this.slider.Slider.value);
    }

    protected virtual void LoadSlider()
    {
        this.slider = transform.GetComponentInChildren<SliderVolume>();
    }
    
    protected virtual void LoadIcon()
    {
        this.iconOn = transform.Find("IconOn");
        this.iconOff = transform.Find("IconOff");
    }

    public virtual void Toggle()
    {
        this.isActive = !this.isActive;
        this.UpdateSlider();
    }

    protected virtual void UpdateSlider()
    {
        this.iconOn.gameObject.SetActive(this.isActive);
        this.iconOff.gameObject.SetActive(!this.isActive);
    }

    public virtual void OnSliderChange(float value)
    {
        if (this.isActive && value > 0) return;
        if (!this.isActive && value <= 0) return;
        this.Toggle();

    }
}

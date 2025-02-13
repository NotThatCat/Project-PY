using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeSfx : SliderVolume
{
    protected override void OnSliderValueChanged(float value)
    {
        this.ctrl.OnSliderChange(value);
        SoundManager.Instance.VolumeSfxUpdating(value);
    }
}

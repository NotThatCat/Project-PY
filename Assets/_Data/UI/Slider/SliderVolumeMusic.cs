using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeMusic  : SliderVolume
{
    protected override void OnSliderValueChanged(float value)
    {
        this.ctrl.OnSliderChange(value);
        SoundManager.Instance.VolumeMusicUpdating(value);
    }
}

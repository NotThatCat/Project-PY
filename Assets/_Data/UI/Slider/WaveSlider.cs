using UnityEngine;
using UnityEngine.UI;

public class WaveSlider : SliderAbstact
{
    [SerializeField] public Slider Slider => slider;

    protected override void OnSliderValueChanged(float value)
    {

    }

    protected override void Start()
    {
        this.Init();
    }

    protected virtual void OnWaveChange(int currentWave)
    {
        //this.slider.maxValue = maxWave;
        this.slider.value = currentWave;
    }

    public override void Init()
    {
        EnemyWaveManager.Instance.RegisOnWaveChange(OnWaveChange);
        this.slider.value = EnemyWaveManager.Instance.GetCurrentWave();
        this.slider.maxValue = EnemyWaveManager.Instance.GetTotalWave();
    }
}

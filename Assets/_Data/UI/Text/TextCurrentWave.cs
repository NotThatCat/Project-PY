using UnityEngine;

public class TextCurrentWave : TextAbstact
{
    protected override void Start()
    {
        EnemyWaveManager.Instance.RegisOnWaveChange(OnWaveChange);
    }

    protected virtual void OnWaveChange(int value)
    {
        int currentWave = EnemyWaveManager.Instance.GetCurrentWave();
        int maxWave = EnemyWaveManager.Instance.GetTotalWave();
        this.textPro.text = value.ToString() + " / " + maxWave.ToString();
    }
}

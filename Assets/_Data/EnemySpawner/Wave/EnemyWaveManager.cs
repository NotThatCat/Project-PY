using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveManager : SaiSingleton<EnemyWaveManager>
{
    [SerializeField] protected List<EnemyWave> enemyWaves = new();
    [SerializeField] protected int nextWaveID = 0;
    [SerializeField] protected int timerID = -1;


    protected override void Awake()
    {
        base.Awake();
        this.HideWaves();
    }

    protected virtual void HideWaves()
    {
        foreach (EnemyWave wave in this.enemyWaves)
        {
            wave.gameObject.SetActive(false);
        }
    }

    protected override void Start()
    {
        this.StartNextWave();
    }

    protected virtual void FixedUpdate()
    {

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaves();
    }

    protected virtual void LoadWaves()
    {
        if (this.enemyWaves.Count > 0) return;
        foreach (Transform child in transform)
        {
            EnemyWave wave = child.GetComponent<EnemyWave>();
            if (wave != null)
            {
                this.enemyWaves.Add(wave);
                wave.id = this.enemyWaves.Count - 1;
            }
        }
        Debug.Log(transform.name + ": LoadWave", gameObject);
    }

    public virtual void WaveComplete(int id, float nextWave = 10f)
    {
        this.nextWaveID = id + 1;
        if (this.nextWaveID < this.enemyWaves.Count)
        {
            this.timerID = TimerManager.Instance.StartTimer(nextWave, this.StartNextWave);
        }
    }

    public virtual void WaveCleaned(int id)
    {
        this.enemyWaves[id].gameObject.SetActive(false);
    }

    public virtual void StartNextWave()
    {
        if (this.timerID != -1)
        {
            TimerManager.Instance.StopTimer(this.timerID);
            this.timerID = -1;
        }

        if (this.nextWaveID < enemyWaves.Count)
        {
            this.enemyWaves[this.nextWaveID].gameObject.SetActive(true);
            this.enemyWaves[this.nextWaveID].StartWave();
        }
    }

}

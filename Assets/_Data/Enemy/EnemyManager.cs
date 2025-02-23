using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SaiSingleton<EnemyManager>
{
    [SerializeField] protected List<EnemyCtrl> enemies = new List<EnemyCtrl>();
    [SerializeField] protected bool isComplete = false;

    void Update()
    {
        float dt = Time.deltaTime;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdateLogic(dt);
        }
        this.CheckStatus();
    }

    public void AddEnemy(EnemyCtrl e)
    {
        if (!enemies.Contains(e))
        {
            enemies.Add(e);
        }
    }

    public void RemoveEnemy(EnemyCtrl e)
    {
        if (enemies.Contains(e))
        {
            enemies.Remove(e);
        }
    }

    public virtual void CheckStatus()
    {
        //if (enemies.Count == 0) this.isComplete = true;
        if (!this.isComplete && this.enemies.Count == 0 && EnemyWaveManager.Instance.IsComplete)
        {
            this.isComplete = true;
            GameManager.Instance.LevelComplete();
        }
    }
}

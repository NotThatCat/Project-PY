using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SaiSingleton<EnemyManager>
{
    [SerializeField] protected List<EnemyCtrl> enemies = new List<EnemyCtrl>();

    void Update()
    {
        float dt = Time.deltaTime;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdateLogic(dt);
        }
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

}

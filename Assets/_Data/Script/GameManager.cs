using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SaiSingleton<GameManager>
{
    //[SerializeField] protected SaveManager save;
    //public SaveManager Save => save;

    //protected override void LoadComponents()
    //{
    //    base.LoadComponents();
    //    this.LoadSaveManager();
    //}

    //protected virtual void LoadSaveManager()
    //{
    //    if (this.save != null) return;
    //    this.save = GetComponentInChildren<SaveManager>();
    //    Debug.Log(transform.name + ": LoadSaveManager", gameObject);
    //}


    [SerializeField] protected string CurrentScene;

    protected override void Start()
    {
        this.CurrentScene = SceneManager.GetActiveScene().name;
        GameObject.DontDestroyOnLoad(this.transform.gameObject);
    }

    public virtual void QuitGame()
    {
        //InventoriesManager.Instance.SaveGameData();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public virtual void Restart()
    {
        this.CleanUp();
        PlayerCtrl.Instance.gameObject.SetActive(true);
        SceneManager.LoadScene(this.CurrentScene);

        Time.timeScale = 1;
    }


    public virtual void MainMenu()
    {
        this.CleanUp();
        SceneManager.LoadScene("MainMenu");
        this.CurrentScene = "MainMenu";
        PlayerCtrl.Instance.gameObject.SetActive(false);
    }

    public virtual void StartLevel(int level)
    {
        PlayerCtrl.Instance.gameObject.SetActive(true);
        SceneManager.LoadScene("Level_" + level);
        this.CurrentScene = "Level_" + level;

        Time.timeScale = 1;
        TimerManager.Instance.StartTimer(1f, this.LevelInit);
    }

    protected virtual void LevelInit()
    {
        InventoriesManager.Instance.Init();
        TowerManager.Instance.Init();
        EnemyWaveManager.Instance.StartNextWave();
    }

    public virtual void GameOver()
    {
        UIGameOver.Instance.Show();
        Time.timeScale = 0;
        PlayerCtrl.Instance.gameObject.SetActive(false);
    }

    protected virtual void CleanUp()
    {
        TimerManager.Instance.ResetTimer();
    }

    public virtual void LevelComplete()
    {
        UIGameVictory.Instance.Toggle();
    }
}

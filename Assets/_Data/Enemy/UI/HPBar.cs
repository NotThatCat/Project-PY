using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : PMono
{
    [SerializeField] protected EnemyCtrl ctrl;
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected Slider easeHealthSlider;
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float health;
    [SerializeField] protected float lerpSpeed = 0.05f;

    protected override void LoadComponents()
    {
        this.LoadEnemyCtrl();
        this.LoadSlider();
    }

    protected virtual void LoadSlider()
    {
        this.healthSlider = transform.Find("HealthBar").GetComponent<Slider>();
        this.easeHealthSlider = transform.Find("EaseHealthSlider").GetComponent<Slider>();
    }

    protected virtual void LoadEnemyCtrl()
    {
        this.ctrl = transform.parent.parent.GetComponent<EnemyCtrl>();
    }

    protected virtual void FixedUpdate()
    {
        this.GetCurrentHealth();
        if (this.healthSlider.value != this.health)
        {
            this.healthSlider.value = this.health;
        }

        if(this.healthSlider.value != this.easeHealthSlider.value)
        {
            this.easeHealthSlider.value = Mathf.Lerp(this.easeHealthSlider.value, this.health, this.lerpSpeed);
        }
    }

    protected virtual void GetCurrentHealth()
    {
        this.health = this.ctrl.EnemyDamageReceiver.CurrentHP;
    }

    protected virtual void Start()
    {
        this.GetCurrentHealth();
        this.maxHealth = this.ctrl.EnemyDamageReceiver.MaxHP;
        this.UpdateSlider();
    }

    protected virtual void UpdateSlider()
    {
        this.healthSlider.maxValue = this.maxHealth;
        this.easeHealthSlider.maxValue = this.maxHealth;
    }
}

using UnityEngine;

public class TextPlayerHP : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.LoadPlayerHP();
    }

    protected virtual void LoadPlayerHP()
    {
        int playerHP = PlayerCtrl.Instance.PlayerDamageReceiver.CurrentHP;
        this.textPro.text = playerHP.ToString();

    }
}

using UnityEngine;

public class BtnRestartGame : ButttonAbstract
{
    public override void OnClick()
    {
        GameManager.Instance.Restart();
    }
}

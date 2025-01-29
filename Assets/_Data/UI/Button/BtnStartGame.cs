using UnityEngine;

public class BtnStartGame : ButttonAbstract
{
    public override void OnClick()
    {
        GameManager.Instance.StartLevel(1);
    }
}

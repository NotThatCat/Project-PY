using UnityEngine;

public class BtnMainMenuGame : ButttonAbstract
{
    public override void OnClick()
    {
        GameManager.Instance.MainMenu();
    }
}

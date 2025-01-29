using UnityEngine;

public class BtnTogglePause : ButttonAbstract
{
    public override void OnClick()
    {
        UIPause.Instance.Toggle();
    }
}

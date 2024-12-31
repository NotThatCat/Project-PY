using UnityEngine;

public class InputManager : SaiSingleton<InputManager>
{
    [SerializeField] protected PlayerInteractAble playerInteractAble;
    public PlayerInteractAble PlayerInteractAble => playerInteractAble;

    private void Update()
    {
        this.CheckMouseClick();
    }

    protected virtual void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerInteractAble interactAble = InputHotkeys.Instance.PlayerInteractAble;
            if (interactAble != null)
            {
                interactAble.MouseInteract();
                this.playerInteractAble = interactAble;
            }
            else
            {
                if (this.playerInteractAble != null)
                    this.playerInteractAble.UnInteract();
            }
        }
    }
}

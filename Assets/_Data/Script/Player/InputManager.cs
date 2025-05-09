using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : SaiSingleton<InputManager>
{
    [SerializeField] protected PlayerInteractAble playerInteractAble;
    public PlayerInteractAble PlayerInteractAble => playerInteractAble;

    [SerializeField] protected PlayerInteractAble currentInteractAble;
    public PlayerInteractAble CurrentInteractAble => currentInteractAble;

    private void Update()
    {
        this.CheckMouseClick();
    }

    protected virtual void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("Click UI, no raycast");
                return;
            }

            //Debug.Log("Mouse Down");
            PlayerInteractAble interactAble = InputHotkeys.Instance.PlayerInteractAble;
            if (interactAble != null)
            {
                this.playerInteractAble = interactAble;
                if (interactAble.CanMouseInteract())
                {
                    this.currentInteractAble = interactAble;
                    interactAble.MouseInteract();
                }
            }
            else
            {
                //if (this.currentInteractAble != null)
                //{
                //    this.currentInteractAble.UnInteract();
                //    this.currentInteractAble = null;
                //}
                InputHotkeys.Instance.ToogleNumber(KeyCode.None);
            }
        }
    }
}

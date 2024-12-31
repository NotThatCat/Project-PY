using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHotkeys : SaiSingleton<InputHotkeys>
{
    protected bool isToogleInventoryUI = false;
    public bool IsToogleInventoryUI => isToogleInventoryUI;

    protected bool isToogleMusic = false;
    public bool IsToogleMusic => isToogleMusic;

    protected bool isToogleSetting = false;
    public bool IsToogleSetting => isToogleSetting;

    [SerializeField] protected KeyCode keyCode;
    public KeyCode KeyCode => keyCode;

    [SerializeField] protected bool isPlaceTower;
    public bool IsPlaceTower => isPlaceTower;

    [SerializeField] protected PlayerInteractAble playerInteractAble;
    public PlayerInteractAble PlayerInteractAble => playerInteractAble;
    [SerializeField] protected LayerMask layerMask = 256;
    [SerializeField] protected float maxDistance = 100f;


    protected virtual void Update()
    {
        this.OpenInventory();
        this.ToogleMusic();
        this.ToogleSetting();
        this.ToogleNumber();
        this.GetInteractable();
    }

    protected virtual void GetInteractable()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, this.maxDistance, layerMask))
        {
            PlayerInteractAble interactAble = hitInfo.collider.transform.parent.GetComponent<PlayerInteractAble>();

            this.playerInteractAble = interactAble;
        }
        else
        {
            this.playerInteractAble = null;
        }
        Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
    }

    protected virtual void OpenInventory()
    {
        this.isToogleInventoryUI = Input.GetKeyUp(KeyCode.I);
    }

    protected virtual void ToogleNumber()
    {
        this.isPlaceTower = Input.GetKey(KeyCode.C);

        for (int i = 1; i <= 9; i++) 
        {
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + i);
            if (Input.GetKeyDown(key)) 
            {
                this.keyCode = this.keyCode == key ? KeyCode.None : key;
                break; 
            }
        }
    }

    public virtual void ToogleNumber(KeyCode key)
    {
        if(this.keyCode != key)
        {
            this.keyCode = key;
        } else
        {
            this.keyCode = KeyCode.None;
        }
    }

    protected virtual void ToogleMusic()
    {
        this.isToogleMusic = Input.GetKeyUp(KeyCode.M);
    }

    protected virtual void ToogleSetting()
    {
        this.isToogleSetting = Input.GetKeyUp(KeyCode.N);
    }
}

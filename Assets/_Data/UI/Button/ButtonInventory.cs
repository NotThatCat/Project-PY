using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInventory : ButtonAbstact
{
    [SerializeField] protected bool isActive = false;

    [ProButton]
    public virtual void AddTest()
    {
        this.button.onClick.AddListener(this.OnClick);
    }

    [SerializeField]
    protected UIInventoryManager uIInventoryManager;

    public override void OnClick()
    {
        //Debug.Log("ButtonInventory");
        //UIInventoryManager uIShopManager = GameObject.FindObjectOfType<UIInventoryManager>();
        if (!isActive)
        {
            isActive = !isActive;
            Time.timeScale = 0;
            uIInventoryManager.gameObject.SetActive(true);
            uIInventoryManager.UpdateInventory();
        }
        else
        {
            isActive = !isActive;
            Time.timeScale = 1;
            uIInventoryManager.gameObject.SetActive(false);
        }
    }

}

using UnityEngine;

public class TextCrystalCount : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.LoadCrystalCount();
    }

    protected virtual void LoadCrystalCount()
    {
        ItemInventory item = InventoriesManager.Instance.Currency().FindItem(ItemCode.Crystal);
        string crystalCount;
        if (item == null) crystalCount = "0";
        else crystalCount = item.itemCount.ToString();
        this.textPro.text = crystalCount;

    }
}

using UnityEngine;

public class TextItemCount : TextAbstact
{
    [SerializeField] protected ItemCode _itemCode;
    public ItemCode ItemCode => _itemCode;

    public virtual void LoadItemCount(ItemCode itemCode)
    {
        ItemInventory item = InventoriesManager.Instance.Currency().FindItem(itemCode);
        string itemCount;
        if (item == null)
        {
            item = InventoriesManager.Instance.Item().FindItem(itemCode);
            if (item == null) itemCount = "0";
            else itemCount = item.itemCount.ToString();
        }
        else itemCount = item.itemCount.ToString();
        this.textPro.text = itemCount;
    }

    public virtual void LoadItemCount(string count)
    {
        this.textPro.text = count;
    }
}

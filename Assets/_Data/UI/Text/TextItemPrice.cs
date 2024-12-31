using UnityEngine;

public class TextItemPrice : TextAbstact
{
    [SerializeField] protected ItemCode _itemCode;
    public ItemCode ItemCode => _itemCode;

    public virtual void LoadItemPrice(string count)
    {
        this.textPro.text = count;
    }
}

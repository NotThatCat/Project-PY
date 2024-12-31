using UnityEngine;

public class TextPrice : TextAbstact
{
    public virtual void LoadPrice(string key)
    {
        this.textPro.text = key;
    }
}

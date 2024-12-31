using UnityEngine;

public class TextHotKeys : TextAbstact
{
    public virtual void LoadHotKeys(string key)
    {
        this.textPro.text = key;
    }
}

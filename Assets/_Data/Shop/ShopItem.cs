using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : PMono
{
    protected int itemId;
    public int ItemID => itemId;

    protected ItemProfileSO itemProfile;
    public ItemProfileSO ItemProfile => itemProfile;

    public int stock = 0;

    public virtual void SetId(int id)
    {
        this.itemId = id;
    }

    public ShopItem(ItemProfileSO itemProfile, int stock)
    {
        this.itemProfile = itemProfile;
        this.stock = stock;
    }

    public virtual void SetShop(ItemProfileSO itemProfile, int stock)
    {
        this.itemProfile = itemProfile;
        this.stock = stock;
    }

    public virtual int PurchaseItem(int num)
    {
        return this.stock -= num;
    }

    public virtual bool Deduct(int number)
    {
        if (!this.CanDeduct(number)) return false;
        this.stock -= number;
        return true;
    }

    public virtual bool CanDeduct(int number)
    {
        return this.stock >= number;
    }
}

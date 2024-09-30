using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInventory
{
    public Item Item;

    public int Count;

    public ItemInventory(Item item, int count)
    {
        Item = item;
        Count = count;
    }
    public ItemInventory(Item item)
    {
        Item = item;
    }
}

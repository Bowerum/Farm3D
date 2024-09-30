using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType { Empty, Food, Material, Instrument, Seed }

[Serializable]
public class Item
{
    public Instrument Instrument;

    public Food Food;

    public Material Material;

    public Seed Seed;

    public string Name;
    public string SpriteUrl;
    public string PrefabUrl;

    public ItemType ItemType;

    public int Id;

    public int CostOfSale;
    public int CostOfPurchase;

    public Item(int id,string name, int costOfSale, int costOfPurchase, ItemType itemType)
    {
        Id = id;
        Name = name;
        CostOfSale = costOfSale;
        CostOfPurchase = costOfPurchase;

        if (Paths.PathsObjects.ContainsKey(new KeyValuePair<ItemType, int>(itemType, Id)))
        {
            SpriteUrl = Paths.PathsObjects[new KeyValuePair<ItemType, int>(itemType, Id)][0];

            PrefabUrl = Paths.PathsObjects[new KeyValuePair<ItemType, int>(itemType, Id)][1];
        }
    }
    public Item(int id, string name, int costOfSale, int costOfPurchase, Food food) : this(id, name, costOfSale, costOfPurchase, ItemType.Food)
    {
        ItemType = ItemType.Food;
        Food = food;
    }
    public Item(int id, string name, int costOfSale, int costOfPurchase, Material material) : this(id, name, costOfSale, costOfPurchase, ItemType.Material)
    {
        ItemType = ItemType.Material;
        Material = material;
    }
    public Item(int id, string name, int costOfSale, int costOfPurchase, Instrument instrument) : this(id, name, costOfSale, costOfPurchase, ItemType.Instrument)
    {
        ItemType = ItemType.Instrument;
        Instrument = instrument;
    }
    public Item(int id, string name, int costOfSale, int costOfPurchase, Seed seed) : this(id, name, costOfSale, costOfPurchase, ItemType.Seed)
    {
        ItemType = ItemType.Seed;
        Seed = seed;
    }
    public Item()
    {
        ItemType = ItemType.Empty;
    }
}


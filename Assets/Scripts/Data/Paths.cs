using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    public static Dictionary<KeyValuePair<ItemType, int>, List<string>> PathsObjects = new Dictionary<KeyValuePair<ItemType, int>, List<string>>()
    {
        [new KeyValuePair<ItemType, int>(ItemType.Instrument,1)] = new List<string> { "Sprites/shovel", "Prefabs/Items/shovel.prefab" },
        [new KeyValuePair<ItemType, int>(ItemType.Instrument,2)] = new List<string> { "Sprites/wateringCan", "Prefabs/Items/wateringCan.prefab" },
        [new KeyValuePair<ItemType, int>(ItemType.Seed,1)] = new List<string> { "Sprites/Seeds/seeds_corn", "Prefabs/Items/shovel.prefab" },
        [new KeyValuePair<ItemType, int>(ItemType.Seed, 2)] = new List<string> { "Sprites/Seeds/seeds_potato", "Prefabs/Items/shovel.prefab" },
        [new KeyValuePair<ItemType, int>(ItemType.Seed, 3)] = new List<string> { "Sprites/Seeds/seeds_strawberry", "Prefabs/Items/shovel.prefab" },
        [new KeyValuePair<ItemType, int>(ItemType.Food, 1)] = new List<string> { "Sprites/Product/products_corn", "Prefabs/Items/shovel.prefab" },
        [new KeyValuePair<ItemType, int>(ItemType.Food, 2)] = new List<string> { "Sprites/Product/products_potato", "Prefabs/Items/shovel.prefab" },
        [new KeyValuePair<ItemType, int>(ItemType.Food, 3)] = new List<string> { "Sprites/Product/products_strawberry", "Prefabs/Items/shovel.prefab" }
    };
    public static Dictionary<int,List<string>> PlantsStages = new Dictionary<int, List<string>>
    {
        [1] = new List<string> { "Prefabs/Plants/Corn/corn1", "Prefabs/Plants/Corn/corn2", "Prefabs/Plants/Corn/corn3" },
        [2] = new List<string> { "Prefabs/Plants/Potato/potato1", "Prefabs/Plants/Potato/potato2", "Prefabs/Plants/Potato/potato3" },
    };
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;

    [SerializeField] private SlotShop _slotShop;

    [SerializeField] private EventBus _eventBus;

    private static List<SlotShop> _slotsShop = new List<SlotShop>(3);

    private static List<Item> _shopItems = new List<Item>();

    private void Start()
    {
        _shopItems.Add(AllItems.Items.First(i => i.ItemType == ItemType.Seed && i.Id == 1));
        _shopItems.Add(AllItems.Items.First(i => i.ItemType == ItemType.Seed && i.Id == 2));
        _shopItems.Add(AllItems.Items.First(i => i.ItemType == ItemType.Seed && i.Id == 3));

        for (int i = 0; i < _slotsShop.Capacity; i++)
        {
            Instantiate(_slotShop, _shopPanel.transform);
        }

        _slotsShop = _shopPanel.GetComponentsInChildren<SlotShop>().ToList();

        AddItems();

       _shopPanel.SetActive(false);
    }
    public static void AddItems()
    {
        for (int i = 0; i < _slotsShop.Count; i++)
        {
            if (i < _shopItems.Count)
                _slotsShop[i].FillShopSlot(_shopItems[i]);
        }
    }
}

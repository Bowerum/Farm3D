using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IEventReceiver<ChangeItemEvent>
{
    public static List<ItemInventory> ItemsInventory = new List<ItemInventory>(17);

    [SerializeField] private EventBus _eventBus;

    [SerializeField] private GameObject _message;
    [SerializeField] private GameObject _button;

    public static bool IsNewGame = true;

    public static string InventoryString;

    private void OnEnable()
    {
        _eventBus.Register(this as IEventReceiver<ChangeItemEvent>);
    }
    private void OnDisable()
    {
        _eventBus.UnRegister(this as IEventReceiver<ChangeItemEvent>);
    }
    private void Start()
    {
        for (int i = 0; i < ItemsInventory.Capacity; i++)
        {
            ItemsInventory.Add(new ItemInventory(new Item()));
        }
        if (IsNewGame)
        {
            _message.SetActive(true);
            _button.SetActive(true);

            ItemsInventory[0] = new ItemInventory(AllItems.Items.First(item => item.ItemType == ItemType.Instrument && item.Id == 1), 1);
            ItemsInventory[1] = new ItemInventory(AllItems.Items.First(item => item.ItemType == ItemType.Instrument && item.Id == 2),1);
            ItemsInventory[2] = new ItemInventory(AllItems.Items.First(item => item.ItemType == ItemType.Seed && item.Id == 1),2);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            StartCoroutine(LoadInventory());
        }
    }

    private IEnumerator LoadInventory()
    {
        yield return new WaitForSeconds(2);

        List<string> items = new List<string>(InventoryString.Split(';'));
        for (int i = 0; i < items.Count - 1; i++)
        {
            List<string> item = new List<string>(items[i].Split(' '));

            if ((ItemType)Enum.Parse(typeof(ItemType), item[1]) != ItemType.Empty)
            {
                ItemsInventory[int.Parse(item[0])] = new ItemInventory(AllItems.Items.First(i => i.ItemType == (ItemType)Enum.Parse(typeof(ItemType), item[1]) && i.Id == int.Parse(item[2])), int.Parse(item[3]));
                _eventBus.Raise(new ChangeSlotEvent(int.Parse(item[0])));
            }
        }
    }

    public void OnEvent(ChangeItemEvent @event)
    {
        if (ItemsInventory.FirstOrDefault(item => item.Item == @event.Item) == null)
        {
            for (int i = 0; i < ItemsInventory.Capacity; i++)
            {
                if (ItemsInventory[i].Item.ItemType == ItemType.Empty)
                {
                    ItemsInventory[i].Item = @event.Item;
                    ItemsInventory[i].Count = @event.Count;

                    _eventBus.Raise(new ChangeSlotEvent(i));

                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < ItemsInventory.Capacity; i++)
            {
                if (ItemsInventory[i].Item.ItemType != ItemType.Empty)
                {
                    if (ItemsInventory[i].Item == @event.Item)
                    {
                        ItemsInventory[i].Count += @event.Count;

                        if (ItemsInventory[i].Count == 0)
                            ItemsInventory[i].Item = new Item();

                        _eventBus.Raise(new ChangeSlotEvent(i));

                        break;
                    }
                }
            }
        }
    }
}

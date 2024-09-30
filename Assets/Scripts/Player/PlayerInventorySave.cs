using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventorySave : MonoBehaviour
{
    public static string SaveInventory()
    {
        string save = "";

        for (int i = 0; i < PlayerInventory.ItemsInventory.Capacity; i++)
        {
            if (PlayerInventory.ItemsInventory[i].Item.ItemType != ItemType.Empty)
            {
                string idInventory = i.ToString();
                string type = PlayerInventory.ItemsInventory[i].Item.ItemType.ToString();
                string idItem = PlayerInventory.ItemsInventory[i].Item.Id.ToString();
                string count = PlayerInventory.ItemsInventory[i].Count.ToString();

                string saveItemString = idInventory + " " + type + " " + idItem + " " + count + ";";

                save += saveItemString;
            }
        }
       return save;
    }
   /* public static void LoadInventory()
    {
        string save = DatabaseCommunicator.LoadProgress();

        List<string> items = new List<string>(save.Split(';'));

        for (int i = 0; i < items.Count - 1; i++)
        {
            List<string> item = new List<string>(items[i].Split(' '));

            if ((ItemType)Enum.Parse(typeof(ItemType), item[1]) != ItemType.Empty)
            {
                PlayerInventory.ItemsInventory[int.Parse(item[0])] = new ItemInventory(AllItems.Items.First(i => i.ItemType == (ItemType)Enum.Parse(typeof(ItemType), item[1]) && i.Id == int.Parse(item[2])), int.Parse(item[3]));

                eventBus.Raise(new ChangeSlotEvent(int.Parse(item[0])));
                Debug.Log(int.Parse(item[0]));
            }
        }
    }*/
}

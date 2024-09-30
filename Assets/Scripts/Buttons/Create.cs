using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public void CreateSave()
    {
        AudioManager.AidioManager.Play("click");

        Save save = new Save(0, PlayerIndicators.Level, PlayerIndicators.Experience, PlayerIndicators.Money, PlayerIndicators.FoodIndicator, PlayerIndicators.FluitIndicator, PlayerInventorySave.SaveInventory(),PlayerIndicators.Score);

        DatabaseCommunicator.CreateSave(save);
    }
}

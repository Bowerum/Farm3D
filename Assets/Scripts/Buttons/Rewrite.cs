using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewrite : MonoBehaviour
{
    public int IdSave;
    public void RewriteSave()
    {
        AudioManager.AidioManager.Play("click");

        Save save = new Save(IdSave, PlayerIndicators.Level, PlayerIndicators.Experience, PlayerIndicators.Money, PlayerIndicators.FoodIndicator, PlayerIndicators.FluitIndicator,PlayerInventorySave.SaveInventory(),PlayerIndicators.Score);

        DatabaseCommunicator.RewriteSave(save);
    }
}

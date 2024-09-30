using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public int IdSave;

    public  void LoadSave()
    {
        AudioManager.AidioManager.Play("click");

        PlayerIndicators.Level = LoadSaves.SavesList[IdSave].Level;
        PlayerIndicators.Experience = LoadSaves.SavesList[IdSave].Experience;
        PlayerIndicators.FoodIndicator = LoadSaves.SavesList[IdSave].Food;
        PlayerIndicators.FluitIndicator = LoadSaves.SavesList[IdSave].Fluit;
        PlayerIndicators.Money = LoadSaves.SavesList[IdSave].Money;

        PlayerIndicators.Score = LoadSaves.SavesList[IdSave].Score;

        PlayerInventory.IsNewGame = false;

        PlayerInventory.InventoryString = LoadSaves.SavesList[IdSave].InventoryString;

        SceneManager.LoadScene(2);
    }
}

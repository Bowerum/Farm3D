using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSaves : MonoBehaviour
{
    public static List<Save> SavesList = new List<Save>();

    public void Start()
    {
        SavesList = DatabaseCommunicator.LoadSaves();

        for (int i = 0;i < SavesList.Count; i++)
        {
            GameObject save = Instantiate(Resources.Load<GameObject>("Prefabs/LoadSave"), transform);
            
            save.GetComponent<Save>().Id = SavesList[i].Id;

            save.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Сохранение\nУровень: {SavesList[i].Level}, деньги: {SavesList[i].Money}";

            save.transform.GetChild(1).GetComponent<Load>().IdSave = i;    
        }
    }

   
}

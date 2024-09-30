using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewriteSaves : MonoBehaviour
{
    public static List<Save> SavesList = new List<Save>();

    public void OnEnable()
    {
        SavesList = new List<Save>();
        SavesList = DatabaseCommunicator.LoadSaves();

        for (int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).GetComponent<Save>().DestroyObject();
        }

        for (int i = 0; i < SavesList.Count; i++)
        {     
            GameObject save = Instantiate(Resources.Load<GameObject>("Prefabs/RewriteSave"), transform);

            save.GetComponent<Save>().Id = SavesList[i].Id;
            save.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Сохранение\nУровень: {SavesList[i].Level}, деньги: {SavesList[i].Money}";
            save.transform.GetChild(1).GetComponent<Rewrite>().IdSave = SavesList[i].Id;
        }
    }
   
}
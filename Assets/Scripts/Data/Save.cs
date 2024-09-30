using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public int Id;
    public int Level;
    public int Experience;
    public int Money;
    public int Food;
    public int Fluit;
    public int Score;

    public string InventoryString;
    public Save(int id,int level,int experience,int money,int food,int fluit,string inventoryString,int score)
    {
        Id = id;
        Level = level;
        Experience = experience;
        Money = money;
        Food = food;
        Fluit = fluit;
        Score = score;

        InventoryString = inventoryString;
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

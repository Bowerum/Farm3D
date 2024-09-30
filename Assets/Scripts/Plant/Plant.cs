using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Plant
{
    public int PlantId;
    public int SeedId;
    public int ProductId;

    public int Experience;

    public bool IsReusable;

    public string NamePlant;

    public List<int> TimeStagesToGrowth = new List<int>();

    public List<string> PlantStages = new List<string>();

    public Plant(int plantId,int seedId,int productId, string timeStages, bool isReusable, string namePlant, int experience)
    {
        PlantId = plantId;
        SeedId = seedId;
        ProductId = productId;

        IsReusable = isReusable;

        NamePlant = namePlant;

        Experience = experience;

         string[] stages = timeStages.Split(',');

        foreach (string s in stages)
            TimeStagesToGrowth.Add(int.Parse(s));

        if (Paths.PlantsStages.ContainsKey(plantId))
            PlantStages = Paths.PlantsStages[plantId];
    }
}

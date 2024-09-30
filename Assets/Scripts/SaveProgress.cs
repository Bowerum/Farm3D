using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class SaveProgress : MonoBehaviour
{
    [SerializeField] Item i;
    [SerializeField] EventBus eventBus;

    private void Start()
    {
        GameObject plantStorage = Resources.Load<GameObject>("Prefabs/Plants/PlantStorage");

        GrowPlant plant = plantStorage.GetComponent<GrowPlant>();

     //   plant.Plant = AllPlants.Plants.First(p => p.SeedId == 1);

      //  Instantiate(plantStorage, new Vector3(12, 1, -10), Quaternion.identity);
    }
}

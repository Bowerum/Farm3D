using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    public Plant Plant;

    public bool IsGrown = false;

    private GameObject _plant;

    private int _stage = 0;

    private List<string> plantStages = new List<string>();

    private void Start()
    {
        plantStages = Paths.PlantsStages[Plant.PlantId];

        StartCoroutine(Growthing());
    }

    private IEnumerator Growthing()
    {
        _plant = Instantiate(Resources.Load<GameObject>(plantStages[0]), transform);

        while (_stage < Plant.TimeStagesToGrowth.Count)
        {
            yield return new WaitForSeconds(Plant.TimeStagesToGrowth[_stage]);

            _stage++;

            Destroy(_plant);

            _plant = Instantiate(Resources.Load<GameObject>(plantStages[_stage]), transform);

            if (_stage == Plant.TimeStagesToGrowth.Count)
                IsGrown = true;
        }
    }
}

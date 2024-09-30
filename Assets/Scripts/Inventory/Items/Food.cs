using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Food
{
    public int Fluit;
    public int Hungry;

    public Food(int fluit, int hungry)
    {
        Fluit = fluit;
        Hungry = hungry;
    }
}

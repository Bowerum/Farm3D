using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Seed
{
    public int LevelUnlock;
    public Seed(int levelUnlock)
    {
        LevelUnlock = levelUnlock;
    }
}

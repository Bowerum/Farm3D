using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Instrument
{
    public int LevelUnlock;
    public Instrument(int levelUnlock)
    {
        LevelUnlock = levelUnlock;
    }
}

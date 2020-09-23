using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public float MAXLevel { get; set; }
    public float ValueScale { get; set; }
    public float Value { get; set; }
    public float Level { get; set; }

    public Stat()
    {
        Level = 1;
    }
}

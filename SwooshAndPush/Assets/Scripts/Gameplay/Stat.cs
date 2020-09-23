using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    //public float MAXLevel { get; set; }
    public float LevelScale { get; set; }
    public float PriceScale { get; set; }
    public float Value { get; set; }
    public int Price { get; set; }

    private int level;
    public int Level
    {
        get => level;
        set
        {
            // Setting value according to its level
            level += value;
            Value = GenerateValue(LevelScale);
            // Setting price according to its level
            Price = (int)GenerateValue(PriceScale);
        }
    }
    
    public Stat()
    {
        Price = 1;
        PriceScale = 1;
        LevelScale = 1;
        Value = 1;
        level = 1;
    }
    
    // Using special formula to calculate value for the next level
    private float GenerateValue(float scale)
    {
        float value = 0;
        for (int i = 0; i < level; i++)
            value += Mathf.Floor(Level * Mathf.Pow(2, i / scale));
        
        return Mathf.Floor(value / 4);
    }
}

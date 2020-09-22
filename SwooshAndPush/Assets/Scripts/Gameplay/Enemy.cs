using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy
{
    // Enemy states
    public float MAXHealth;
    public float CurrentHealth;

    public readonly float MagicResistance;
    public readonly float PhysicalResistance;
    public readonly float PureResistance;
    

    public Enemy(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Casual:
                MAXHealth = LocationManager.CurrentLocation.HealthCasual;
                CurrentHealth = MAXHealth;
                break;
            case StageType.Boss:
                MAXHealth = LocationManager.CurrentLocation.HealthBoss;
                CurrentHealth = MAXHealth;
                break;

            case StageType.SuperBoss:
                MAXHealth = LocationManager.CurrentLocation.HealthSuperBoss;
                CurrentHealth = MAXHealth;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        MagicResistance = LocationManager.CurrentLocation.GetResistance(ResistanceType.Magical, stageType);
        PhysicalResistance = LocationManager.CurrentLocation.GetResistance(ResistanceType.Physical, stageType);
        PureResistance = LocationManager.CurrentLocation.GetResistance(ResistanceType.Pure, stageType);
    }
}

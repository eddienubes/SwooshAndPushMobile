using UnityEngine;
using System;
using Newtonsoft.Json;

public enum ComboType
{
    Idle, // Just tap
    BloodOcean, // Left->Left->Up->Down
    LeftRightSlash, // Left->Right->Left->Right, yeah, i know, im genius
    UpDownSlash, // Up->Down->Up->Down
    ClockwiseSlash // Up->Right->Down->Left
}

public enum PlayerAction
{
    None,
    Tap,
    SwipeUp,
    SwipeDown,
    SwipeLeft,
    SwipeRight
}

public enum StageType
{
    Casual,
    Boss,
    SuperBoss
}

public enum ResistanceType
{
    Physical,
    Pure,
    Magical
}
[System.Serializable] public class Location
{
    public string Name { get; set; } = "Loren";
    
    // Health
    [JsonRequired] private float healthMINCasual = 10000f;
    [JsonRequired] private float healthMAXCasual = 15000f;
    [JsonIgnore] public float HealthCasual => UnityEngine.Random.Range(healthMINCasual, healthMAXCasual);
    
    [JsonRequired] private float healthMINBoss = 30000f;
    [JsonRequired] private float healthMAXBoss = 35000f;
    [JsonIgnore] public float HealthBoss => UnityEngine.Random.Range(healthMINBoss, healthMAXBoss);
    
    [JsonRequired] private float healthMINSuperBoss = 45000f;
    [JsonRequired] private float healthMAXSuperBoss = 50000f;
    [JsonIgnore] public float HealthSuperBoss => UnityEngine.Random.Range(healthMINSuperBoss, healthMAXSuperBoss);

    // Resistance

    //  // Casual
    [JsonRequired] private float resCasualPhysical = 0.3f;
    [JsonRequired] private float resCasualMagical = 0.3f;
    [JsonRequired] private float resCasualPure = 0.3f;
    
    //  // Bosses
    [JsonRequired] private float resBossPhysical = 0.1f;
    [JsonRequired] private float resBossMagical = 0.6f;
    [JsonRequired] private float resBossPure = 0.3f;

    //  // SuperBosses
    [JsonRequired] private float resSuperBossPhysical = 0.3f;
    [JsonRequired] private float resSuperBossMagical = 0.3f;
    [JsonRequired] private float resSuperBossPure = 0.3f;

    //Method of getting particular resistance using enum types
    public float GetResistance(ResistanceType resistanceType, StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Casual:
                switch (resistanceType)
                {
                    case ResistanceType.Magical:
                        return resCasualMagical;
                    case ResistanceType.Physical:
                        return resCasualPhysical;
                    case ResistanceType.Pure:
                        return resCasualPure;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case StageType.Boss:
                switch (resistanceType)
                {
                    case ResistanceType.Magical:
                        return resBossMagical;
                    case ResistanceType.Physical:
                        return resBossPhysical;
                    case ResistanceType.Pure:
                        return resBossPure;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case StageType.SuperBoss:
                switch (resistanceType)
                {
                    case ResistanceType.Magical:
                        return resSuperBossMagical;
                    case ResistanceType.Physical:
                        return resSuperBossPhysical;
                    case ResistanceType.Pure:
                        return resSuperBossPure;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

[System.Serializable] public class PlayerStats
{
    // Physical dmg(tap)
    [JsonRequired] private float physicalMINTapDmg = 90.0f;
    [JsonRequired] private float physicalMAXTapDmg = 150.0f;
    [JsonIgnore] public float PhysicalTapDamage => UnityEngine.Random.Range(physicalMINTapDmg, physicalMAXTapDmg);

    [JsonRequired] private float pureMINTapDmg = 20.0f;
    [JsonRequired] private float pureMAXTapDmg = 50f;
    [JsonIgnore] public float PureTapDmg 
    {
        set
        {
            pureMINTapDmg += value;
            pureMAXTapDmg += value;
        }
        get => UnityEngine.Random.Range(pureMINTapDmg, pureMAXTapDmg);
    }
    
    // Luck
    public float LuckPhysicalCrit { get; set; } = 0.1f;
    
    // Values of crit
    [JsonRequired] private float critMultiplier = 0.10f;
    [JsonIgnore] public float CritDamage 
    {
        get => PhysicalTapDamage + PhysicalTapDamage * critMultiplier;
        set => critMultiplier += value;
    }

    // Combo Damages
    public float ComboBloodOceanDmg { get; set; } = 500.0f;
    public float ComboLeftRightSlashDmg { get; set; } = 700.0f;
    public float ComboUpDownSlashDmg { get; set; } = 200.0f;
    public float ComboClockWiseSlashDmg { get; set; } = 1000.0f;

    // Currencies
    public float GoldCoins { get; set; } = 0;
    public int Diamonds { get; set; } = 0;

    // Player Level
    public int PlayerLevel { get; set; } = 1;
    
    public PlayerStats()
    {
        
    }

}

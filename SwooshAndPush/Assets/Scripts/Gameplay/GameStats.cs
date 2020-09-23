using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public enum ComboType
{
    Idle, // Just tap
    BloodOcean, // Left->Left->Up->Down
    LeftRightSlash, // Left->Right->Left->Right, yeah, i know, im genius
    UpDownSlash, // Up->Down->Up->Down
    ClockwiseSlash // Up->Right->Down->Left
}

public enum ResistanceType
{
    Physical,
    Pure,
    Magical
}

public enum PlayerStatType
{
    PureTapDmg,
    LuckPhysicalCrit,
    CritMultiplier,
    
    ComboBloodOceanDmg,
    ComboLeftRightSlashDmg,
    ComboUpDownSlashDmg,
    ComboClockWiseSlashDmg,
    PlayerLevel
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
    [JsonRequired] private float physicalMINTapDmg;
    [JsonRequired] private float physicalMAXTapDmg;
    [JsonIgnore] public float PhysicalTapDamage
    {
        get => UnityEngine.Random.Range(physicalMINTapDmg, physicalMAXTapDmg);
        set
        {
            physicalMINTapDmg += value;
            physicalMAXTapDmg += value;
        }
    }
    
    // Values of crit
    [JsonIgnore] public float CritDamage => PhysicalTapDamage + PhysicalTapDamage * Stats[(int)PlayerStatType.CritMultiplier].Value;

    // Currencies    
    public float GoldCoins { get; set; } = 99999999999;
    public float Diamonds { get; set; } = 0;
    
    // Stats list (Contains luck, crit, player level etc.)
    // Described in the constructor.  
    // Set of the list capacity according to the quantity of the player stats 
    [JsonRequired] public Stat[] Stats = new Stat[Enum.GetNames(typeof(PlayerStatType)).Length];
    
    public PlayerStats()
    {
        for (int i = 0; i < Enum.GetNames(typeof(PlayerStatType)).Length; i++)
        {
            Stats[i] = new Stat();
        }
        // Assign of the private vars used in calculations
        // Physical Tap Damage
        physicalMINTapDmg = 90f;
        physicalMAXTapDmg = 150.0f;
        
        // Assign of the default values when create new stats object(start of the new game)
        // Physical Tap Damage
        Stats[(int) PlayerStatType.PureTapDmg].Value = 50f;
        
        // Luck
        Stats[(int) PlayerStatType.LuckPhysicalCrit].Value = 0.10f;
        
        // Values of crit
        Stats[(int) PlayerStatType.CritMultiplier].Value = 0.10f;
        
        // Combo Damages
        Stats[(int) PlayerStatType.ComboBloodOceanDmg].Value = 500f;
        Stats[(int) PlayerStatType.ComboLeftRightSlashDmg].Value = 700f;
        Stats[(int) PlayerStatType.ComboUpDownSlashDmg].Value = 200f;
        Stats[(int) PlayerStatType.ComboClockWiseSlashDmg].Value = 1000f;
        
        // Player Level
        Stats[(int) PlayerStatType.PlayerLevel].LevelScale = 15f;
        
        // I use exponential increasing for prices, damages, etc.
        // So, in order to differentiate values I assign different scales for each of those
        // Physical Tap 
        Stats[(int) PlayerStatType.PureTapDmg].LevelScale = 15f;
        Stats[(int) PlayerStatType.PureTapDmg].PriceScale = 15f;
        
        // Luck
        Stats[(int) PlayerStatType.LuckPhysicalCrit].LevelScale = 15f;
        Stats[(int) PlayerStatType.LuckPhysicalCrit].PriceScale = 15f;

        // Values of crit
        Stats[(int) PlayerStatType.CritMultiplier].LevelScale = 15f;
        Stats[(int) PlayerStatType.CritMultiplier].PriceScale = 15f;

        // Combo Damages
        Stats[(int) PlayerStatType.ComboBloodOceanDmg].LevelScale = 15f;
        Stats[(int) PlayerStatType.ComboLeftRightSlashDmg].LevelScale = 15f;
        Stats[(int) PlayerStatType.ComboUpDownSlashDmg].LevelScale = 15f;
        Stats[(int) PlayerStatType.ComboClockWiseSlashDmg].LevelScale = 15f;
        
        Stats[(int) PlayerStatType.ComboBloodOceanDmg].PriceScale = 15f;
        Stats[(int) PlayerStatType.ComboLeftRightSlashDmg].PriceScale = 15f;
        Stats[(int) PlayerStatType.ComboUpDownSlashDmg].PriceScale = 15f;
        Stats[(int) PlayerStatType.ComboClockWiseSlashDmg].PriceScale = 15f;
        
        // Player Level
        Stats[(int) PlayerStatType.PlayerLevel].PriceScale = 15f;
        Stats[(int) PlayerStatType.PlayerLevel].LevelScale = 1;

        // Set of the default prices
        // Physical Tap Damage
        Stats[(int) PlayerStatType.PureTapDmg].Price = 150;
        
        // Luck
        Stats[(int) PlayerStatType.LuckPhysicalCrit].Price = 150;
        
        // Values of crit
        Stats[(int) PlayerStatType.CritMultiplier].Price = 150;
        
        // Combo Damages
        Stats[(int) PlayerStatType.ComboBloodOceanDmg].Price = 150;
        Stats[(int) PlayerStatType.ComboLeftRightSlashDmg].Price = 150;
        Stats[(int) PlayerStatType.ComboUpDownSlashDmg].Price = 150;
        Stats[(int) PlayerStatType.ComboClockWiseSlashDmg].Price = 150;
        
        // Player Level
        Stats[(int) PlayerStatType.PlayerLevel].Price = 150;
    }
}

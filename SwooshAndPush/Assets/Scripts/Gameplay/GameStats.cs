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
    [JsonRequired] private Stat physicalMINTapDmg = new Stat();
    [JsonRequired] private Stat physicalMAXTapDmg = new Stat();
    [JsonIgnore] public float PhysicalTapDamage => UnityEngine.Random.Range(physicalMINTapDmg.Value, physicalMAXTapDmg.Value);
    
    // Pure Tap Damage
    [JsonRequired] public Stat PureTapDmg = new Stat();

    // Luck
    [JsonRequired] public Stat LuckPhysicalCrit = new Stat();
    
    // Values of crit
    [JsonRequired] public Stat CritMultiplier = new Stat();
    
    [JsonIgnore] public float CritDamage 
    {
        get => PhysicalTapDamage + PhysicalTapDamage * CritMultiplier.Value;
        set => CritMultiplier.Value += value;
    }

    // Combo Damages
    public Stat ComboBloodOceanDmg = new Stat();
    public Stat ComboLeftRightSlashDmg = new Stat();
    public Stat ComboUpDownSlashDmg = new Stat();
    public Stat ComboClockWiseSlashDmg = new Stat();
    
    // Currencies    
    public float GoldCoins { get; set; } = 0;
    public float Diamonds { get; set; } = 0;
    
    // Player Level
    public int PlayerLevel { get; set; } = 1;
    
    public PlayerStats()
    {
        // Assign of the default values when create new stats object(start of the new game)
        // Physical Tap Damage
        physicalMINTapDmg.Value = 90.0f;
        physicalMAXTapDmg.Value = 150.0f;
        
        // Pure Tap Damage
        PureTapDmg.Value = 50f;
        
        // Luck
        LuckPhysicalCrit.Value = 0.10f;
        
        // Values of crit
        CritMultiplier.Value = 0.10f;
        
        // Combo Damages
        ComboBloodOceanDmg.Value = 500f;
        ComboLeftRightSlashDmg.Value = 700f;
        ComboUpDownSlashDmg.Value = 200f;
        ComboClockWiseSlashDmg.Value = 1000f;
    }

}

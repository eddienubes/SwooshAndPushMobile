using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum PlayerState
{
    Idle, // Just tap
    BloodOcean, // Tap 2 times then swipe up and down
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

[System.Serializable] public class Location // TODO Copy to JSon
{
    public string name;

    // Health
    public float health_minCasual;
    public float health_maxCasual;

    public float health_minBoss;
    public float health_maxBoss;

    public float health_minSuperBoss;
    public float health_maxSuperBoss;

    // Resistance

    //  // Casual
    public float res_CasualPhysical;
    public float res_CasualMag;
    public float res_CasualPure;

    //  // Bosses
    public float res_BossPhysical;
    public float res_BossMag;
    public float res_BossPure;

    //  // SuperBosses
    public float res_SuperBossPhysical;
    public float res_SuperBossMag;
    public float res_SuperBossPure;

    public Location()
    {

    }
}

[System.Serializable] public class PlayerStats
{
    // Physical dmg(tap)
    public float phy_minTapDmg;
    public float phy_maxTapDmg;

    // Luck
    public float luck_PhysicalCrit;

    // Values of crit
    public float crit_dmg;

    // Combo Damages
    public float combo_bloodOceanDmg;
    public float combo_leftRightSlashDmg;
    public float combo_upDownSlashDmg;
    public float combo_clockWiseSlashDmg;

    // Currencies
    public float goldCoins;
    public int diamonds;

    public int playerLevel;

    public PlayerStats()
    {
        
        crit_dmg = GetCritDmg();
    }

    // Calculating Crit damage using formula
    private float GetCritDmg() => ((phy_maxTapDmg + phy_minTapDmg) / 2) * 2;
}

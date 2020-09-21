using System;

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

[Serializable] public class Location
{
    public string name = "Loren";

    // Health
    public float healthMINCasual = 10000f;
    public float healthMAXCasual = 15000f;

    public float healthMINBoss = 30000f;
    public float healthMAXBoss = 35000f;

    public float healthMINSuperBoss = 45000f;
    public float healthMAXSuperBoss = 50000f;

    // Resistance

    //  // Casual
    public float resCasualPhysical = 0.3f;
    public float resCasualMag = 0.3f;
    public float resCasualPure = 0.3f;

    //  // Bosses
    public float resBossPhysical = 0.1f;
    public float resBossMag = 0.6f;
    public float resBossPure = 0.3f;

    //  // SuperBosses
    public float resSuperBossPhysical = 0.3f;
    public float resSuperBossMag = 0.3f;
    public float resSuperBossPure = 0.3f;

    public Location()
    {

    }
}

[Serializable] public class PlayerStats
{
    // Physical dmg(tap)
    public float phyMINTapDmg = 90.0f;
    public float phyMAXTapDmg = 150.0f;

    // Luck
    public float luckPhysicalCrit = 0.1f;

    // Values of crit
    public float critDamage;

    // Combo Damages
    public float comboBloodOceanDmg = 500.0f;
    public float comboLeftRightSlashDmg = 700.0f;
    public float comboUpDownSlashDmg = 200.0f;

    public float comboClockWiseSlashDmg = 1000.0f;

    // Currencies
    public float goldCoins = 0;
    public int diamonds = 0;

    public int playerLevel = 1;

    public PlayerStats()
    {
        
        critDamage = GetCritDmg();
    }

    // Calculating Crit damage using formula
    private float GetCritDmg() => (phyMAXTapDmg + phyMINTapDmg) / 2 * 3;
}

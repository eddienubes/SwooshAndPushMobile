using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    public Location currentLocation;
    public Enemy currentEnemy;
    public HealthBarController healthBar;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private CoinManager coinManager;

    private StageType stageType;

    void Start()
    {
        currentEnemy = new Enemy();
        stageType = StageType.Casual;
        SetUpScene();
    }

    void Update()
    {
        
        UpdateScene();
        healthBar.SetHealth(currentEnemy.currentHealth);
    }

    /// <summary>
    /// Complete Loren Setup
    /// </summary>
    private void SetUpScene()
    {
        switch (stageType)
        {
            case StageType.Casual:
                currentEnemy.maxHealth = Random.Range(currentLocation.health_minCasual, currentLocation.health_maxCasual);

                currentEnemy.magicResistance = currentLocation.res_CasualMag;
                currentEnemy.physicalResistance = currentLocation.res_CasualPhysical;
                currentEnemy.pureResistance = currentLocation.res_CasualPure;

                currentEnemy.currentHealth = currentEnemy.maxHealth;
                break;

            case StageType.Boss:
                currentEnemy.maxHealth = Random.Range(currentLocation.health_minBoss, currentLocation.health_maxBoss);

                currentEnemy.magicResistance = currentLocation.res_BossMag;
                currentEnemy.physicalResistance = currentLocation.res_BossPhysical;
                currentEnemy.pureResistance = currentLocation.res_BossPure;

                currentEnemy.currentHealth = currentEnemy.maxHealth;
                break;

            case StageType.SuperBoss:
                currentEnemy.maxHealth = Random.Range(currentLocation.health_minSuperBoss, currentLocation.health_maxSuperBoss);

                currentEnemy.magicResistance = currentLocation.res_SuperBossMag;
                currentEnemy.physicalResistance = currentLocation.res_SuperBossPhysical;
                currentEnemy.pureResistance = currentLocation.res_SuperBossPure;

                currentEnemy.currentHealth = currentEnemy.maxHealth;
                break;
            default:
                break;
        }
        healthBar.SetMaxHealth(currentEnemy.currentHealth);
    }

    private void UpdateScene()
    {
        switch (stageType)
        {
            case StageType.Casual:
                if (currentEnemy.currentHealth <= 0)
                {
                    // Health resetting
                    currentEnemy.maxHealth = Random.Range(currentLocation.health_minCasual, currentLocation.health_maxCasual);
                    currentEnemy.currentHealth = currentEnemy.maxHealth;
                    healthBar.SetMaxHealth(currentEnemy.currentHealth);

                    // Give some gold
                    coinManager.SpawnCoins(5);

                    // Respawn enemy // TODO
                }
                break;
            case StageType.Boss:
                break;

            case StageType.SuperBoss:
                break;
            default:
                break;
        }
    }
}

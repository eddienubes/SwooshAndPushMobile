using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LocationManager : MonoBehaviour
{
    public Enemy CurrentEnemy;
    public Location currentLocation;
    public HealthBarController healthBar;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private CoinManager coinManager;
    
    private StageType stageType;

    void Start()
    {
        CurrentEnemy = new Enemy();
        stageType = StageType.Casual;
        SetUpScene();
    }

    void Update()
    {
        
        UpdateScene();
        healthBar.SetHealth(CurrentEnemy.CurrentHealth);
    }


    /// <summary>
    /// Complete Loren Setup
    /// </summary>
    private void SetUpScene()
    {
        switch (stageType)
        {
            case StageType.Casual:
                CurrentEnemy.MAXHealth = Random.Range(currentLocation.healthMINCasual, currentLocation.healthMAXCasual);

                CurrentEnemy.MagicResistance = currentLocation.resCasualMag;
                CurrentEnemy.PhysicalResistance = currentLocation.resCasualPhysical;
                CurrentEnemy.PureResistance = currentLocation.resCasualPure;

                CurrentEnemy.CurrentHealth = CurrentEnemy.MAXHealth;
                break;

            case StageType.Boss:
                CurrentEnemy.MAXHealth = Random.Range(currentLocation.healthMINBoss, currentLocation.healthMAXBoss);

                CurrentEnemy.MagicResistance = currentLocation.resBossMag;
                CurrentEnemy.PhysicalResistance = currentLocation.resBossPhysical;
                CurrentEnemy.PureResistance = currentLocation.resBossPure;

                CurrentEnemy.CurrentHealth = CurrentEnemy.MAXHealth;
                break;

            case StageType.SuperBoss:
                CurrentEnemy.MAXHealth = Random.Range(currentLocation.healthMINSuperBoss, currentLocation.healthMAXSuperBoss);

                CurrentEnemy.MagicResistance = currentLocation.resSuperBossMag;
                CurrentEnemy.PhysicalResistance = currentLocation.resSuperBossPhysical;
                CurrentEnemy.PureResistance = currentLocation.resSuperBossPure;

                CurrentEnemy.CurrentHealth = CurrentEnemy.MAXHealth;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        healthBar.SetMaxHealth(CurrentEnemy.CurrentHealth);
    }

    private void UpdateScene()
    {
        switch (stageType)
        {
            case StageType.Casual:
                if (CurrentEnemy.CurrentHealth <= 0)
                {
                    // Health resetting
                    CurrentEnemy.MAXHealth = Random.Range(currentLocation.healthMINCasual, currentLocation.healthMAXCasual);
                    CurrentEnemy.CurrentHealth = CurrentEnemy.MAXHealth;
                    healthBar.SetMaxHealth(CurrentEnemy.CurrentHealth);

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
                throw new ArgumentOutOfRangeException();
        }
    }
}

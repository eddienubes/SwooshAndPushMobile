using System;
using UnityEngine;
using Random = UnityEngine.Random;


public enum StageType
{
    Casual,
    Boss,
    SuperBoss
}

public class LocationManager : MonoBehaviour
{
    public static Enemy CurrentEnemy;
    public static Location CurrentLocation;
    
    public HealthBarController healthBar;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private CoinManager coinManager;
    
    private StageType stageType;

    void Start()
    {
        stageType = StageType.Casual;
        CurrentEnemy = new Enemy(stageType);
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
                    CurrentEnemy.MAXHealth = CurrentLocation.HealthCasual;
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

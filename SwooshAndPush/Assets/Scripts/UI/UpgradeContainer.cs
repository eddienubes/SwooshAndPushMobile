using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeContainer : MonoBehaviour
{
    private Text[] texts;
    private Button levelUp;
    [SerializeField] private PlayerStatType stat;
    private string description;
    private void Start()
    {
        texts = GetComponentsInChildren<Text>();
        levelUp = GetComponentInChildren<Button>();
        levelUp.onClick.AddListener(LevelUp);
        description = texts[3].text;
        UpdateText();
    }
        
    private void Update()
    {
        Debug.Log(Player.PlayerStats.PhysicalTapDamage);
    }
    
    private void UpdateText()
    {
        texts[1].text = Player.PlayerStats.Stats[(int) stat].Price + "GOLD \nLevelUp"; // Current price
        texts[2].text = "Lvl. " + Player.PlayerStats.Stats[(int) stat].Level; // Current level text
        if (stat == PlayerStatType.PlayerLevel)
        {
            texts[3].text = description + " " + Player.PlayerStats.PhysicalTapDamage; // TODO

        }
        else
        {
            texts[3].text =
                string.Concat(texts[3].text + " ", Player.PlayerStats.Stats[(int) stat].Value); // Description
        }
    }

    private void LevelUp()
    {
        if (!(Player.PlayerStats.GoldCoins >= Player.PlayerStats.Stats[(int) stat].Price)) return;
        
        Player.PlayerStats.Stats[(int) stat].Level = 1;
        Player.PlayerStats.GoldCoins -= Player.PlayerStats.Stats[(int) stat].Price;
        
        UpdateText();
    }
}

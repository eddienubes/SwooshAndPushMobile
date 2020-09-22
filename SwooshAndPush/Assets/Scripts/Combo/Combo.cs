using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Player;
using System;

public class Combo 
{
    
    public readonly List<PlayerAction> CurrentComboInputs = new List<PlayerAction>();

    private readonly List<Tuple<List<PlayerAction>, ComboType, float>> allCombos = new List<Tuple<List<PlayerAction>, ComboType, float>>();

    public Combo(PlayerStats playerStats)
    {
        // Tuples of existing combos
        var bloodOcean = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeLeft,
            PlayerAction.SwipeLeft,
            PlayerAction.SwipeUp,
            PlayerAction.SwipeDown
        },  ComboType.BloodOcean, playerStats.ComboBloodOceanDmg);

        var leftRightSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeLeft,     
            PlayerAction.SwipeRight, 
            PlayerAction.SwipeLeft, 
            PlayerAction.SwipeRight
        },  ComboType.LeftRightSlash, playerStats.ComboLeftRightSlashDmg);

        var upDownSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeDown, 
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeDown
        },  ComboType.UpDownSlash, playerStats.ComboUpDownSlashDmg);

        var clockwiseSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeRight, 
            PlayerAction.SwipeDown, 
            PlayerAction.SwipeLeft
        },  ComboType.ClockwiseSlash, playerStats.ComboClockWiseSlashDmg);

        //Adding Combos to comboList
        allCombos.Add(bloodOcean);
        allCombos.Add(leftRightSlash);
        allCombos.Add(upDownSlash);
        allCombos.Add(clockwiseSlash);

    }

    // Add combo
    public void AddComboInput(PlayerAction input)
    {
        CurrentComboInputs.Add(input);
    }

    // Clearing allComboList
    public void ResetCombo()
    {
        CurrentComboInputs.Clear();
        Player.ComboType = ComboType.Idle;
        //Debug.Log("Reseted!");
    }

    // Checking if currentCombo ecuals to one of the sub-lists of allCombos and setting players state
    public bool GetPlayerStateIfComboExist(out float comboDmg)
    {
        //Debug.Log("CHECKIIIING!");
        foreach (var list in allCombos)
        {
            if (!list.Item1.SequenceEqual(CurrentComboInputs)) continue;
            
            Player.ComboType = list.Item2;
            comboDmg = list.Item3; 
            return true;
        }
        Player.ComboType = ComboType.Idle;
        comboDmg = 0;
        ResetCombo();
        return false;
    }


}

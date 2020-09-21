using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Player;
using System;

public class Combo 
{
    
    public readonly List<PlayerAction> CurrentComboInputs = new List<PlayerAction>();

    private readonly List<Tuple<List<PlayerAction>, PlayerState, float>> allCombos = new List<Tuple<List<PlayerAction>, PlayerState, float>>();

    public Combo(PlayerStats playerStats)
    {
        // Tuples of existing combos
        var bloodOcean = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeLeft,
            PlayerAction.SwipeLeft,
            PlayerAction.SwipeUp,
            PlayerAction.SwipeDown
        },  PlayerState.BloodOcean, playerStats.comboBloodOceanDmg);

        var leftRightSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeLeft,     
            PlayerAction.SwipeRight, 
            PlayerAction.SwipeLeft, 
            PlayerAction.SwipeRight
        },  PlayerState.LeftRightSlash, playerStats.comboLeftRightSlashDmg);

        var upDownSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeDown, 
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeDown
        },  PlayerState.UpDownSlash, playerStats.comboUpDownSlashDmg);

        var clockwiseSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeRight, 
            PlayerAction.SwipeDown, 
            PlayerAction.SwipeLeft
        },  PlayerState.ClockwiseSlash, playerStats.comboClockWiseSlashDmg);

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
        Player.PlayerState = PlayerState.Idle;
        //Debug.Log("Reseted!");
    }

    // Checking if currentCombo ecuals to one of the sub-lists of allCombos and setting players state
    public bool GetPlayerStateIfComboExist()
    {
        //Debug.Log("CHECKIIIING!");
        foreach (var list in allCombos)
        {
            if (list.Item1.SequenceEqual(CurrentComboInputs))
            {
                Player.PlayerState = list.Item2;
                ComboDmg = list.Item3;
                //Debug.Log("We've found this combo, wohoooo!");
                return true;
            }
            //Debug.Log("CHEEECKING");
        }
        Player.PlayerState = PlayerState.Idle;
        ResetCombo();
        //Debug.Log("We didn't found such a combo :c");
        return false;
    }


}

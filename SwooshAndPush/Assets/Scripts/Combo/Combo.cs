using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static PlayerController;
using System;

public class Combo 
{
    
    public List<PlayerAction> currentComboInputs = new List<PlayerAction>();

    public List<Tuple<List<PlayerAction>, PlayerState, float>> allCombos = new List<Tuple<List<PlayerAction>, PlayerState, float>>();

    public Combo()
    {
        // Tuples of existing combos
        var BloodOcean = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeLeft,
            PlayerAction.SwipeLeft,
            PlayerAction.SwipeUp,
            PlayerAction.SwipeDown
        },  PlayerState.BloodOcean, 500f);

        var LeftRightSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeLeft, 
            PlayerAction.SwipeRight, 
            PlayerAction.SwipeLeft, 
            PlayerAction.SwipeRight
        },  PlayerState.LeftRightSlash, 700f);

        var UpDownSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeDown, 
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeDown
        },  PlayerState.UpDownSlash, 200f);

        var ClockwiseSlash = Tuple.Create(new List<PlayerAction>
        {
            PlayerAction.SwipeUp, 
            PlayerAction.SwipeRight, 
            PlayerAction.SwipeDown, 
            PlayerAction.SwipeLeft
        },  PlayerState.ClockwiseSlash, 1000f);

        //Adding Combos to comboList
        allCombos.Add(BloodOcean);
        allCombos.Add(LeftRightSlash);
        allCombos.Add(UpDownSlash);
        allCombos.Add(ClockwiseSlash);

    }

    // Add combo
    public void AddComboInput(PlayerAction input)
    {
        currentComboInputs.Add(input);
    }

    // Clearing allComboList
    public void ResetCombo()
    {
        currentComboInputs.Clear();
        playerState = PlayerState.Idle;
        //Debug.Log("Reseted!");
    }

    // Checking if currentCombo ecuals to one of the sub-lists of allCombos and setting players state
    public bool GetPlayerStateIfComboExist()
    {
        //Debug.Log("CHECKIIIING!");
        foreach (var list in allCombos)
        {
            if (list.Item1.SequenceEqual(currentComboInputs))
            {
                playerState = list.Item2;
                comboDmg = list.Item3;
                //Debug.Log("We've found this combo, wohoooo!");
                return true;
            }
            //Debug.Log("CHEEECKING");
        }
        playerState = PlayerState.Idle;
        ResetCombo();
        //Debug.Log("We didn't found such a combo :c");
        return false;
    }


}

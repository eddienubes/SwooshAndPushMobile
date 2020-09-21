using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DamageIdicator
{
    Raw,
    Crit,
    Ability
}

public class CombatText
{
    public bool IsActive;
    public GameObject GO;
    public Text Txt;
    public Vector2 Motion = Vector2.up * 80;
    public Vector3 Offset;

    private const float Duration = 2.2f;
    
    private float lastShow;

    public void Show()
    {
        IsActive = true;
        lastShow = Time.time;
        GO.SetActive(true);
    }

    private void Hide()
    {
        IsActive = false;
        GO.SetActive(false);
    }

    public void UpdateCombatText()
    {
        if (!IsActive)
            return;

        if (Time.time - lastShow > Duration)
        {
            Hide();
        }
        GO.transform.position += new Vector3(Motion.x * Time.deltaTime, Motion.y * Time.deltaTime);

    }
}

public class CombatTextManager : MonoBehaviour
{
    // Values to manipulate animation of the damage
    private readonly Vector2 rawMotion = Vector2.up * 80;
    private readonly Vector2 abilityMotion = Vector2.up * 40;
    private readonly Vector2 critMotion = Vector2.up * 40;
    
    private readonly Vector3 textRawOffset = new Vector2(0, 450);
    private readonly Vector3 textAbilityOffset = new Vector3(-100, 450);
    private readonly Vector3 textCritOffset = new Vector3(100, 450);
    
    public static CombatTextManager Instance { private set; get; }

    public GameObject combatTextContainer;
    public GameObject combatTextPrefab;

    private readonly List<CombatText> combatTexts = new List<CombatText>();
    private GameObject ui;
    
    
    
    private void Start()
    {
        ui = GameObject.Find("Interface");
        Instance = this;
    }

    private void Update()
    {
        foreach(CombatText cmb in combatTexts)
        {
            cmb.UpdateCombatText();
        }
    }

    private CombatText GetCombatText()
    {
        // Trying to find empty "slot" to insert text
        CombatText cmb = combatTexts.Find(c => !c.IsActive);

        // In case we don't find - we create a new one or, otherwise, reset transform position
        if (cmb == null)
        {
            cmb = new CombatText
            {
                GO = Instantiate(combatTextPrefab, combatTextContainer.transform.position, Quaternion.identity,
                    ui.transform)
            };

            cmb.Txt = cmb.GO.GetComponent<Text>();
             
            combatTexts.Add(cmb);
        }
        else
        {
            cmb.GO.transform.position = combatTextContainer.transform.position;
        }
        return cmb;
    }

    public void Show(float damage, DamageIdicator indicator)
    {
        // Getting slot
        CombatText cmb = GetCombatText();

        // Assinging incoming damage, and rounding to 0.00 format
        cmb.Txt.text = damage.ToString("0.00");

        // Test code // Will be deleted in the future // TODO

        switch (indicator)
        {
            case DamageIdicator.Raw:
                cmb.GO.transform.position = combatTextContainer.transform.position + textRawOffset;
                cmb.Txt.color = Color.white;
                cmb.Motion = rawMotion;
                break;
            case DamageIdicator.Ability:
                cmb.GO.transform.position = combatTextContainer.transform.position + textAbilityOffset;
                cmb.Txt.color = Color.magenta;
                cmb.Motion = abilityMotion;
                break;
            case DamageIdicator.Crit:
                cmb.GO.transform.position = combatTextContainer.transform.position + textCritOffset;
                cmb.Txt.color = Color.red;
                cmb.Motion = critMotion;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        cmb.Show();
    }
}

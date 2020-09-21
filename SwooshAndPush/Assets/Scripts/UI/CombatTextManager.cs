using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText
{
    public bool IsActive;
    public GameObject GO;
    public Text Txt;
    public readonly Vector3 TextOffset = new Vector2(0, 450);
    
    private const float Duration = 2.2f;
    private readonly Vector2 motion = Vector2.up * 80;
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
        GO.transform.position += new Vector3(motion.x * Time.deltaTime, motion.y * Time.deltaTime);

    }
}

public class CombatTextManager : MonoBehaviour
{
    public static CombatTextManager Instance { set; get; }

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
            cmb = new CombatText();
            cmb.GO = Instantiate(combatTextPrefab, combatTextContainer.transform.position + cmb.TextOffset, Quaternion.identity, ui.transform);
            
            
            cmb.Txt = cmb.GO.GetComponent<Text>();
             
            combatTexts.Add(cmb);
        }
        else
        {
            cmb.GO.transform.position = combatTextContainer.transform.position + cmb.TextOffset;
        }
        return cmb;
    }

    public void Show(float damage, bool isCrit)
    {
        // Getting slot
        CombatText cmb = GetCombatText();

        // Assinging incoming damage, and rounding to 0.00 format
        cmb.Txt.text = damage.ToString("0.00");

        // Test code // Will be deleted in the future // TODO

        cmb.Txt.color = isCrit ? Color.red : Color.white;
        
        cmb.Show();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText
{
    public bool isActive;
    public GameObject go;
    public Text txt;
    public Vector2 motion = Vector2.up * 80;
    public float duration = 2.2f;
    public float lastShow;
    public Vector3 textOffset = new Vector2(0, 450);

    public void Show()
    {
        isActive = true;
        lastShow = Time.time;
        go.SetActive(true);
    }

    public void Hide()
    {
        this.isActive = false;
        go.SetActive(false);
    }

    public void UpdateCombatText()
    {
        if (!isActive)
            return;

        if (Time.time - lastShow > duration)
        {
            //Debug.Log("HIDDEN!");
            Hide();
        }
        go.transform.position += new Vector3(motion.x * Time.deltaTime, motion.y * Time.deltaTime);

    }
}

public class CombatTextManager : MonoBehaviour
{
    public static CombatTextManager Instance { set; get; }

    public GameObject combatTextContainer;
    public GameObject combatTextPrefab;

    private List<CombatText> combatTexts = new List<CombatText>();

    private void Start()
    {
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
        CombatText cmb = combatTexts.Find(c => !c.isActive);

        // In case we don't find - we create a new one or, otherwise, reset transform position
        if (cmb == null)
        {
            cmb = new CombatText();
            cmb.go = Instantiate(combatTextPrefab, combatTextContainer.transform.position + cmb.textOffset, Quaternion.identity, GameObject.Find("Interface").transform);

            cmb.txt = cmb.go.GetComponent<Text>();
            combatTexts.Add(cmb);
        }
        else
        {
            cmb.go.transform.position = combatTextContainer.transform.position + cmb.textOffset;
        }
        return cmb;
    }

    public void Show(float damage, bool isCrit)
    {
        // Getting slot
        CombatText cmb = GetCombatText();

        // Assinging incoming damage, and rounding to 0.00 format
        cmb.txt.text = damage.ToString("0.00");

        // Test code // Will be deleted in the future // TODO
        if (isCrit)
            cmb.txt.color = Color.red;
        else
            cmb.txt.color = Color.white;

        //Debug.Log("SHOWED!");
        cmb.Show();
    }
}

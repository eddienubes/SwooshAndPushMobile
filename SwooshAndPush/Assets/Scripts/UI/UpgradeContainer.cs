using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeContainer : MonoBehaviour
{
    private Text[] texts;
    
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
    }
        
    void Update()
    {
        
    }

    void UpdateText()
    {
        // Skip 0 text 'cause this is our caption
        //texts[1].
        //texts[2].
    }
}

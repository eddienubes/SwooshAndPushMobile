using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinManager coinManager;
    
    private void Awake()
    {
        coinManager = GameObject.Find("Location Manager").GetComponent<CoinManager>();
    }

    private void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("TAPPED"); // TODO 
    }
}

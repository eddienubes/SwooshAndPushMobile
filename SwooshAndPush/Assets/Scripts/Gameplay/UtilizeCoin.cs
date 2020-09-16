using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilizeCoin : MonoBehaviour
{
    private CoinManager coinManager;

    private void Awake()
    {
        coinManager = GameObject.Find("Location Manager").GetComponent<CoinManager>();
        StartCoroutine(coinManager.CountDownToDestroy(gameObject));
    }
}

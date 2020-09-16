using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private GameObject coin;
    private int coinLifeInSeconds = 5;

    private void Start()
    {
        player = GameObject.Find("Input").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("coin"))
                {
                    Debug.Log(ray.origin);
                    IncreaseGoldAndDestroyCoin(hit.collider.gameObject);
                }
            }
            
            
        }
    }

    private void IncreaseGoldAndDestroyCoin(GameObject coin)
    {
        player.playerStats.goldCoins += 100;
        // Animation
        Destroy(coin);
    }

    public IEnumerator CountDownToDestroy(GameObject coin)
    {
        int countDown = coinLifeInSeconds;
        while (countDown > 0)
        {
            yield return new WaitForSeconds(1);
            countDown--;
        }
        Debug.Log("COROUTINE!");
        IncreaseGoldAndDestroyCoin(coin);
    }
}

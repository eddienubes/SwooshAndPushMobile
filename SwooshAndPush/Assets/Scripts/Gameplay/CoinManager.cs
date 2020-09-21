using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [Header("Game objects used in the script")]
    private Player player;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject animatedCoinPrefab;
    [SerializeField] private Transform targetForCoinAnimation;
    [SerializeField] private Transform coinDropPosition;
    [SerializeField] private LocationManager locationManager;
    [SerializeField] private Text goldAmountUIText;
    [SerializeField] private Text diamondAmountUIText;



    [Space]
    [Header("Available coin : (coins to pool)")]
    [SerializeField] private int coinQuantity;
    private readonly Queue<GameObject> coinsQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimationDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimationDuration;
    [SerializeField] private Ease easeType;
    [SerializeField] private int coinsQuantityInAnimation;
    private Vector3 targetPosition;

    [Space]
    [Header("Values")]
    [SerializeField] private float goldAmount;
    [SerializeField] private float goldAmountMultiplier = 1000f;
    private const int CoinLifeInSeconds = 5;
    
    private void Awake()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(targetForCoinAnimation.position);
        PrepareCoin();
    }

    private void Start()
    {
        locationManager = GameObject.Find("Location Manager").GetComponent<LocationManager>();
        player = GameObject.Find("Input").GetComponent<Player>();
        goldAmount = (player.playerLevel * locationManager.CurrentEnemy.MAXHealth) / goldAmountMultiplier;
        
    }

    void FixedUpdate()
    {
        // Updating gold and diamond currencies
        goldAmountUIText.text = player.playerStats.goldCoins.ToString();
        diamondAmountUIText.text = player.playerStats.diamonds.ToString();

        #region Touch input
        
        // Detecting touch input for coins
        if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider == null) return;
        
        if (hit.collider.gameObject.CompareTag("coin"))
        {
            IncreaseGoldAndDestroyCoin(hit.collider.gameObject);
        }
        
        #endregion
    }


    private void AnimateCoinAndIncreaseGoldAmount(GameObject collectedCoinPosition, int amountOfCoinPrefsInAnimation)
    {
        float partOfWholeGoldAmount = goldAmount / amountOfCoinPrefsInAnimation;
        for (int i = 0; i < amountOfCoinPrefsInAnimation; i++)
        {
            // Check if there is coins in the queue
            if (coinsQueue.Count > 0)
            {
                // Dequeue coin and animate it
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);
                coin.transform.position = collectedCoinPosition.transform.position;
                
                // Animate coin
                float duration = Random.Range(minAnimationDuration, maxAnimationDuration);
                coin.transform.DOMove(targetPosition, duration)
                    .SetEase(easeType)
                    .OnComplete(() => {
                        // when coins reaches target position
                        player.playerStats.goldCoins += partOfWholeGoldAmount;
                        coin.SetActive(false);
                        coinsQueue.Enqueue(coin);
                    });
            }
        }
    }

    private void IncreaseGoldAndDestroyCoin(GameObject coin)
    {
        // Animation
        AnimateCoinAndIncreaseGoldAmount(coin, coinsQuantityInAnimation);
        Destroy(coin);
    }

    private void PrepareCoin()
    {
        GameObject coin;
        for (int i = 0; i < coinQuantity; i++)
        {
            coin = Instantiate(animatedCoinPrefab);
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }

    public IEnumerator CountDownToDestroy(GameObject coin)
    {
        int countDown = CoinLifeInSeconds;
        while (countDown > 0)
        {
            yield return new WaitForSeconds(1);
            countDown--;
        }
        IncreaseGoldAndDestroyCoin(coin);
    }

    public void SpawnCoins(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            Instantiate(coinPrefab, coinDropPosition.position, coinDropPosition.rotation);
        }
    }
}

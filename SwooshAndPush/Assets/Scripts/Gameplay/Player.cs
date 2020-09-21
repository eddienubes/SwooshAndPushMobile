using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // GameObjects and components
    [HideInInspector] public LocationManager locationManager;

    // Values
    private const int MINComboActionQuantity = 4;
    
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;
    private float touchBounds;
    
    [HideInInspector] public int playerLevel;
    public float lastActionTime = 0f, comboTime = 0.35f;
        
    // Player Action variables
    [SerializeField] private PlayerAction playerAction;
    public static PlayerState PlayerState = PlayerState.Idle;
    
    // Combo Stuff
    private Combo combo;
    public static float ComboDmg = 0f;

    // Data vars
    public PlayerStats playerStats;

    private void Start()
    {
        locationManager = GameObject.Find("Location Manager").GetComponent<LocationManager>();
        SaveSystem.LoadPlayer(out locationManager.currentLocation, out playerStats);
        combo = new Combo(playerStats);
        touchBounds = (float)(Screen.height * 0.15); // 0.15 - 300 pixels of standart 1080 x 1920 mobile screen size
        playerLevel = playerStats.playerLevel;
    }

    private void Update()
    {
        #region Calculating swipes and taps
        playerAction = PlayerAction.None;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began when CheckDeadZone(Input.touches[0].position):
                    isDragging = true;
                    playerAction = PlayerAction.Tap;
                    startTouch = Input.touches[0].position;
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    Reset();
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Calculating the distance
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touchCount > 0)
                swipeDelta = Input.touches[0].position - startTouch;
        }

        // Deadzone restrictions
        if (swipeDelta.magnitude > 125f)
        {
            // Calculating direction
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or right
                playerAction = x < 0 ? PlayerAction.SwipeLeft : PlayerAction.SwipeRight;
            }
            else
            {
                // Up or down
                playerAction = y < 0 ? PlayerAction.SwipeDown : PlayerAction.SwipeUp;
            }
            Reset();
        }
        #endregion
        FightManager();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveSystem.SavePlayer(locationManager.currentLocation, playerStats);
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SavePlayer(locationManager.currentLocation, playerStats);
    }

    // Reseting position of our startTouch var
    private void Reset()
    {
        startTouch = swipeDelta = Vector3.zero;
        isDragging = false;
    }

    // Dealing damage to the current enemy facing us
    private void DealDamage()
    {
        bool isCrit = IsCritPossible();
        float completeDamage = 0f;

        // Using weapon argument as a modification for damage
        if (PlayerState == PlayerState.Idle && playerAction == PlayerAction.Tap)
        {
            completeDamage += GetRawTapDmg();

            if (isCrit)
                completeDamage += playerStats.critDamage;

            completeDamage -= locationManager.CurrentEnemy.PhysicalResistance * completeDamage;

            CombatTextManager.Instance.Show(completeDamage, isCrit);

            locationManager.CurrentEnemy.CurrentHealth -= completeDamage;
        }

        if (PlayerState != PlayerState.Idle)
        {
            switch (PlayerState)
            {
                case PlayerState.BloodOcean:
                    locationManager.CurrentEnemy.CurrentHealth -= ComboDmg;
                    // PlayerAnimationFunc
                    break;
                case PlayerState.LeftRightSlash:
                    locationManager.CurrentEnemy.CurrentHealth -= ComboDmg;
                    // PlayerAnimationFunc
                    break;
                case PlayerState.UpDownSlash:
                    locationManager.CurrentEnemy.CurrentHealth -= ComboDmg;
                    // PlayerAnimationFunc
                    break;
                case PlayerState.ClockwiseSlash:
                    locationManager.CurrentEnemy.CurrentHealth -= ComboDmg;
                    //Debug.Log("CLOCKWISE");
                    // PlayerAnimationFunc
                    break;
                case PlayerState.Idle:
                    break;
                default:
                    break;
            }
            combo.ResetCombo();
        }
    }

    // Allowing player to use combo if he didn't pass the time barrier
    private bool IsComboAvailable() => !(Time.time - lastActionTime > comboTime);

    private void FightManager()
    {
        if (playerAction != PlayerAction.None && playerAction != PlayerAction.Tap)
        {
            lastActionTime = Time.time;
            if (IsComboAvailable())
            {
                combo.AddComboInput(playerAction);
                //Debug.Log("Added");
            }
        }
        if (combo.CurrentComboInputs.Count >= MINComboActionQuantity && !IsComboAvailable())
        {
            // Case when we don't find combo
            if (!combo.GetPlayerStateIfComboExist())
            {
                combo.ResetCombo();
                //Debug.Log("Reseted");
            }
        }

        DealDamage();

        if (!IsComboAvailable())
            combo.ResetCombo();
    }
    
    private bool CheckDeadZone(Vector2 touchPosition) => touchPosition.y < Screen.height - touchBounds && touchPosition.y > touchBounds;
    
    private float GetRawTapDmg() => UnityEngine.Random.Range(playerStats.phyMINTapDmg, playerStats.phyMAXTapDmg);
    
    private bool IsCritPossible() => UnityEngine.Random.value <= playerStats.luckPhysicalCrit;
}

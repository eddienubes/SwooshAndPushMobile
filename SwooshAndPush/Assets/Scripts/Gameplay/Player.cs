using System;
using UnityEngine;

public enum PlayerAction
{
    None,
    Tap,
    SwipeUp,
    SwipeDown,
    SwipeLeft,
    SwipeRight
}

public class Player : MonoBehaviour
{
    // Values
    private const int MINComboActionQuantity = 4;
    
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;
    private float touchBounds;
    
    [HideInInspector] public int playerLevel;
    public float lastActionTime = 0f, comboTime = 0.35f;
        
    // Player Action variables
    [SerializeField] private PlayerAction playerAction;
    public static ComboType ComboType = ComboType.Idle;
    
    // Combo Stuff
    private Combo combo;
    private DamageIdicator damageType;
    private float comboDmg = 0f;
    
    
    // Data vars
    public static PlayerStats PlayerStats;

    private void Awake()
    {
        SaveSystem.LoadPlayer();
    }

    private void Start()
    {
        combo = new Combo(PlayerStats);
        touchBounds = (float)(Screen.height * 0.15); // 0.15 - 300 pixels of standard 1080 x 1920 mobile screen size
        playerLevel = (int)PlayerStats.Stats[(int)PlayerStatType.PlayerLevel].Value;
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
            SaveSystem.SavePlayer();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SavePlayer();
    }

    // Reset position of our startTouch var
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
        if (ComboType == ComboType.Idle && playerAction == PlayerAction.Tap)
        {
            damageType = DamageIdicator.Raw;
            completeDamage += PlayerStats.PhysicalTapDamage;
            if (isCrit)
            {
                completeDamage += PlayerStats.CritDamage;
                damageType = DamageIdicator.Crit;
            }
            completeDamage -= LocationManager.CurrentEnemy.PhysicalResistance * completeDamage;

            CombatTextManager.Instance.Show(completeDamage, damageType);

            LocationManager.CurrentEnemy.CurrentHealth -= completeDamage;
        }

        if (ComboType == ComboType.Idle) return;
        
        switch (ComboType)
        {
            case ComboType.BloodOcean:
                // PlayerAnimationFunc
                break;
            case ComboType.LeftRightSlash:
                // PlayerAnimationFunc
                break;
            case ComboType.UpDownSlash:
                // PlayerAnimationFunc
                break;
            case ComboType.ClockwiseSlash:
                // PlayerAnimationFunc
                break;
            default:
                break;
        }
        CombatTextManager.Instance.Show(comboDmg, DamageIdicator.Ability);
        LocationManager.CurrentEnemy.CurrentHealth -= comboDmg;
        
        combo.ResetCombo();
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
            }
        }
        if (combo.CurrentComboInputs.Count >= MINComboActionQuantity)
        {
            // Case when we don't find combo
            if (!combo.GetPlayerStateIfComboExist(out comboDmg))
            {
                combo.ResetCombo();
            }
        }

        DealDamage();

        if (!IsComboAvailable())
            combo.ResetCombo();
    }
    private bool CheckDeadZone(Vector2 touchPosition) => touchPosition.y < Screen.height - touchBounds && touchPosition.y > touchBounds;
    
    private bool IsCritPossible() => UnityEngine.Random.value <= PlayerStats.Stats[(int)PlayerStatType.LuckPhysicalCrit].Value;
}

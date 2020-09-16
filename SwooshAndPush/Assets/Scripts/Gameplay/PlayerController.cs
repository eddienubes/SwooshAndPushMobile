using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // GameObjects and components
    public LocationManager locationManager;

    // Values
    private int minComboActionQuantity = 4;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    private float touchBounds;
    public float lastActionTime = 0f, comboTime = 0.35f;
   
    // Player Action variables
    [SerializeField] private PlayerAction playerAction;
    [SerializeField] public static PlayerState playerState = PlayerState.Idle;

    // Combo Stuff
    private Combo combo = new Combo();
    public static float comboDmg = 0f;

    // Data vars
    public PlayerStats playerStats;

    private void Start()
    {
        locationManager = GameObject.Find("Location Manager").GetComponent<LocationManager>();
        touchBounds = (float)(Screen.height * 0.15); // 0.15 - 300 pixels of standart 1080 x 1920 mobile screen size
        SaveSystem.LoadPlayer(out locationManager.currentLocation, out playerStats);
    }

    private void Update()
    {
        #region Calculating swipes and taps
        playerAction = PlayerAction.None;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (Input.touches[0].phase == TouchPhase.Began && CheckDeadZone(Input.touches[0].position))
            {
                isDraging = true;
                playerAction = PlayerAction.Tap;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }

        // Calculating the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
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
                if (x < 0)
                    playerAction = PlayerAction.SwipeLeft;
                else
                    playerAction = PlayerAction.SwipeRight;
            }
            else
            {
                // Up or down
                if (y < 0)
                    playerAction = PlayerAction.SwipeDown;
                else
                    playerAction = PlayerAction.SwipeUp;
            }
            Reset();
        }
        #endregion

        FightManager();

        Debug.Log(playerState.ToString());
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SavePlayer(locationManager.currentLocation, playerStats);
    }

    // Reseting position of our startTouch var
    private void Reset()
    {
        startTouch = swipeDelta = Vector3.zero;
        isDraging = false;
    }

    // Dealing damage to the current enemy facing us
    private void DealDamage()
    {
        bool isCrit = IsCritPossible();
        float completeDamage = 0f;

        // Using weapon argument as a modification for damage
        if (playerState == PlayerState.Idle && playerAction == PlayerAction.Tap)
        {
            completeDamage += GetRawTapDmg();

            if (isCrit)
                completeDamage += playerStats.crit_dmg;

            completeDamage -= locationManager.currentEnemy.physicalResistance * completeDamage;

            CombatTextManager.Instance.Show(completeDamage, isCrit);

            locationManager.currentEnemy.currentHealth -= completeDamage;
        }

        if (playerState != PlayerState.Idle)
        {
            switch (playerState)
            {
                case PlayerState.BloodOcean:
                    locationManager.currentEnemy.currentHealth -= comboDmg;
                    // PlayerAnimationFunc
                    break;
                case PlayerState.LeftRightSlash:
                    locationManager.currentEnemy.currentHealth -= comboDmg;
                    // PlayerAnimationFunc
                    break;
                case PlayerState.UpDownSlash:
                    locationManager.currentEnemy.currentHealth -= comboDmg;
                    // PlayerAnimationFunc
                    break;
                case PlayerState.ClockwiseSlash:
                    locationManager.currentEnemy.currentHealth -= comboDmg;
                    //Debug.Log("CLOCKWISE");
                    // PlayerAnimationFunc
                    break;
                default:
                    break;
            }
            //Debug.Log("DAMAGED BY SOME COMBO");
            //Debug.Log("Set to Idle");
            combo.ResetCombo();
        }
    }

    // Allowing player to use combo if he didn't pass the time barrier
    private bool IsComboAvailable()
    {
        if (Time.time - lastActionTime > comboTime)
            return false;
        else
            return true;
    }

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


        if (combo.currentComboInputs.Count >= minComboActionQuantity && !IsComboAvailable())
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

    private bool CheckDeadZone(Vector2 touchposition)
    {
        //// Current point data
        //PointerEventData pointer = new PointerEventData(EventSystem.current);
        //pointer.position = Camera.main.ScreenPointToRay(Input.touches[0].position);

        //// Var to store data after raycast
        //List<RaycastResult> result = new List<RaycastResult>();

        //// The process of raycasting 
        //EventSystem.current.RaycastAll(pointer, result);
        //// Checking if we touched DeadZone 
        // If we did -> return true and allow player to damage enemy otherwise return false
        //if (result.Count > 0)
        //{
        //    foreach (var it in result)
        //    {
        //        Debug.Log("Raycasting");

        //        if (it.gameObject.CompareTag("DeadZone"))
        //        {
        //            Debug.Log("In the deadzone");
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //Debug.Log("Nothing happened ((");

        //Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //if (hit.collider != null) // TODO
        //{
        //    if (hit.collider.gameObject.CompareTag("DeadZone"))
        //    {
        //        Debug.Log(ray.origin);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //return false;

        //Debug.Log($"{touchposition.x}, {touchposition.y}");

        if (touchposition.y < Screen.height - touchBounds && touchposition.y > touchBounds)
            return true;
        else
            return false;
    }

    private float GetRawTapDmg() => UnityEngine.Random.Range(playerStats.phy_minTapDmg, playerStats.phy_maxTapDmg);

    private bool IsCritPossible()
    {
        bool test = UnityEngine.Random.value <= playerStats.luck_PhysicalCrit;
        //Debug.Log("CRIT DEBUG: " + test.ToString());
        if (test)
        {
            //Debug.Log("CRIT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return true;
        }
        else
            return false;
    }


}

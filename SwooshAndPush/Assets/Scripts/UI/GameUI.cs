using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Button[] buttons;
    
    [SerializeField] private PlayerController player;

    // Animators of locations
    private Animator a_tappedLocation;
    [SerializeField] private GameObject map;

    private void Start()
    {
        // Navigate to first panel
        NavigateTo(0);
    }

    private void Update()
    {
        ChangeLocationIfTapOnMap();
        
    }

    private void ChangeLocationIfTapOnMap()
    {
        if (Input.touchCount > 0)
        {

            Vector2 touchPosition = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);


            if (Physics2D.OverlapPoint(touchPosition))
            {
                switch (Physics2D.OverlapPoint(touchPosition).name)
                {
                    case "Tropical Forest":
                        PlayCurrentTapAnimationOnMap(touchPosition, Physics2D.OverlapPoint(touchPosition).name);
                        break;
                    case "Forgotten Desert":
                        PlayCurrentTapAnimationOnMap(touchPosition, Physics2D.OverlapPoint(touchPosition).name);
                        break;
                    case "Wild Lands":
                        PlayCurrentTapAnimationOnMap(touchPosition, Physics2D.OverlapPoint(touchPosition).name);
                        break;
                    case "Pacific Island":
                        PlayCurrentTapAnimationOnMap(touchPosition, Physics2D.OverlapPoint(touchPosition).name);
                        break;
                    case "Heaven Gates":
                        PlayCurrentTapAnimationOnMap(touchPosition, Physics2D.OverlapPoint(touchPosition).name);
                        break;
                    case "Peace Land":
                        PlayCurrentTapAnimationOnMap(touchPosition, Physics2D.OverlapPoint(touchPosition).name);
                        break;
                    default:
                        Debug.Log("Default!");
                        break;

                }
                if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                {
                    Debug.Log("Tap has ended!");
                    a_tappedLocation.SetBool("HasTapEnded", true);
                    Debug.Log(Physics2D.OverlapPoint(touchPosition).gameObject.name);
                    a_tappedLocation.SetBool("isIdle", true);
                }
            }

        }
    }

    private void PlayCurrentTapAnimationOnMap(Vector2 _touchPosition, string objName)
    {
        // Write location utility function to continue animation an load next scene
        a_tappedLocation = GameObject.Find(objName).GetComponent<Animator>();
        a_tappedLocation.SetBool("isIdle", false);
        a_tappedLocation.SetBool("isTapped", true);
        //Debug.Log("Tap Animation!");
    }

   
    // Finds the panel that player pressed, and turns off everything else
    public void NavigateTo(int menuIndex)
    {
        for (int i = 0; i < panels.Length; ++i)
        {
            if (i == menuIndex)
            {
                panels[i].SetActive(true);
                buttons[i].Select();
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    public void OpenCloseMap()
    {
        bool isActive = map.gameObject.activeSelf;
        map.gameObject.SetActive(!isActive);
    }

}

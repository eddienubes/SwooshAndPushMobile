using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtomUIOpenClose : MonoBehaviour
{
    private bool isOpened = false;
    [SerializeField] private RectTransform buttomUI;
    [SerializeField] private Text buttonText;

    private const float Duration = 0.4f;

    private void Start()
    {
        StartCoroutine(Slide());  
    }

    private void Update()
    {
        
    }

    private IEnumerator Slide()
    {
        Vector3 endPosition = isOpened ? new Vector3(0, 0, 0) : new Vector3(0, -945, 0);
        buttonText.text = isOpened ? "Close" : "Open";

        var tweener = buttomUI.DOAnchorPos(endPosition, Duration).SetEase(Ease.InCirc);

        while (tweener.IsActive()) { yield return null; }

        isOpened = !isOpened;
    }


    public void SlideButtomUI()
    {
        StartCoroutine(Slide());
    }

}

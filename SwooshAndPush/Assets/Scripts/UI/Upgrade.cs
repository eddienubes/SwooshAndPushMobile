using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private Player player;
    
    private void Start()
    {
        player = GameObject.Find("Input").GetComponent<Player>();
    }

    private void Update()
    {
        
    }
    
    
}

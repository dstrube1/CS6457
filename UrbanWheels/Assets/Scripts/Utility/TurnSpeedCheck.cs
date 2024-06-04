using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSpeedCheck : MonoBehaviour
{
    public GameManager gameManager;   
    public  GameObject[] speedIndicator;    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.speed > gameManager.maxTurnSpeed *2 )
        {
            foreach(GameObject indicator in speedIndicator)
            {
                indicator.GetComponent<Renderer>().material.color = Color.red;
            }
            
            
        }
        else
        {
            foreach(GameObject indicator in speedIndicator)
            {
                indicator.GetComponent<Renderer>().material.color = Color.green;
            }
            
            
        }
    }
}

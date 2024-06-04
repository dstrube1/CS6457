//Author Jordan Esposito jesposito32@gatech.edu


using System;
using System.Collections;
using System.Collections.Generic;
using Quests;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestCompletion : MonoBehaviour
{
    private GameManager gameManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        CompleteQuest();
    }
    
    private void CompleteQuest()
    {
        if (gameManager.activeQuest != null)
        {
            gameManager.completedQuests.Add(gameManager.activeQuest);
            float ratingMod = Random.Range(.1f, .8f);
            if ((gameManager.overallRating += ratingMod) >= 5f)
            {
                gameManager.overallRating = 5f;
            }
            else
            {
                gameManager.overallRating += ratingMod;
            }

            gameManager.earnings += gameManager.activeQuest.quest.rewards;
            gameManager.activeQuest = null;
            GameObject deleteObject = GameObject.Find("Dropoff Point(Clone)").gameObject;
            Destroy(deleteObject);
        }
    }
}

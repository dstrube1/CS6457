//Author Jordan Esposito jesposito32@gatech.edu

using System;
using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestCollider : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerController pc;
     public Quest ActiveQuest;
    private GameManager gameManager;
    private string currentTurn;
    public bool isInCollider;

    void Start()
    {
        pc = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (!pc.isTurning && other.gameObject.transform.parent.name.Contains("Player"))
        {
            isInCollider = true;
            if(!pc.isTurning)
            {
                FailQuest();
            }
        }
    }
    
    
    private void FailQuest()
    { 
        ActiveQuest = gameManager.activeQuest;
        // Remove the failed quest from the GameManager
        if (gameManager.activeQuest != null)
        {
            gameManager.failedQuests.Add(ActiveQuest);
            gameManager.activeQuest = null;
            gameManager.turnDirection = null;
            ActiveQuest = null;
            gameManager.questFailed = true;
            float ratingMod = Random.Range(.1f, .8f);
            if ((gameManager.overallRating - ratingMod) <= 0f)
            {
                gameManager.overallRating = 0f;
            }
            else
            {
                gameManager.overallRating -= ratingMod;
            }
            Debug.Log("Quest failed due to incorrect turn.");
        }
    }
}

//Author Steve Deam sdeam13@gatech.edu
// other {add your name here}

using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;


public class GameManager : MonoBehaviour
{
     // City Generation and Turning variables Needed   
    public CityGenerator cityGenerator;   
    public PlayerController playerController;
    bool wasTurning = false;

    // this value needs to increment at the end of every day
    //TODO: i think health should reset every "day" or maybe trip? default to start at 100

    //Settable from playtest panel
    public int dayCount;
    public float earnings;
    public int totalTrips;
    public int health;
    public float overallRating;
    public int maxSpeed;
    public int gearsCount;
    public int minTurnSpeed;
    public int maxTurnSpeed = 30;
    public int questLength;
    public int obstaclesCount;
    public int obstaclePenalty;
    public string turnDirection;
    public bool questFailed;
    //END Settable from playtest panel
    
    public int speed;

    private VelocityReporter velocityReporter;   
    public QuestManager questManager;
    public Quest activeQuest;
    public List<Quest> completedQuests;
    public List<Quest> failedQuests;
    // Start is called before the first frame update
    void Start()
    {
        questManager = gameObject.AddComponent<QuestManager>();
        //cityGenerator.GenerateStraightSection(); // start the city!  // moved to City Generator
        velocityReporter = playerController.GetComponent<VelocityReporter>(); 
        questFailed = false;
        
        if (PlayerPrefs.GetInt("dayCount") == 0) {
            PlayerPrefs.SetInt("dayCount", 1);
            dayCount = 1;
            PlayerPrefs.SetFloat("overallRating", 5);
            PlayerPrefs.Save();
        }
        
        else{
            dayCount = PlayerPrefs.GetInt("dayCount") + 1;
        }
        earnings = PlayerPrefs.GetFloat("Earnings");
        totalTrips = PlayerPrefs.GetInt("totalTrips"); 
        overallRating = PlayerPrefs.GetFloat("overallRating");
        health = 100;
        speed = 0;
        maxTurnSpeed = 30;

    }

    // Update is called once per frame
    void Update()
    {
        //if (playerController.isTurning && !wasTurning)
        //{            
            //cityGenerator.GenerateStraightSection(playerController.GetNearestExit().ExitAnchors);
            //wasTurning = playerController.isTurning;
        //}
         //wasTurning = playerController.isTurning; 

         /* city generation on turning moved to playercontroller */ 

         speed = (int)velocityReporter.velocity.magnitude < 1 ? 0 : (int)velocityReporter.velocity.magnitude * 2 + 2;
    }


    public void levelComplete() {
        dayCount += 1;
        if (completedQuests.Count > 0)
        {
            foreach(Quest q in completedQuests)
            {
                if (q.quest.Flag == Quest.QuestFlags.Completed)
                {
                    earnings += q.quest.rewards;
                }
            }
        }
        //remove this once we confirm the top portion of the code works.
        else
        {
            earnings += 100; 
        }
        totalTrips += 1;
    }

}

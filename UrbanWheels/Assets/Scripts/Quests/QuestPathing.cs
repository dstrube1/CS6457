using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Quests
{
    public class QuestPathing : MonoBehaviour
    {
        public Quest ActiveQuest;
        public GameManager gameManager;
        private PlayerController pc;
        private bool isTurning;
        private float initialPlayerRotationY;
        private List<Transform> leftTurns = new List<Transform>(); // List to store left turns
        private List<Transform> rightTurns = new List<Transform>(); // List to store right turns
        private Transform nearestLeftTurn;
        private Transform nearestRightTurn;
        private GameObject questDropoffPrefab;
        private GameObject nextTurn;
        private int instantiationCount;
        private string prevTurnDirection;
        
        void Start()
        {
            pc = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            ActiveQuest = gameManager.activeQuest;
            initialPlayerRotationY = pc.transform.rotation.eulerAngles.y;
            questDropoffPrefab = Resources.Load<GameObject>("Prefabs/Dropoff Point");
            
            // Invoke the method to continuously update left and right turns
            InvokeRepeating("FindLeftAndRightTurns", 0f, 1f); // Update every second
        }

        void Update()
        {
            string turnDirection;
            
            if (gameManager.activeQuest != null && pc.isTurning == false)
            {                
                ActiveQuest = gameManager.activeQuest;
                
                nearestLeftTurn = leftTurns[0].transform;
                nearestRightTurn = rightTurns[0].transform;

                if (ActiveQuest.quest.Directions.Count > 0)
                {
                    nextTurn = leftTurns[0].name.Contains(ActiveQuest.quest.Directions[0]) ? 
                        nearestLeftTurn.gameObject : nearestRightTurn.gameObject;
                    // Debug.Log(ActiveQuest.quest.Directions[0]);
                    gameManager.turnDirection = ActiveQuest.quest.Directions[0];
                }

                if (nextTurn != null)
                {
                    if (!nextTurn.GetComponent<BoxCollider>() && !nextTurn.GetComponent<QuestCollider>() && instantiationCount < 1)
                    {
                        BoxCollider obx = nextTurn.AddComponent<BoxCollider>();
                        obx.center = new Vector3(0f,3f,90f);
                        obx.size = new Vector3(16f, 5f, 5f);
                        nextTurn.AddComponent<QuestCollider>();
                        instantiationCount += 1;
                    }
                }
                if (ActiveQuest.quest.Flag == Quest.QuestFlags.Completed && !isTurning && !GameObject.Find("Dropoff Point(Clone)"))
                {
                    Invoke("CreateDropoff", 4f);
                }
            }
            // Check if the player is currently turning and if there is an active quest with directions
            if (isTurning && ActiveQuest != null && ActiveQuest.quest.Flag != Quest.QuestFlags.Failed && ActiveQuest.quest.Directions.Count >= 1)
            {
                // Determine whether the turn is left or right based on the player's input
                turnDirection = pc.isRight ? "Right" : (pc.isLeft ? "Left" : "");

                // Check if the player's input matches the direction specified in the quest
                if (turnDirection == ActiveQuest.quest.Directions[0])
                {
                    // Check if the turn has completed (rotation change >= 90 degrees)
                    if (Mathf.Abs(pc.transform.rotation.eulerAngles.y - initialPlayerRotationY) >= 90f)
                    {
                        // Update the directions only once per turn
                        initialPlayerRotationY = pc.transform.rotation.eulerAngles.y;
                        prevTurnDirection = turnDirection;
                        ActiveQuest.UpdateDirections();
                        if (ActiveQuest.quest.Directions.Count == 0)
                            ActiveQuest.quest.Flag = Quest.QuestFlags.Completed;
                    }
                    instantiationCount = 0;
                    FindLeftAndRightTurns();
                }
                else if(turnDirection != ActiveQuest.quest.Directions[0])
                {
                    if (prevTurnDirection == null || prevTurnDirection != turnDirection)
                    {
                        ActiveQuest.quest.Flag = Quest.QuestFlags.Failed;
                        FailQuest();
                        instantiationCount = 0;
                    }
                }
            }

            if (gameManager.completedQuests.Count >= 3)
            {
                PlayerPrefs.SetInt("dayCount", gameManager.GetComponent<GameManager>().dayCount);
                PlayerPrefs.SetFloat("Earnings", gameManager.GetComponent<GameManager>().earnings);
                PlayerPrefs.SetInt("totalTrips", gameManager.GetComponent<GameManager>().totalTrips);
                PlayerPrefs.SetFloat("overallRating", gameManager.GetComponent<GameManager>().overallRating);
                PlayerPrefs.Save();
                SceneManager.LoadScene("End of Day");
            }
            

            // Update the isTurning variable for the next frame
            isTurning = pc.isTurning;
        }

        private void CreateDropoff()
        {
            if (!GameObject.Find(questDropoffPrefab.name+"(Clone)"))
            {
                GameObject prefabSpawn = Instantiate(questDropoffPrefab, rightTurns[0]);
                Vector3 pos = rightTurns[0].gameObject.transform.position;
                pos.x += 5f;
                prefabSpawn.transform.position = pos; 
            }
        }

        private void FailQuest()
        { 
            // Remove the failed quest from the GameManager
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

        private void FindLeftAndRightTurns()
        {
            leftTurns.Clear();
            rightTurns.Clear();

            // Find all GameObjects with the "CityBlock" tag
            GameObject[] cityBlocks = GameObject.FindGameObjectsWithTag("CityBlock");

            // Sort the cityBlocks into left and right turns based on gameObject name
            foreach (GameObject block in cityBlocks)
            {
                bool isVisisbleBlock = block.GetComponent<Renderer>().isVisible;
                if (isVisisbleBlock)
                {
                    if (block.name.Contains("Left"))
                    {
                        leftTurns.Add(block.transform);
                    }
                    else if (block.name.Contains("Right"))
                    {
                        rightTurns.Add(block.transform);
                    }
                }
                
            }
        }
    }
}

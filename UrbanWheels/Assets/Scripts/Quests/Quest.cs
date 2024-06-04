//Author Jordan Esposito jesposito32@gatech.edu

using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Quests
{
    public class Quest : MonoBehaviour
    {

        public QuestModel quest;
        public GameManager gameManager;
        
        public enum QuestFlags
        {
            Completed,
            Active,
            Failed
        }

        public struct QuestModel
        {
            public string QuestTitle { get; set; }
            public string Description { get; set; }
            public float rewards { get; set; }
            public QuestFlags Flag { get; set; }
            public float Distance { get; set; }
            public List<string> Directions { get; set; }
        }

        public Quest(string title, string desc, float rewards)
        {
            quest.QuestTitle = title;
            quest.Description = desc;
            quest.rewards = rewards;
            quest.Flag = QuestFlags.Active;
        }

        public void UpdateDirections()
        {
            if (quest.Directions.Count > 0)
            {
                quest.Directions.RemoveAt(0); // Remove the first element

                if (quest.Directions.Count == 0)
                {
                    // If there are no more directions, the quest is completed
                    CompleteQuest();
                }
            }
        }

        private void CompleteQuest()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            quest.Flag = QuestFlags.Completed;
            gameManager.earnings += gameManager.GetComponent<GameManager>().activeQuest.quest.rewards;
            gameManager.completedQuests.Add(gameManager.activeQuest);
            gameManager.totalTrips += 1;
            float ratingMod = Random.Range(.3f, .6f);
            if ((gameManager.overallRating + ratingMod) >= 5f)
            {
                gameManager.overallRating = 5f;
            }
            else
            {
                gameManager.overallRating += ratingMod;
            }
            Debug.Log("Quest completed successfully.");
            gameManager.GetComponent<GameManager>().activeQuest = null;
            gameManager.turnDirection = null;
            gameManager.earnings += gameManager.GetComponent<GameManager>().activeQuest.quest.rewards;
            // Implement quest completion logic here
        }

        public void GenerateDirections()
        {

            quest.Directions = new List<string>();
            List<string> possiblePaths = new List<string> { "Left", "Right" }; 

            List<string> directions = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                int index = Random.Range(0, possiblePaths.Count);
                directions.Add(possiblePaths[index]); // Add a random direction to the list
            }

            quest.Directions = directions;
            quest.Distance = Random.Range(20f, 100f);
        }
        
    }
    
    
}
//Author Jordan Esposito jesposito32@gatech.edu

using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Quests
{
    public class QuestGenerator : MonoBehaviour
    {
        public List<Quest> questList = new();

        private void Start()
        {
            GenerateQuests();
        }

        private void GenerateQuests()
        {
            string[] passengerNames = { "Alice", "Bob", "Charlie", "David", "Eva", "Frank", "Grace", "Henry", "Ivy", "Jack" };
            string[] streetNames = { "Maple", "Oak", "Cedar", "Elm", "Pine", "Main", "Broadway", "Market", "River", "Park" };

            for (int i = 0; i < 100; i++)
            {
                string passengerName = passengerNames[Random.Range(0, passengerNames.Length)];
                string streetName = streetNames[Random.Range(0, streetNames.Length)] + " Street";

                string title = "Quest " + (i + 1);
                string description = $"Deliver {passengerName} to {streetName}";
                float rewardAmount = Random.Range(10f, 100f); // Reward amount randomly generated between 10 and 100

                Quest.QuestModel questModel = new Quest.QuestModel
                {
                    QuestTitle = title,
                    Description = description,
                    rewards = rewardAmount,
                    Flag = Quest.QuestFlags.Active
                };

                Quest quest = gameObject.AddComponent<Quest>(); 
                quest.quest = questModel; // Assign the quest model to the quest
                questList.Add(quest);
            }
        }

        public Quest GetRandomQuest()
        {
            if (questList.Count > 0)
            {
                int randomIndex = Random.Range(0, questList.Count);
                Quest randomQuest = questList[randomIndex];
                questList.RemoveAt(randomIndex);
                return randomQuest;
            }
            else
            {
                Debug.LogWarning("No quests available.");
                return null;
            }
        }
    }
}

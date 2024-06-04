//Author Jordan Esposito jesposito32@gatech.edu

using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    public List<GameObject> passengerList;
    public GameObject passenger;
    private QuestGenerator generator;
    void Start()
    {
        generator = GameObject.Find("QuestGenerator").GetComponent<QuestGenerator>();
        int randPassIndex = Random.Range(0, passengerList.Count - 1);
        Vector3 position = transform.position;
        passenger = Instantiate(passengerList[randPassIndex], position, Quaternion.Euler(new Vector3(0,90,0)));
        passenger.transform.parent = transform;
        passenger.AddComponent<Quest>();
        passenger.GetComponent<Quest>().quest = generator.GetRandomQuest().quest;
        passenger.GetComponent<Quest>().GenerateDirections();
    }
}

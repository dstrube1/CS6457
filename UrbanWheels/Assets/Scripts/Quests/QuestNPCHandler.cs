//Author Jordan Esposito jesposito32@gatech.edu

using System.Collections;
using System.Collections.Generic;
using Quests;
using Unity.VisualScripting;
using UnityEngine;

public class QuestNPCHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Collision _collision { get; private set; }
    public GameManager gameManager; 

    void OnTriggerStay(Collider col)
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject passenger = gameObject.transform.parent.gameObject;
        VelocityReporter velReporter = col.gameObject.transform.parent.GetComponent<VelocityReporter>();
        
        if (col.gameObject.transform.parent.CompareTag("Player") && gameManager.activeQuest == null)
        {
            if (velReporter.velocity.magnitude <= 1)
            {
                
                passenger.SetActive(false);
            }
            
            if (!passenger.activeSelf)
            {
                foreach (Transform t in passenger.gameObject.transform)
                {
                    if (t.gameObject.name.Contains("demo"))
                    {
                        gameManager.activeQuest = CopyComponent<Quest>(t.gameObject.GetComponent<Quest>(), gameManager.gameObject);
                        Debug.Log($"Retrieved the quest: {gameManager.GetComponent<GameManager>().activeQuest.quest.QuestTitle}");
                    }
                }
            }
        }
        
    }
    
    private T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        T copy = destination.AddComponent(type) as T;
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
    }
}
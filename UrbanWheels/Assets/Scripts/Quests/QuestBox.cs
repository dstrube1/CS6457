// //Author Jordan Esposito jesposito32@gatech.edu

// using TMPro;
// using UnityEngine;
// using UnityEngine.Serialization;
// using UnityEngine.UI;

// public class QuestBox : MonoBehaviour
// {
//     public GameObject gameManager;
//     public GameObject questBoxUI;
//     public TextMeshProUGUI questTitleText;
//     public TextMeshProUGUI questDescriptionText;
    
//     public GameManager gameManagerScript;

//     void Start()
//     {
//         if (gameManager == null)
//         {
//             Debug.LogError("GameManager object is not assigned to QuestBox script!");
//         }

//         gameManagerScript = gameManager.GetComponent<GameManager>();

//         if (gameManagerScript == null)
//         {
//             Debug.LogError("GameManagerScript component is missing on the GameManager object!");
//         }
        
//     }

//     void Update()
//     {
//         if (gameManagerScript.activeQuest != null)
//         {
//             if (!questBoxUI.activeSelf)
//                 questBoxUI.SetActive(true);

//             UpdateQuestBoxUI();
//         }
//         else
//         {
//             if (questBoxUI.activeSelf)
//                 questBoxUI.SetActive(false);
//         }
//     }

//     void UpdateQuestBoxUI()
//     {
//         questTitleText.text = gameManagerScript.activeQuest.quest.QuestTitle;
//         questDescriptionText.text = gameManagerScript.activeQuest.quest.Description;
//     }
// }
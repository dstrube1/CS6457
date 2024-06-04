using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof (Button))]
public class SaveGame : MonoBehaviour
{
    public GameObject gameManager;
    public Button saveButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData() {
        saveButton = GetComponent<Button>();
        PlayerPrefs.SetInt("dayCount", gameManager.GetComponent<GameManager>().dayCount);
        PlayerPrefs.SetFloat("Earnings", gameManager.GetComponent<GameManager>().earnings);
        PlayerPrefs.SetInt("totalTrips", gameManager.GetComponent<GameManager>().totalTrips);
        PlayerPrefs.SetFloat("overallRating", gameManager.GetComponent<GameManager>().overallRating);
        PlayerPrefs.Save();
        saveButton.interactable = false;
        Debug.Log("DayCount: " + PlayerPrefs.GetInt("dayCount"));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudDisplay : MonoBehaviour
{
    public Text earnings_Text;
    public Text health_Text;
    public Text day_Text;
    public Text rating_Text;
    public Text mph_Text;
    public Text failText;
    public Text questDescription;
    public Image questIcon;
    public Image leftSignal;
    public Image rightSignal;
    public Image failIcon;
    string earnings_display = "${0:F2}";
    string health_display = "{0}%";
    string days_display = "Day {0}";
    string rating_display = "{0:F1}";
    public GameObject gameManager;
    public GameObject activeQuest;
    private float earnings;
    private int health;
    private int dayCount;
    private float overallRating;
    private int speed;
    private string description;
    // Start is called before the first frame update
    void Start()
    {
        questIcon.enabled = false;
        leftSignal.enabled = false;
        rightSignal.enabled = false;
        failIcon.enabled = false;
        failText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // numDays = gameManager.GetComponent<GameManager>().dayCount;
        // Debug.Log(gameManager.GetComponent<GameManager>().activeQuest.quest.Description);
        earnings = gameManager.GetComponent<GameManager>().earnings;
        health = gameManager.GetComponent<GameManager>().health;
        dayCount = gameManager.GetComponent<GameManager>().dayCount;
        overallRating = gameManager.GetComponent<GameManager>().overallRating;
        speed = gameManager.GetComponent<GameManager>().speed;
        // totalTrips = gameManager.GetComponent<GameManager>().totalTrips;
        earnings_Text.text = string.Format(earnings_display, earnings);
        health_Text.text = string.Format(health_display, health);
        day_Text.text = string.Format(days_display, dayCount);
        rating_Text.text = string.Format(rating_display, overallRating);
        mph_Text.text = string.Format("{0} MPH", speed);

        if (gameManager.GetComponent<GameManager>().activeQuest != null) {
            activeQuest = gameManager.GetComponent<GameManager>().activeQuest.gameObject;
            //questDescription.text = string.Format(gameManager.GetComponent<GameManager>().activeQuest.quest.Description);
             questDescription.text = string.Format(gameManager.GetComponent<GameManager>().activeQuest.quest.Description) + " Make your next " + gameManager.GetComponent<GameManager>().activeQuest.quest.Directions[0];
            questIcon.enabled = true;
            if (gameManager.GetComponent<GameManager>().turnDirection == "Left") {
                leftSignal.enabled = true;
                rightSignal.enabled = false;
            }
            else if (gameManager.GetComponent<GameManager>().turnDirection == "Right") {
                rightSignal.enabled = true;
                leftSignal.enabled = false;
            }
        }
        else {
            questDescription.text = "";
            questIcon.enabled = false;
            leftSignal.enabled = false;
            rightSignal.enabled = false;
        }
        if (gameManager.GetComponent<GameManager>().questFailed) {
            StartCoroutine(ShowImageCoroutine());
        }

        IEnumerator ShowImageCoroutine()
        {
            // Show the image
            failIcon.enabled = true;
            failText.enabled = true;

            // Wait for 5 seconds
            yield return new WaitForSeconds(5f);

            // Hide the image after 5 seconds
            failIcon.enabled = false;
            failText.enabled = false;
            gameManager.GetComponent<GameManager>().questFailed = false;
        }
    }
}

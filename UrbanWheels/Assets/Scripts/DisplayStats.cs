using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// [RequireComponent(typeof (Text))]
public class DisplayStats : MonoBehaviour
{
    public GameManager gameManager;
    public Text dayText;
    public Text earningsText;
    public Text totalTripsText;
    public Text overallRatingText;
    public Text gameOver;
    public GameObject dayCountBanner;
    public GameObject restartButton;
    public GameObject trophy;
    // string display = "You finished Day {0}\nDaily Earnings: ${1:F2}\nTotal Trips: {0}\nRest up for tomorrow!";
    string day_display = "Day {0} Complete";
    // public Text m_Text;
    private int numDays;
    private float earnings;
    private int totalTrips;
    private float overallRating;
    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        // m_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        numDays = PlayerPrefs.GetInt("dayCount");
        earnings = PlayerPrefs.GetFloat("Earnings");
        Debug.Log(earnings);
        totalTrips = PlayerPrefs.GetInt("totalTrips");
        overallRating = PlayerPrefs.GetFloat("overallRating");
        // Set the num of levels/days to 10 so after 10, it will say you beat the game.
        earningsText.text = string.Format("${0:F2}", earnings); 
        totalTripsText.text = string.Format("Trips Completed: {0}", totalTrips);
        overallRatingText.text = string.Format("{0:F1}", overallRating);
        if (gameManager.health <= 0) {
            gameOver.gameObject.SetActive(true);
            dayText.gameObject.SetActive(false);
            dayCountBanner.SetActive(false);
            continueButton.SetActive(false);
            restartButton.SetActive(true);
            trophy.SetActive(false);
        }
        else if(numDays < 6) {
            restartButton.SetActive(false);
            continueButton.SetActive(true);
            trophy.SetActive(false);
            // m_Text.text = string.Format(display, numDays, earnings, totalTrips);
            dayText.text = string.Format(day_display, numDays); 
        }
        else {
            dayText.text = "You beat the game!";
            continueButton.SetActive(false);
            restartButton.SetActive(true);
            trophy.SetActive(true);
        }
    }
}
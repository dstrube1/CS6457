using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Button))]
public class ApplyPlaytestSettings : MonoBehaviour
{
    public GameManager gameManager;
    public DayCountInputReader dayCountInputReader;
    public EarningsInputReader earningsInputReader;
    public TotalTripsInputReader totalTripsInputReader;
    public OverallRatingInputReader overallRatingInputReader;
    public HealthInputReader healthInputReader;
    public MaxSpeedInputReader maxSpeedInputReader;
    public GearsCountInputReader gearsCountInputReader;
    public MinTurnSpeedInputReader minTurnSpeedInputReader;
    public MaxTurnSpeedInputReader maxTurnSpeedInputReader;
    public QuestLengthInputReader questLengthInputReader;
    public ObstaclesCountInputReader obstaclesCountInputReader;
    public ObstaclePenaltyInputReader obstaclePenaltyInputReader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData() {
        if(dayCountInputReader.isSet){
            gameManager.dayCount = dayCountInputReader.dayCount;
            PlayerPrefs.SetInt("dayCount", gameManager.GetComponent<GameManager>().dayCount);
        }
        if(earningsInputReader.isSet){
            gameManager.earnings = earningsInputReader.earnings;
            PlayerPrefs.SetFloat("Earnings", gameManager.GetComponent<GameManager>().earnings);
        }
        if(totalTripsInputReader.isSet){
            gameManager.totalTrips = totalTripsInputReader.totalTrips;
            PlayerPrefs.SetInt("totalTrips", gameManager.GetComponent<GameManager>().totalTrips);
        }
        if(overallRatingInputReader.isSet){
            gameManager.overallRating = overallRatingInputReader.overallRating;
            PlayerPrefs.SetFloat("overallRating", gameManager.GetComponent<GameManager>().overallRating);
        }
        if (healthInputReader.isSet){
            gameManager.health = healthInputReader.health;
            PlayerPrefs.SetFloat("health", gameManager.GetComponent<GameManager>().health);
        }
        if (maxSpeedInputReader.isSet){
            gameManager.maxSpeed = maxSpeedInputReader.maxSpeed;
            PlayerPrefs.SetFloat("maxSpeed", gameManager.GetComponent<GameManager>().maxSpeed);
        }
        if (gearsCountInputReader.isSet){
            gameManager.gearsCount = gearsCountInputReader.gearsCount;
            PlayerPrefs.SetFloat("gearsCount", gameManager.GetComponent<GameManager>().gearsCount);
        }
        if (minTurnSpeedInputReader.isSet){
            gameManager.minTurnSpeed = minTurnSpeedInputReader.minTurnSpeed;
            PlayerPrefs.SetFloat("minTurnSpeed", gameManager.GetComponent<GameManager>().minTurnSpeed);
        }
        if (maxTurnSpeedInputReader.isSet){
            gameManager.maxTurnSpeed = maxTurnSpeedInputReader.maxTurnSpeed;
            PlayerPrefs.SetFloat("maxTurnSpeed", gameManager.GetComponent<GameManager>().maxTurnSpeed);
        }
        if (questLengthInputReader.isSet){
            gameManager.questLength = questLengthInputReader.questLength;
            PlayerPrefs.SetFloat("questLength", gameManager.GetComponent<GameManager>().questLength);
        }
        if (obstaclesCountInputReader.isSet){
            gameManager.obstaclesCount = obstaclesCountInputReader.obstaclesCount;
            PlayerPrefs.SetFloat("obstaclesCount", gameManager.GetComponent<GameManager>().obstaclesCount);
        }
        if (obstaclePenaltyInputReader.isSet){
            gameManager.obstaclePenalty = obstaclePenaltyInputReader.obstaclePenalty;
            PlayerPrefs.SetFloat("obstaclePenalty", gameManager.GetComponent<GameManager>().obstaclePenalty);
        }
        
        PlayerPrefs.Save();
    }
}
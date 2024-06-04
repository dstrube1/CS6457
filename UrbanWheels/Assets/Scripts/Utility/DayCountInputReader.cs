using UnityEngine;

//Derived from here: https://www.youtube.com/watch?v=guelZvubWFY

public class DayCountInputReader : MonoBehaviour
{
    // public GameObject gameManager;
    public int dayCount;
    public bool isSet = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReadInputString(string s)
    {
        if (s != null && s.Length > 0){
            dayCount = int.Parse(s);
            isSet = true;
        }else{
            isSet = false;
        }
        // gameManager.GetComponent<GameManager>().dayCount = dayCount;
        // Debug.Log("DayCount: " + dayCount);
    }
}

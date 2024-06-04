using UnityEngine;

public class OverallRatingInputReader : MonoBehaviour
{
    public float overallRating;
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
            overallRating = float.Parse(s);
            isSet = true;
        }else{
            isSet = false;
        }
    }
}

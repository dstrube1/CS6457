using UnityEngine;

public class TotalTripsInputReader : MonoBehaviour
{
    public int totalTrips;
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
            totalTrips = int.Parse(s);
            isSet = true;
        }else{
            isSet = false;
        }
    }
}

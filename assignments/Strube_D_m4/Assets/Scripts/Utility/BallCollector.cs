using UnityEngine;

public class BallCollector : MonoBehaviour
{
    //Not for setting outside of the class, only for verifying that the value changed when a ball is collected
    public bool hasBall = false;
    
    
    public void ReceiveBall() {
        hasBall =true; 
    }
}

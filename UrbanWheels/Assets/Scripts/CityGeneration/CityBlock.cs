//Author Steve Deam sdeam13@gatech.edu

using UnityEngine;

public class CityBlock : MonoBehaviour
{
    public float orientation;
    [SerializeField]
    private ExitLocation[] Exits;  // exit locations are set to show where to connect the next block:
    public Transform[] EntryAnchors;
    
    

//for reference this is the ExitLocation class
     // public ExitLocation(float x, float z, float orientation, CityBlock connectedBlock = null)
    //{
        //this.position = new Vector3(x, 0, z);
        //this.orientation = orientation;
        //this.connectedBlock = connectedBlock;
    //}
    public Vector3 Entrance {get;set;}  // to potentially log the current city block upon entry into the block

    void Awake()
    {        
        orientation = transform.eulerAngles.y;                     
    }    

    public ExitLocation[] GetExits()
    {
        return Exits;
    }
}

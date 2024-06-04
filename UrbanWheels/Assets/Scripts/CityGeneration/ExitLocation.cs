//Author Steve Deam sdeam13@gatech.edu

using UnityEngine;

[System.Serializable]
public class ExitLocation 
{
    public Vector3 position;
    public float orientation;
    public CityBlock connectedBlock; // Reference to the connected CityBlock
    public Transform[] ExitAnchors;

    public ExitLocation(float x, float z, float orientation, CityBlock connectedBlock = null)
    {
        this.position = new Vector3(x, 0, z);
        this.orientation = orientation;
        this.connectedBlock = connectedBlock;
        
    }
}

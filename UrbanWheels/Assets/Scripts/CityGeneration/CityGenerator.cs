//Author Steve Deam sdeam13@gatech.edu


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] StraightLeftTurns;
    public GameObject[] StraightRightTurns;
    public GameObject[] StraightNoTurns;    
    public GameObject[] TIntersections;
    public GameObject[] FourWayIntersections;
    public GameObject crossingBoy;
    public GameObject cat;
    private List<GameObject> currentStraight = new List<GameObject>(); // Track the current straight section
    private List<GameObject> priorStraight = new List<GameObject>(); // Track the current straight section
    private GameObject[] allStraights;
    
    void Awake()
    {
        allStraights = CombineArrays(StraightLeftTurns, StraightRightTurns, StraightNoTurns,FourWayIntersections);  // get all straight sections
        StartCoroutine(GenerateStraightSection());        
    }   
    public IEnumerator GenerateStraightSection(Transform[] PriorExitAnchors = null)
    {
        
       if( currentStraight.Count >0)  // reset the current straight section if it is not empty
       {            
           priorStraight = new List<GameObject>(currentStraight);
           currentStraight.Clear();
           DeleteOldCityBlocks();
       }
        
       int straightLength =  Random.Range(8, 20); // Determine random length for the straight section
       GameObject lastBlock = null;

       for(int i = 0;i<=straightLength;i++)
       {
            GameObject selectedBlockPrefab = new GameObject();
            if(i == 0)
            {
                selectedBlockPrefab = StraightNoTurns[0];
            }else{
                selectedBlockPrefab = allStraights[Random.Range(0, allStraights.Length)];       
            }
           GameObject newBlock;     
            
           if(i == 0 && PriorExitAnchors == null)// first block of the game place at 0,0,0 and no rotation
           {
               newBlock = Instantiate(selectedBlockPrefab, Vector3.zero, Quaternion.identity);
           }           
           //else if(i == 0) // first block of a section after a turn, place at the last exit of the previous section
           //{
           //newBlock = PlaceAndOrientChunk(selectedBlockPrefab, PriorExitAnchors);
           //currentStraight.Add(newBlock); 
           //LastBlock = newBlock;
           //}
           //else // continuation of the current section, place at the last exit of the last block
           //{
           //newBlock = PlaceAndOrientChunk(selectedBlockPrefab, LastBlock.GetComponent<CityBlock>().GetExits()[0].ExitAnchors);
           //currentStraight.Add(newBlock); 
           //LastBlock = newBlock;
           //}
           else
           {
               newBlock = PlaceAndOrientChunk(selectedBlockPrefab, i == 0 ? PriorExitAnchors : lastBlock.GetComponent<CityBlock>().GetExits()[0].ExitAnchors);
           }
           newBlock.transform.SetParent(this.transform);
           currentStraight.Add(newBlock);
           AddObstacles(newBlock);
           lastBlock = newBlock;
       }
       yield return null;        
    }

    public GameObject PlaceAndOrientChunk(GameObject chunkPrefab, Transform[] PriorExitAnchors)
    {
        

       // Instantiate the new chunk
        GameObject newChunk = Instantiate(chunkPrefab);

        if(PriorExitAnchors != null)
        {
            // Get entry anchors from the instantiated chunk
            Transform entryAnchor1 = newChunk.GetComponent<CityBlock>().EntryAnchors[0];
            Transform entryAnchor2 = newChunk.GetComponent<CityBlock>().EntryAnchors[1];

            // Calculate the direction vectors in world space
            Vector3 exitDirection = (PriorExitAnchors[1].position - PriorExitAnchors[0].position).normalized;
            exitDirection.y = 0; // Only rotate on the y axis
            Vector3 entryDirection = (entryAnchor2.position - entryAnchor1.position).normalized;
            entryDirection.y = 0; // Only rotate on the y axis

            // Determine the rotation needed to align the new chunk
            // From to sometimes flips x 180?  //Quaternion rotationDifference = Quaternion.FromToRotation(entryDirection, exitDirection);
            
            float angle = Vector3.SignedAngle(entryDirection, exitDirection, Vector3.up);


            // Apply the rotation
            newChunk.transform.Rotate(0,angle,0,Space.World); //Quaternion rotationDifference * newChunk.transform.rotation;

            // Correctly calculate the position adjustment using the newly applied rotation
            Vector3 positionAdjustment = PriorExitAnchors[0].position - newChunk.transform.TransformPoint(entryAnchor1.localPosition);

            // Apply the position adjustment
            newChunk.transform.position += positionAdjustment;
            
        }

        ///////////////////////
        ///
        ///Attempt #2 below...  
        //////////////////////

        /*//get entry anchor points for reference
        Transform entryAnchor1 = chunkPrefab.GetComponent<CityBlock>().EntryAnchors[0];
        Transform entryAnchor2 = chunkPrefab.GetComponent<CityBlock>().EntryAnchors[1];

        // calculate direction of exit
        Vector3 exitDirection = (PriorExitAnchors[1].position - PriorExitAnchors[0].position).normalized;
        //calculate direction of entry
        Vector3 entryDirection = (entryAnchor2.position - entryAnchor1.position).normalized;

        //get rotaion difference between entry and exit
        Quaternion rotationDifference = Quaternion.FromToRotation(entryDirection, exitDirection);   
        rotationDifference.x = 0; // only rotate on the y axis
        rotationDifference.z = 0; // only rotate on the y axis

        GameObject newChunk = Instantiate(chunkPrefab); // instantiate the new chunk
        newChunk.transform.rotation = rotationDifference * newChunk.transform.rotation; 
        var x = newChunk.transform.rotation;        

        // set chunk at correct location
        newChunk.transform.position += PriorExitAnchors[0].position - newChunk.transform.TransformPoint(entryAnchor1.localPosition);
        newChunk.transform.SetParent(this.transform);*/

        return newChunk;
    }

    void DeleteOldCityBlocks()
    {
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("CityBlock");
        foreach(GameObject block in allBlocks)
        {
            if(!currentStraight.Contains(block) && !priorStraight.Contains(block))
            {
                Destroy(block);
            }
        }
    }

    GameObject[] CombineArrays(params GameObject[][] arrays)
    {
        int totalLength = 0;
        foreach (var array in arrays)
        {
            if (array != null)
            {
                totalLength += array.Length;
            }
        }

        GameObject[] result = new GameObject[totalLength];
        int offset = 0;
        foreach (var array in arrays)
        {
            if (array != null)
            {
                System.Array.Copy(array, 0, result, offset, array.Length);
                offset += array.Length;
            }
        }

        return result;
    }

    public void AddObstacles(GameObject newChunk)
    {
        Vector3 cityArea = newChunk.transform.position;
        // Need to set x and y values but get a random z value to get a random position along the city block
        float randomZValue = cityArea.z + Random.Range(-cityArea.z/2f, cityArea.z/2f);
        float[] sideOfStreet = {8.3f, -9.4f};
    
        Vector3 boyPos = new Vector3(sideOfStreet[Random.Range(Mathf.RoundToInt(0), Mathf.RoundToInt(2))], 0f, randomZValue);
        GameObject crossingBoyObject = Instantiate(crossingBoy, boyPos, Quaternion.identity);
        crossingBoyObject.transform.SetParent(newChunk.transform);
        crossingBoyObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

        // Add a few that just walk down the sidewalk and dont cross
        float randomZValue2 = cityArea.z + Random.Range(-cityArea.z/2f, cityArea.z/2f);
        Vector3 boyPos2 = new Vector3(sideOfStreet[Random.Range(Mathf.RoundToInt(0), Mathf.RoundToInt(2))], 0f, randomZValue2);
        GameObject walkingBoyObject = Instantiate(crossingBoy, boyPos2, Quaternion.identity);  
        walkingBoyObject.transform.SetParent(newChunk.transform);

        float randomZValueCat = cityArea.z + Random.Range(-cityArea.z/2f, cityArea.z/2f);
        int i = Random.Range(Mathf.RoundToInt(0), Mathf.RoundToInt(2));
        float rotator = i == 8.3f ? -90f : 90f;
        //Vector3 catPos = new Vector3(sideOfStreet[i], 0f, randomZValueCat);
        //GameObject catObject = Instantiate(cat, catPos, Quaternion.identity);
        //catObject.transform.SetParent(newChunk.transform);
        //catObject.transform.rotation = Quaternion.Euler(0f, rotator, 0f); 
    }
}


    



   /*public void GenerateStraightSection(ExitLocation startExit = null)
    {
        int straightLength = Random.Range(3, 11); // Determine random length for the straight section

        GameObject[] allStraights = CombineArrays(StraightLeftTurns, StraightRightTurns, StraightNoTurns,FourWayIntersections);
        Vector3 position = startExit != null ? startExit.position : Vector3.zero;
        float orientation = startExit != null ? startExit.orientation : 0f;

        for (int i = 0; i < straightLength; i++)
        {
            GameObject selectedBlockPrefab = allStraights[Random.Range(0, allStraights.Length)];
            GameObject newBlock = PlaceAndOrientChunk(selectedBlockPrefab, position, orientation);
            currentStraight.Add(newBlock); // Add to the current straight list

            CityBlock cityBlockComponent = newBlock.GetComponent<CityBlock>();
            if (cityBlockComponent != null && cityBlockComponent.GetExits().Length > 0)
            {
                ExitLocation exit = cityBlockComponent.GetExits()[0];
                position += Quaternion.Euler(0, orientation, 0) * exit.position; // Update position for the next block
                orientation += exit.orientation; // Update orientation for the next block
            }
        }
    }

    
    
  public GameObject PlaceAndOrientChunk(GameObject chunkPrefab, Vector3 currentPosition, float priorOrientation)
{
    Vector3 newPosition = currentPosition + Quaternion.Euler(0, priorOrientation, 0) * new Vector3(0, 0, 10); // Example offset
    float newOrientation = priorOrientation;

    GameObject newChunk = Instantiate(chunkPrefab, newPosition, Quaternion.Euler(0, newOrientation, 0));
    newChunk.transform.SetParent(this.transform);




    Vector3 cityArea = newChunk.transform.position;
    
    
    
    
    
    // Need to set x and y values but get a random z value to get a random position along the city block
    float randomZValue = cityArea.z + Random.Range(-cityArea.z/2f, cityArea.z/2f);
    float[] sideOfStreet = {8.3f, -9.4f};
    
    Vector3 boyPos = new Vector3(sideOfStreet[Random.Range(Mathf.RoundToInt(0), Mathf.RoundToInt(2))], 0f, randomZValue);
    GameObject crossingBoyObject = Instantiate(crossingBoy, boyPos, Quaternion.identity);
    crossingBoyObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

    // Add a few that just walk down the sidewalk and dont cross
    float randomZValue2 = cityArea.z + Random.Range(-cityArea.z/2f, cityArea.z/2f);
    Vector3 boyPos2 = new Vector3(sideOfStreet[Random.Range(Mathf.RoundToInt(0), Mathf.RoundToInt(2))], 0f, randomZValue2);
    GameObject walkingBoyObject = Instantiate(crossingBoy, boyPos2, Quaternion.identity);  

    float randomZValueCat = cityArea.z + Random.Range(-cityArea.z/2f, cityArea.z/2f);
    int i = Random.Range(Mathf.RoundToInt(0), Mathf.RoundToInt(2));
    float rotator = i == 8.3f ? -90f : 90f;
    Vector3 catPos = new Vector3(sideOfStreet[i], 0f, randomZValueCat);
    GameObject catObject = Instantiate(cat, catPos, Quaternion.identity);
    catObject.transform.rotation = Quaternion.Euler(0f, rotator, 0f); 


    
    CityBlock cityBlockComponent = newChunk.GetComponent<CityBlock>();
    if (cityBlockComponent != null)
    {        
        cityBlockComponent.orientation = newOrientation;       
        foreach (ExitLocation exit in cityBlockComponent.GetExits())
        {
            exit.connectedBlock = cityBlockComponent; // Set the reference to the connected block
        }
    }

    return newChunk; // Return the newly instantiated chunk
}*/
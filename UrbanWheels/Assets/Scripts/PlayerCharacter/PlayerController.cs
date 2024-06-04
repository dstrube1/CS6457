//Author Steve Deam sdeam13@gatech.edu

using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public UrbanWheelsControls playerControls;
    public Vector2 moveInput;
    // basic driving functionality
    public float maxSpeed = 60f;
    public float maxGearSpeed;
    public float numberOfGears = 5f;
    public float currentGear = 1f;    
    public float currentSpeed = 0f;
    public float acceleration = 1f;
    public float brakingForce;
    public float searchRadius = 50f;

    // turning functionality
    public Collider playerCollider;
    public float maxTurnSpeed = 20f;
    public bool isTurning = false;
    public TurnValue turnDirection = TurnValue.NoTurn;
    Transform TurnTarget;
    ExitLocation nearestExit;
    CityGenerator cityGenerator;
    GameManager gameManager;
    public bool isLeft, isRight, isEarly, isPerfect, isLate;

    public float turnRemaining = 90f;
    Quaternion startRotation;
    
    //public GameObject city;

    public enum TurnValue
    {
        NoTurn,
        TurnLeftEarly,
        TurnLeftPerfect,
        TurnLeftLate,
        TurnRightEarly,
        TurnRightPerfect,
        TurnRightLate
    }

    
    // constructor for instantiation from another script
    public PlayerController(
        float MaxSpeed = 50f, 
        float NumberOfGears = 5f,         
        float Acceleration = 10f)
    {
        maxSpeed = MaxSpeed;
        numberOfGears = NumberOfGears;
        acceleration = Acceleration;
        maxGearSpeed = maxSpeed / numberOfGears;
        currentGear = 1f;
        currentSpeed = 0f;
    }
    
    private void Awake()
    {
        // set up input controls
        playerControls = new UrbanWheelsControls();
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        playerControls.Player.ShiftUp.performed += ctx => ShiftUp();
        playerControls.Player.ShiftDown.performed += ctx => ShiftDown();
        playerControls.Player.Action.performed += ctx => CheckTurn();

        // set derived values
        maxGearSpeed = maxSpeed / numberOfGears;
        brakingForce = acceleration * 5f;
        maxTurnSpeed = maxSpeed;        
        
        //city = GameObject.Find("City");
    }


    private void Start()
    {
        playerCollider = transform.Find("ColliderBox").GetComponent<Collider>();
        cityGenerator = GameObject.Find("City").GetComponent<CityGenerator>();    
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();    
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }   

    private void OnDisable()
    {
        playerControls.Disable();
    } 

    // Update is called once per frame
    void Update()
    {
        if(isTurning)
        {
            ExecuteTurn();
        }else
        {
            ExecuteMove();
        }
    }

    private void ExecuteMove()
    {
        if (moveInput.y > 0)
        {
            if (currentSpeed < maxGearSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            else
            {
                currentSpeed = maxGearSpeed;
            }
        }
        else if (moveInput.y < 0)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= brakingForce * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0;
            }
        }        
        
        Vector3 forwardMove = transform.forward * currentSpeed * Time.deltaTime;
        Vector3 lateralMove = transform.right * moveInput.x * (currentSpeed / 2f) * Time.deltaTime;
        Vector3 move = forwardMove + lateralMove;
        
        transform.position += move;
    }

    void CheckTurn()
    {
        Debug.Log(transform.position);
        if(currentSpeed >0 && !isTurning && currentSpeed <= gameManager.maxTurnSpeed)
        {            
            TurnTarget = CheckTurnCollision();
            if(TurnTarget != null)
            {
                startRotation = transform.rotation;
                isTurning = true;                 
                SetNearestExit(TurnTarget);
                StartCoroutine(cityGenerator.GenerateStraightSection(nearestExit.ExitAnchors)); //                     
                ExecuteTurn();                           
            }           
        }
    }

    public ExitLocation GetNearestExit()
    {
        return nearestExit;
    }

    private void SetNearestExit(Transform turnTarget)
    {
        var x = turnTarget.parent.parent.parent.GetComponent<CityBlock>();
        
        foreach(var exit in x.GetExits())
        {
            if(exit.position.x <0 && turnRemaining < 0)
            { 
                nearestExit = exit;
                return;
            }
            else if (exit.position.x > 0 && turnRemaining > 0)
            {
                nearestExit = exit;
                return;
            }
        }
        nearestExit = null;
    }

    void ExecuteTurn()
    {
        if (!isTurning || TurnTarget == null)
        {
            return;
        }

    
    //    float turnStep = maxTurnSpeed * Time.deltaTime;
        float moveStep = currentSpeed * Time.deltaTime;

      //  Vector3 targetDirection = (TurnTarget.position - transform.position).normalized;
        


        /*if (turnRemaining >= 0) // Turning right
        {
            turnStep = Mathf.Min(turnStep, turnRemaining); // Ensure we don't over-turn
            transform.Rotate(0, turnStep, 0);
            turnRemaining -= turnStep; // Decrease turnRemaining
        }
        else if (turnRemaining <= 0) // Turning left
        {
            turnStep = Mathf.Min(-turnStep, -turnRemaining); // Ensure we don't over-turn
            transform.Rotate(0, turnStep, 0);
            turnRemaining += Math.Abs(turnStep); // Increase turnRemaining since it's negative
        }    
        transform.Translate(Vector3.forward * moveStep);    
        if (Mathf.Abs(turnRemaining) < 1f) // Close enough to consider the turn complete
        {
            isTurning = false;
            AlignWithTurnTarget(TurnTarget.position); // Align the player with the target position
        }*/
        
        
        Quaternion targetRotation = Quaternion.Euler(0, startRotation.eulerAngles.y + (turnRemaining > 0 ? 90 : -90), 0);
        
        float angleToTurn = maxTurnSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angleToTurn);
        
        transform.Translate(Vector3.forward * moveStep, Space.Self);

        
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            isTurning = false;
            
            AlignWithTurnTarget(TurnTarget.position);
        }


    }


    void AlignWithTurnTarget(Vector3 targetPosition)
    {        
        //var yRotation = Mathf.Round(transform.eulerAngles.y / 90f) * 90f; // Round to the nearest 90 degrees
        //transform.position = targetPosition; // Snap to target position        
        //transform.rotation = Quaternion.Euler(0, yRotation, 0); // Snap to target rotation        
         var targetRotation = Quaternion.Euler(0, Mathf.Round(transform.eulerAngles.y / 90f) * 90f, 0);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10); 
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10); 
    }   

    private Transform CheckTurnCollision()
    {   
        if(!isTurning){
            Collider[] hitColliders = Physics.OverlapBox(
                playerCollider.bounds.center, 
                playerCollider.bounds.extents, 
                playerCollider.transform.rotation,             
                LayerMask.GetMask("TurnLayer"));

            isLeft = false;
            isRight = false;
            isEarly = false;
            isPerfect = false;
            isLate = false;
            

            foreach (var hitCollider in hitColliders)  // determine the turn and value separately to allow for future potential difficulty adaptations
            {        
                if (hitCollider.CompareTag("TurnLeft"))
                {                
                    isLeft = true;
                }
                else if (hitCollider.CompareTag("TurnRight"))
                {
                    isRight = true;
                }

                if (hitCollider.name.Contains("Early"))
                {
                    isEarly = true;
                }
                else if (hitCollider.name.Contains("Perfect"))
                {
                    isPerfect = true;
                }
                else if (hitCollider.name.Contains("Late"))
                {
                    isLate = true;
                }        
            }

            if (hitColliders.Length == 0){
                Debug.Log("Empty hitColliders");
                return null;
            }
            Transform l1 = hitColliders[0].transform.parent.parent;
            Transform entry = null;
            
            if (isLeft) // process the turn and value, for now using the furthest reached as the determinant.  
            {
                turnRemaining =-90f;
                isTurning = true;
                
            } else if (isRight)
            {
                turnRemaining = 90f;
                isTurning = true;
            }   

            if (isPerfect) 
            {            
                
                entry = l1.Find("Perfect").GetChild(1).transform;
                
            }
            if (isLate)
            { 
                entry = l1.Find("Late").GetChild(1).transform;
            }            
            if (isEarly)
            {
                entry = l1.Find("Early").GetChild(1).transform;;
            }
            return entry;
        }
        return null;
    }


    private void ShiftUp()
    {
        if(currentGear < numberOfGears)
        {
            currentGear += 1;
            maxGearSpeed = maxSpeed / numberOfGears * currentGear;
        }
        
    }
    private void ShiftDown()
    {
        if(currentGear > 1)
        {
            currentGear -= 1;
            maxGearSpeed = maxSpeed / numberOfGears * currentGear;
            
        }
    }

    

    
}

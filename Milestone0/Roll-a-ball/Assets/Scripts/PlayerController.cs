using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{

    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public ParticleSystem ps;
    public TextMeshProUGUI detailsText;
    public GameObject innerNorthWall;
    public GameObject innerSouthWall;
    public GameObject innerEastWall;
    public GameObject innerWestWall;
    private bool hitInnerWall = false;
    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody>(); 
        count = 10; 
        SetCountText();
        winTextObject.SetActive(false);
        ps.Stop();
        SetDetailsText();
    }

    void OnMove (InputValue movementValue)
   {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y; 
   }

   void SetCountText() 
   {
        //Counting down instead of up
       countText.text =  "Pickups remaining: " + count.ToString();
       if (count <= 0)
       {
        winTextObject.SetActive(true);
        if (!hitInnerWall){
            //If done with skill and care, reward the player with a confetti shower
            ps.Play();
        }
        innerNorthWall.SetActive(false);
        innerSouthWall.SetActive(false);
        innerEastWall.SetActive(false);
        innerWestWall.SetActive(false);
       }
   }

    // Update is called once per frame
    void Update()
    {
        //Tried setting these to just (-)9.25, 0, z (or x, 0, (-)9.25), but the player would get still fall off the board sometimes
        //Figured that setting y to 0.5 would be a good way to keep the player on the board
        if (rb.position.x > 9.25) rb.position = new Vector3(9.25f, 0.5f, rb.position.z);
        if (rb.position.x < -9.25) rb.position = new Vector3(-9.25f, 0.5f, rb.position.z);
        if (rb.position.z > 9.25) rb.position = new Vector3(rb.position.x, 0.5f, 9.25f);
        if (rb.position.z < -9.25) rb.position = new Vector3(rb.position.x, 0.5f, -9.25f);
        SetDetailsText();
    }

    private void FixedUpdate() {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        rb.AddForce(movement * speed); 
    }

    void SetDetailsText(){
        Vector3 pos = rb.position;
        detailsText.text = "X: " + pos.x + "\nZ: " + pos.z;
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count--;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("InnerWall")) 
        {
            hitInnerWall = true;
            
            innerNorthWall.transform.position = new Vector3(random.Next(-10, 10), 0, random.Next(-10, 10));
            innerSouthWall.transform.position = new Vector3(random.Next(-10, 10), 0, random.Next(-10, 10));
            innerEastWall.transform.position = new Vector3(random.Next(-10, 10), 0, random.Next(-10, 10));
            innerWestWall.transform.position = new Vector3(random.Next(-10, 10), 0, random.Next(-10, 10));
        }
    }
}

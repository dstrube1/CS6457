using UnityEngine;

public class RandomMover : MonoBehaviour
{
    protected Rigidbody rbody;
    protected System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        //Initial rbody
        rbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Three blind dice
        int moveDie = random.Next(0, 10);
        int forceDie = random.Next(0, 20);
        int directionDie = random.Next(0, 5);

        //Jump at random intervals if the jumping bean is on the ground
        if (moveDie < 4 && rbody.position.y < 0.6f)
        {
            switch (directionDie)
            {
                case 0:
                    rbody.AddForce(Vector3.up * forceDie * 2, ForceMode.Impulse);
                    break;
                case 1:
                    rbody.AddForce(Vector3.right * forceDie, ForceMode.Impulse);
                    break;
                case 2:
                    rbody.AddForce(Vector3.left * forceDie, ForceMode.Impulse);
                    break;
                case 3:
                    rbody.AddForce(Vector3.forward * forceDie, ForceMode.Impulse);
                    break;
                case 4:
                    rbody.AddForce(Vector3.back * forceDie, ForceMode.Impulse);
                    break;
            }
        }
    }
}

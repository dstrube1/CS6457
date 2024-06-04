using UnityEngine;

public class CollectableBall : RandomMover //commenting this out until I can figure out how to make it work
{
    void OnTriggerEnter(Collider c) {
        if (c.attachedRigidbody != null)
        {
            BallCollector bc = c.attachedRigidbody.gameObject.GetComponent<BallCollector>();
            if (bc != null)
            {
                EventManager.TriggerEvent<SphereCollectionEvent, Vector3>(c.transform.position);
                bc.ReceiveBall();
                Destroy(this.gameObject);
            }
        }
    }

    //This doesn't keep it from sinking 
    // void Update(){
    //     if (rbody.position.y < 1.5f)
    //     {
    //         rbody.position = new Vector3(rbody.position.x, 1.5f, rbody.position.z);
    //     }
    // }

    //Overriding the FixedUpdate method from RandomMover so that CollectableBall doesn't jump
       void FixedUpdate()
    {
        //This doesn't keep it from sinking
        // if (rbody.position.y < 1.5f)
        // {
        //     rbody.position = new Vector3(rbody.position.x, 1.5f, rbody.position.z);
        // }

        //Three blind dice
        int moveDie = random.Next(0, 10);
        int forceDie = random.Next(0, 20);
        int directionDie = random.Next(0, 4);

        //Jump at random intervals if the jumping bean is on the ground
        if (moveDie < 4 && rbody.position.y < 0.6f)
        {
            switch (directionDie)
            {
                case 0:
                    rbody.AddForce(Vector3.right * forceDie, ForceMode.Impulse);
                    break;
                case 1:
                    rbody.AddForce(Vector3.left * forceDie, ForceMode.Impulse);
                    break;
                case 2:
                    rbody.AddForce(Vector3.forward * forceDie, ForceMode.Impulse);
                    break;
                case 3:
                    rbody.AddForce(Vector3.back * forceDie, ForceMode.Impulse);
                    break;
            }
        }
    }

}

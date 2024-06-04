using UnityEngine;

public class BombBounceReporter : MonoBehaviour
{

    void OnCollisionEnter(Collision c)
    {

        if (c.impulse.magnitude > 0.25f)
        {
            //we'll just use the first contact point for simplicity
            EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.contacts[0].point);

            // Note that BombBounceEvent only takes one arg, not two. EventManager.TriggerEvent<BombBounceEvent, Vector3>(....
        }
            

    }
}

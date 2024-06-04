
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MinionAI : MonoBehaviour
{
    public GameObject[] gameObjects;
    private int currWaypoint = 0;
    private Animator anim;

    private enum AIState {GotoStatics, ChaseMovingWaypoint};
    private AIState aiState;

    [SerializeField]
    private GameObject destinationTracker;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("vely",1f);
        aiState = AIState.GotoStatics;
        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vely", GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.magnitude / GetComponent<UnityEngine.AI.NavMeshAgent>().speed);
        
        switch (aiState)
        {
            case AIState.GotoStatics:
                if (currWaypoint == 6)
                {
                    aiState = AIState.ChaseMovingWaypoint;
                }else{
                    //call setNextWaypoint(), but only if the navMeshAgent has reached the target waypoint
                    //Also, navMeshAgent.pathPending is not true so that being near a waypoint doesn’t cause rapid iteration through the waypoints before the new path can be found
                    //More complicated NavMeshAgent implementations may necessitate NavMeshAgent.hasPath and NavMeshAgent.pathStatus
                    if (GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance < 0.5f && !GetComponent<UnityEngine.AI.NavMeshAgent>().pathPending)
                    {
                        setNextWaypoint();
                    }
                }
                break;
            case AIState.ChaseMovingWaypoint:
                if (!GetComponent<UnityEngine.AI.NavMeshAgent>().pathPending && GetComponent<UnityEngine.AI.NavMeshAgent>().pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete)
                    {
                        chaseMovingWaypoint();
                    }
                break;
        }
    }

    private void setNextWaypoint()
    {
        if (gameObjects.Length == 0)
        {
            Debug.LogWarning("No waypoints set for minion");
            return;
        }

        if (currWaypoint < gameObjects.Length)
        {
            if (currWaypoint < gameObjects.Length - 1)
            {
                //Save moving waypoint for last
                GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(gameObjects[currWaypoint].transform.position);   
                destinationTracker.transform.position = gameObjects[currWaypoint].transform.position;             
            }
            else{
                chaseMovingWaypoint();
            }
            currWaypoint++;
            // Debug.LogWarning("currWaypoint incremebted to: " + currWaypoint);
        }else
        {
            currWaypoint = 0;
        }
    }

    private void chaseMovingWaypoint(){
        if (currWaypoint < gameObjects.Length)
        {
            Vector3 targetPos = gameObjects[5].transform.position;
            Vector3 currentPos = this.transform.position;
            // Debug.Log("targetPos: " + targetPos + "; currentPos: " + currentPos);
            float dist = (targetPos - currentPos).magnitude;
            float lookAhead = dist / GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
            // Debug.LogWarning("dist: " + dist + "; lookAhead: " + lookAhead);
            Vector3 futureTarget = targetPos + (lookAhead * gameObjects[5].GetComponent<VelocityReporter>().velocity);
            // Debug.LogWarning("futureTarget before clamp: " + futureTarget);

            //Setting max z to the max z of the moving waypoint
            futureTarget.z = Mathf.Clamp(futureTarget.z, -15f, 15f);
            destinationTracker.transform.position = futureTarget;
            // Debug.LogWarning("destinationTracker after clamp: " + destinationTracker.transform.position);
            
            // Debug.LogWarning("futureTarget after clamp: " + futureTarget);
            GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(futureTarget);
            if (GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance < 0.1f)
            {
                aiState = AIState.GotoStatics;
                setNextWaypoint();
            }

        }else
        {
            currWaypoint = 0;
        }
    }
}

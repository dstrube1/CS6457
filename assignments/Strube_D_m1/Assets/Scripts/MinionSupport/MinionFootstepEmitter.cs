using UnityEngine;

public class MinionFootstepEmitter : MonoBehaviour {



    public void ExecuteFootstep() {

        EventManager.TriggerEvent<MinionFootstepEvent, Vector3>(transform.position);
    }
}

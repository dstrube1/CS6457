
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ReactiveMinionScript : MonoBehaviour {
    //Necessary:
    private Animator anim;

    //TODO: Necessary?:
    private Rigidbody rbody;
    private bool animating;

    void Start()
    {
        animating = false;
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (animating)
        {
            anim.Play("MinionForward");
        } 
    }

    void OnTriggerEnter(Collider c) {
        //On Trigger, do some animation 
        var x = c.gameObject.GetComponent<BallCollector>();
        if (x == null)
        {
            return;
        }
        animating = true;
        Debug.Log($"Minion is animating {c.gameObject.name}");
        anim.SetFloat("vely",1f);
    }


    void OnTriggerExit (Collider c)
    {
        var x = c.gameObject.GetComponent<BallCollector>();
        if (x == null)
        {
            return;
        }
        animating = false;
        Debug.Log("Minion is not animating");
        anim.SetFloat("vely",0f);
    }

    

}
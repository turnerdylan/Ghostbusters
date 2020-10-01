using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoController : Weapon
{
    Animator anim;
    Ghost capturedGhost;

    private LineRenderer lr;
    private SpringJoint joint;
    private SpringJoint joint2;
    playerController player;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<playerController>();
    }

    public override void Use()
    {
        base.Use();
        anim.SetTrigger("Lasso");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ghost>())
        {
            Ghost capturedGhost = other.gameObject.GetComponent<Ghost>();
            anim.SetBool("Caught", true);
            CatchGhostInLasso(capturedGhost);
            /*if (other.attachedRigidbody != null)
            {
                other.attachedRigidbody.isKinematic = true;
            }*/
        }
        else
        {
            print("you either missed or already have a ghost!");
        }
    }

    //start the grapple
    public void CatchGhostInLasso(Ghost ghost)
    {
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint2 = ghost.gameObject.AddComponent<SpringJoint>();

        joint.autoConfigureConnectedAnchor = false;
        joint2.autoConfigureConnectedAnchor = false;

        joint.connectedAnchor = player.transform.position;
        joint2.connectedAnchor = ghost.transform.position;

        joint.connectedBody = ghost.GetComponent<Rigidbody>();
        joint2.connectedBody = player.GetComponent<Rigidbody>();

        float distanceFromPoint = Vector3.Distance(player.transform.position, ghost.transform.position);

        //joint.maxDistance = distanceFromPoint * 0.8f;

        
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

    }

    public void ReleaseGhost()
    { 

    }

}

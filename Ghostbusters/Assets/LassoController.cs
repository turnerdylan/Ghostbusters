using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoController : Weapon
{
    Animator anim;
    Ghost capturedGhost;

    private LineRenderer lr;
    private SpringJoint joint;
    playerController player;


    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
        player = GetComponentInParent<playerController>();
    }

    private void LateUpdate()
    {
        DrawRope();
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
            capturedGhost = other.gameObject.GetComponent<Ghost>();
            anim.SetBool("Caught", true);
            CatchGhostInLasso(capturedGhost);
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
        joint.connectedBody = ghost.GetComponent<Rigidbody>();
        joint.autoConfigureConnectedAnchor = false;

        joint.maxDistance = 2f;
        joint.minDistance = 1f;
        joint.spring = 10f;
        joint.damper = 2f;

    }

    public void ReleaseGhost()
    { 

    }

    void DrawRope()
    {
        if(capturedGhost != null)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, capturedGhost.transform.position);
        }
    }

}

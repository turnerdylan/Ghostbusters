using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoController : Weapon
{
    Animator anim;
    Ghost capturedGhost;

    private LineRenderer lr;
    private SpringJoint joint;
    Player player;
    public bool isLassoing = false;


    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    public override void Use()
    {
        base.Use();

        if(!isLassoing) anim.SetTrigger("Lasso");
        else
        {
            ReleaseGhost();
        }
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
        isLassoing = true;

        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.connectedBody = ghost.GetComponent<Rigidbody>();
        joint.autoConfigureConnectedAnchor = false;

        joint.maxDistance = 2f;
        joint.minDistance = 1f;
        joint.spring = 10f;
        joint.damper = 2f;

        lr.positionCount = 2;
    }

    public void ReleaseGhost()
    {
        isLassoing = false;
        capturedGhost = null;
        anim.SetBool("Caught", false);
        lr.positionCount = 0;
        Destroy(joint);
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

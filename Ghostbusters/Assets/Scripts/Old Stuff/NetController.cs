using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : Weapon
{
    Player pc;
    public Transform storedGhostTransform;
    public Ghost capturedGhost;
    Animator animator;

    private void Start()
    {
        pc = GetComponentInParent<Player>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(capturedGhost == null && other.gameObject.GetComponent<Ghost>())
        {
            Ghost capturedGhost = other.gameObject.GetComponent<Ghost>();
            CatchGhostInNet(capturedGhost);
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

    public override void Use()
    {
        base.Use();
        animator.SetBool("Attack", true);
        if(capturedGhost != null)
        {
            ReleaseGhost();
        }
    }

    public void ResetNet()
    {
        animator.SetBool("Attack", false);
    }

    public void CatchGhostInNet(Ghost ghost)
    {
        //TODO set ghost state
        capturedGhost = ghost;
        ghost.transform.parent = storedGhostTransform.parent;
        ghost.transform.position = storedGhostTransform.position;
        ghost.GetComponent<Rigidbody>().isKinematic = true;
        ghost.agent.enabled = false;
        ghost.SetState(AI_GHOST_STATE.CAUGHT);
    }

    public void ReleaseGhost()
    {
        //TODO set ghost state
        capturedGhost.transform.parent = null;
        capturedGhost.GetComponent<Rigidbody>().isKinematic = false;
        capturedGhost.ResetTimer();
        capturedGhost = null;

    }
}

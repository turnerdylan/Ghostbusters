using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : Weapon
{
    PlayerController pc;
    public Transform storedGhostTransform;
    Ghost capturedGhost;
    Animator animator;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(pc.capturedGhost == null && other.gameObject.GetComponent<Ghost>())
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
    }

    public void ResetNet()
    {
        animator.SetBool("Attack", false);
    }

    public void CatchGhostInNet(Ghost ghost)
    {
        //TODO set ghost state
        ghost.currentNet = this;
        pc.OnCapturedGhost(ghost);
        ghost.transform.parent = storedGhostTransform.parent;
        ghost.transform.position = storedGhostTransform.position;
        ghost.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ReleaseGhost(Ghost ghost)
    {
        //TODO set ghost state
        ghost.transform.parent = null;
        ghost.GetComponent<Rigidbody>().isKinematic = false;
        ghost.ResetTimer(); 
        pc.capturedGhost = null;

    }
}

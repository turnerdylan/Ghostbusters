using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour
{
    playerController pc;
    public Transform storedGhost;

    private void Start()
    {
        pc = GetComponentInParent<playerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(pc.capturedGhost == null && other.gameObject.layer == 10)
        {
            pc.OnCapturedGhost(other.gameObject );
            other.transform.parent = storedGhost.parent;
            other.transform.position = storedGhost.position;
            if(other.attachedRigidbody != null)
            {
                other.attachedRigidbody.isKinematic = true;
            }
        }
    }
}

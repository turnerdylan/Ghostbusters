using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    Transform teleportLocation;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            teleportLocation = child.transform;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Ghost newGhost = other.GetComponent<Ghost>();
        if (newGhost){
            other.gameObject.transform.position = teleportLocation.position;
            newGhost.SetState(AI_GHOST_STATE.IDLE);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : Interactable
{
    public GameObject buttonSprite;
    public Transform storedGhost;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(canInteract)
        {
            buttonSprite.gameObject.SetActive(true);
            
        }
        else
        {
            buttonSprite.gameObject.SetActive(false);
        }
    }

    //interact with the trap
    public override void Interact()
    {
        base.Interact();
        if(canInteract)
        {
            TrapGhost();
        }
        else
        {
            print("cant interact!");
        }
        
    }

    private void TrapGhost()
    {
        if (true)
        {
            NetController net = FindObjectOfType<NetController>();
            net.capturedGhost.transform.parent = transform;
            net.capturedGhost.transform.position = storedGhost.position;
            net.capturedGhost.SetStateText("trapped!");
            net.capturedGhost = null;
        }
    }
}

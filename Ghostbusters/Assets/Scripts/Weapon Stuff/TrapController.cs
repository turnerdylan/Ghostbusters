﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : Interactable
{
    PlayerController pc;
    public GameObject buttonSprite;
    public Transform storedGhost;
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

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
        if(canInteract && pc.capturedGhost)
        {
            TrapGhost();
        }
        else
        {
            print("no ghost to trap or cant interact!");
        }
        
    }

    private void TrapGhost()
    {
        print("trapping the ghost!");
        if (pc.capturedGhost != null)
        {
            pc.capturedGhost.transform.parent = transform;
            pc.capturedGhost.transform.position = storedGhost.position;
            //set state to caught
            pc.capturedGhost.SetStateText("trapped!");
            pc.capturedGhost = null;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class TutorialInputHandler : MonoBehaviour
{
    private TutorialPlayer player;

    private float scareInput;
    private float getBagInput;
    private float swingBagInput;
    private float dropBagInput;

    private void Awake()
    {
        player = GetComponent<TutorialPlayer>();
        //controls = new PlayerControls();
    }

    public void OnMove(CallbackContext context)
    {
        if (player != null)
            player.SetMoveVector(context.ReadValue<Vector2>());
    }

    public void DepositGhosts(CallbackContext context)
    {
        if(context.performed)
        {
            player.DepositGhosts();
        }
    }

    public void Dive(CallbackContext context)
    {
        if (context.performed)
        {
            player.Dive();
        }
    }


    public void UpScare(CallbackContext context)
    {
        if(player != null && context.performed)
            player.Scare(TUTORIAL_BUTTON_PRESS.Up);
    }
    public void DownScare(CallbackContext context)
    {
        if(player != null && context.performed)
            player.Scare(TUTORIAL_BUTTON_PRESS.Down);
    }
    public void LeftScare(CallbackContext context)
    {
        if(player != null && context.performed)
            player.Scare(TUTORIAL_BUTTON_PRESS.Left);
    }
    public void RightScare(CallbackContext context)
    {
        if(player != null && context.performed)
            player.Scare(TUTORIAL_BUTTON_PRESS.Right);
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandlerTest : MonoBehaviour
{
    private Player pc;

    private float scareInput;

    private void Awake()
    {
        pc = GetComponent<Player>();
        //controls = new PlayerControls();
    }

    public void OnMove(CallbackContext context)
    {
        if (pc != null)
            pc.SetMoveVector(context.ReadValue<Vector2>());
    }

    public void Scare(CallbackContext context)
    {

        scareInput = context.ReadValue<float>();
        if(scareInput == 1)
        {
            pc.Scare();
        }
        print("scare input is" + scareInput);
    }

}

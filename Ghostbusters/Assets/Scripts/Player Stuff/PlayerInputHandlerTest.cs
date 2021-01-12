using System.Collections;
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

    /*    public void InitializePlayer(PlayerConfiguration config)
        {
            playerConfig = config;
            playerMesh.material = config.playerMaterial;
            config.Input.onActionTriggered += Input_onActionTriggered;
        }*/

    /*private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }
    }*/

    public void OnMove(CallbackContext context)
    {
        if (pc != null)
            pc.SetMoveVector(context.ReadValue<Vector2>());
    }

    /*public void OnLook(CallbackContext context)
    {
        if (pc != null)
            pc.SetLookVector(context.ReadValue<Vector2>());
    }*/

    public void Scare(CallbackContext context)
    {
        /*if (pc != null)
        {
            if (context.canceled)
            {
                pc.Scare();
            }
        }*/

        scareInput = context.ReadValue<float>();
        pc.Scare();
        print("scare input is" + scareInput);
    }

}

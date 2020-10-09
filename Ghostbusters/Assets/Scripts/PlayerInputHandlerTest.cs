using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandlerTest : MonoBehaviour
{
    private playerController pc;

    private void Awake()
    {
        pc = GetComponent<playerController>();
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

    public void OnLook(CallbackContext context)
    {
        if (pc != null)
            pc.SetLookVector(context.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext context)
    {
        if (pc != null)
        {
            if (context.performed)
            {
                pc.Jump();
            }          
        }
    }

    public void OnUseItem(CallbackContext context)
    {
        if (pc != null)
        {
            if(context.performed)
            {
                pc.UseItem();
            }
        }  
    }

    public void OnSwitchWeapon(CallbackContext context)
    {
        if (pc != null)
        {
            if (context.performed)
            {
                //Debug.Log("Action was performed");
                pc.SwitchWeapon();
            }
        }
    }

    public void OnInteract(CallbackContext context)
    {
        if (pc != null)
        {
            if(context.performed)
            {
                pc.Interact();
            }
        }
    }

}

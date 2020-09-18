using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private Mover mover;

    [SerializeField]
    private MeshRenderer playerMesh;

    private void Awake()
    {
        mover = GetComponent<Mover>();
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
        if (mover != null)
            mover.SetMoveVector(context.ReadValue<Vector2>());
    }

    public void OnLook(CallbackContext context)
    {
        if (mover != null)
            mover.SetLookVector(context.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext context)
    {
        if (mover != null)
            mover.Jump();
    }

    public void OnUseItem(CallbackContext context)
    {
        //if (mover != null)
            //mover.UseItem();
    }

}

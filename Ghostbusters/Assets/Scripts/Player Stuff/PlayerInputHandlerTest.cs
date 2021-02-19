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
    private float getBagInput;
    private float swingBagInput;
    private float dropBagInput;

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

    public void GetBag(CallbackContext context)
    {
        getBagInput = context.ReadValue<float>();
        if(context.performed)
        {
            pc.PickupBag();
        }
    }

    public void SwingBag(CallbackContext context)
    {
        swingBagInput = context.ReadValue<float>();
        if (context.performed)
        {
            pc.SwingBag();
        }
    }
    public void UpScare(CallbackContext context)
    {
        if(pc != null && context.performed)
            pc.Scare(BUTTON_PRESS.Up);
    }
    public void DownScare(CallbackContext context)
    {
        if(pc != null && context.performed)
            pc.Scare(BUTTON_PRESS.Down);
    }
    public void LeftScare(CallbackContext context)
    {
        if(pc != null && context.performed)
            pc.Scare(BUTTON_PRESS.Left);
    }
    public void RightScare(CallbackContext context)
    {
        if(pc != null && context.performed)
            pc.Scare(BUTTON_PRESS.Right);
    }
}

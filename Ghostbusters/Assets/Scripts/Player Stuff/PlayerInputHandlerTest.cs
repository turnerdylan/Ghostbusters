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

    public void Scare(CallbackContext context)
    {

        scareInput = context.ReadValue<float>();
        if(scareInput == 1)
        {
            pc.Scare();
        }
        print("scare input is" + scareInput);
    }

    public void GetBag(CallbackContext context)
    {
        getBagInput = context.ReadValue<float>();
        if(getBagInput == 1)
        {
            pc.PickupBag();
        }
    }

    public void DropBag(CallbackContext context)
    {
        dropBagInput = context.ReadValue<float>();
        if (dropBagInput == 1)
        {
            pc.DropBag();
        }
    }

    public void SwingBag(CallbackContext context)
    {
        print("test");
        swingBagInput = context.ReadValue<float>();
        if (context.performed)
        {
            print("test1");
            pc.SwingBag();
        }
    }
    public void UpScare(CallbackContext context)
    {
        if(pc != null && context.performed)
        {
            //Debug.Log("Hello");
            pc.UpScare();
        }
    }
    public void DownScare(CallbackContext context)
    {
        if(pc != null && context.performed)
            pc.DownScare();
    }
    public void LeftScare(CallbackContext context)
    {
        if(pc != null && context.performed)
            pc.LeftScare();
    }
    public void RightScare(CallbackContext context)
    {
        if(pc != null && context.performed)
            pc.RightScare();
    }
}

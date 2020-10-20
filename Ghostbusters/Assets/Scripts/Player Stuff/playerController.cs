using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //serializables
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float jumpForce = 10f;

    Interactable currentInteraction;
    WeaponsController weapons;

    public Ghost capturedGhost;

    //references
    private Rigidbody rb;
    private Animator anim;

    //private vars
    //world direction the guy moves
    private Vector3 moveDirection = Vector3.zero;
    //this is the input from joystick, in x and y dims
    private Vector2 inputMoveVector = Vector2.zero;

    private Vector3 inputLookVector = Vector3.zero;
    private float storedLookValue;
    private CheckIfGrounded isGrounded;

    private float currentJumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = GetComponentInChildren<CheckIfGrounded>();
        weapons = GetComponentInChildren<WeaponsController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SetLookDirection();
    }

    private void FixedUpdate()
    {
        SetMoveDirection();
    }

    public void OnCapturedGhost(Ghost ghost)
    {
        capturedGhost = ghost;
    }



    public void SetMoveVector(Vector2 direction)
    {
        inputMoveVector = direction;
    }

    //this was for old control scheme
    /*public void SetLookVector(Vector2 direction)
    {
        inputLookVector = direction;
    }*/

    public void Jump()
    {
        if (isGrounded.GetIsGrounded())
        {
            rb.velocity = new Vector3(inputMoveVector.x, jumpForce, inputMoveVector.y);
        }
    }

    public void Interact()
    {
        currentInteraction.Interact();
    }

    public void SwitchWeapon()
    {
        weapons.ChangeWeapon();
    }

    public void UseItem()
    {
        weapons.UseWeapon();
    }


    private void SetMoveDirection()
    {
        if (inputMoveVector.magnitude > 0.5f)
        {
            moveDirection = inputMoveVector * moveSpeed * Time.deltaTime;

            rb.MovePosition(transform.position + new Vector3(moveDirection.x, 0, moveDirection.y));
            anim.SetBool("Run", true);
            
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    private void SetLookDirection()
    {
        if (inputMoveVector.magnitude > 0.7f)
        {
            storedLookValue = Mathf.Atan2(inputMoveVector.x, inputMoveVector.y);
            transform.rotation = Quaternion.Euler(0, storedLookValue * Mathf.Rad2Deg, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, storedLookValue * Mathf.Rad2Deg, 0);

        }
        else
        {
            transform.rotation = Quaternion.Euler(0, storedLookValue * Mathf.Rad2Deg, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable;
        if (other.gameObject.GetComponent<Interactable>())
        {
            interactable = other.gameObject.GetComponent<Interactable>();
            currentInteraction = interactable;
        }
    }
}

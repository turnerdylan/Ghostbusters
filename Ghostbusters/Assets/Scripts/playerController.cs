﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
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
    }

    void Update()
    {
        SetLookDirection();
        AdjustJumpSettings();
    }

    private void FixedUpdate()
    {
        SetMoveDirection();
    }


    private void AdjustJumpSettings()
    {
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * 20);
        }
    }

    public void OnCapturedGhost(Ghost ghost)
    {
        capturedGhost = ghost;
    }



    public void SetMoveVector(Vector2 direction)
    {
        inputMoveVector = direction;
    }

    public void SetLookVector(Vector2 direction)
    {
        inputLookVector = direction;
    }

    public void Jump()
    {
        if (isGrounded.GetIsGrounded())
        {
            print("jumping");
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
        //makes it so that it moves relative to the camera
        //moveDirection = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.forward) * inputMoveVector;


        //add speed and deltatime
        moveDirection = inputMoveVector * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + new Vector3(moveDirection.x, 0, moveDirection.y));
    }

    private void SetLookDirection()
    {
        if (inputLookVector.magnitude > 0.5f)
        {
            storedLookValue = Mathf.Atan2(inputLookVector.x, inputLookVector.y);
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

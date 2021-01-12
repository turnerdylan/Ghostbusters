using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //serializables
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float jumpForce = 10f;

    Interactable currentInteraction;
    WeaponsController weapons;

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
        int numberOfGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, gameObject.transform.position) < 5
                && GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                numberOfGhosts++;
                //GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().SplitApart();
            }
        }
    }

    private void FixedUpdate()
    {
        SetMoveDirection();
    }

    public void SetMoveVector(Vector2 direction)
    {
        inputMoveVector = direction;
    }

    public void Jump()
    {
        if (isGrounded.GetIsGrounded())
        {
            rb.velocity = new Vector3(inputMoveVector.x, jumpForce, inputMoveVector.y);
        }
    }

    /*public void Interact()
    {
        if (currentInteraction.transform.name == "Ghost_Trap")
            currentInteraction.Interact(this);
        else
            currentInteraction.Interact();
    }*/

    public void Scare()
    {
        print("scared");
        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, gameObject.transform.position) < 5
                && GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                print("test");
                GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().SplitApart();
            }
        }
    }


    private void SetMoveDirection()
    {
        if (inputMoveVector.magnitude > 0.5f)
        {
            moveDirection = inputMoveVector * moveSpeed;
            //rb.MovePosition(transform.position + new Vector3(moveDirection.x, 0, moveDirection.y));
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.y);
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

    /*private void OnTriggerEnter(Collider other)
    {
        Interactable interactable;
        if (other.gameObject.GetComponent<Interactable>())
        {
            interactable = other.gameObject.GetComponent<Interactable>();
            currentInteraction = interactable;
        }
    }*/
}

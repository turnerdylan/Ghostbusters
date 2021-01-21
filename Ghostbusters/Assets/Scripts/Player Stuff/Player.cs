using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //serializables
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float viewAngle = 45f;
    [SerializeField]
    private float scareRange = 5f;

    

    //references
    private Rigidbody rb;
    private Interactable currentInteraction;
    private WeaponsController weapons;

    //private vars
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputMoveVector = Vector2.zero;
    private Vector3 inputLookVector = Vector3.zero;
    private float storedLookValue;
    private CheckIfGrounded isGrounded;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = GetComponentInChildren<CheckIfGrounded>();
        weapons = GetComponentInChildren<WeaponsController>();
    }

    void Update()
    {
        SetLookDirection();
        //print( "Ghosts in range is " + TestForGhostsInRange());
    }

    private void FixedUpdate()
    {
        SetMoveDirection();
    }




    private int TestForGhostsInRange()
    {
        int numberOfGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, gameObject.transform.position) < scareRange
                && GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                numberOfGhosts++;
            }
        }
        return numberOfGhosts;
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
        //TODO fix this logic so there is less repeating code

        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            //check if ghost is close enough and is active
            if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, transform.position) < scareRange
                && GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                Vector3 dirToGhost = (GhostManager.Instance.bigGhosts[i].transform.position - transform.position).normalized;
                float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                //print(angleBetweenPlayerandGhost);

                if(angleBetweenPlayerandGhost  < viewAngle / 2)
                {
                    if(Physics.Linecast(transform.position, GhostManager.Instance.bigGhosts[i].transform.position)){
                        //GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().SplitApart();
                        GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().AddPlayerScare(this);
                    }
                }
            }
        }

        for (int i = 0; i < GhostManager.Instance.maxMediumGhosts; i++)
        {
            //check if ghost is close enough and is active
            if (Vector3.Distance(GhostManager.Instance.mediumGhosts[i].transform.position, transform.position) < scareRange
                && GhostManager.Instance.mediumGhosts[i].activeSelf)
            {
                Vector3 dirToGhost = (GhostManager.Instance.mediumGhosts[i].transform.position - transform.position).normalized;
                float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                //print(angleBetweenPlayerandGhost);

                if (angleBetweenPlayerandGhost < viewAngle / 2)
                {
                    if (Physics.Linecast(transform.position, GhostManager.Instance.mediumGhosts[i].transform.position))
                    {
                        GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().SplitApart();
                    }
                }
            }
        }

        for (int i = 0; i < GhostManager.Instance.maxSmallGhosts; i++)
        {
            //check if ghost is close enough and is active
            if (Vector3.Distance(GhostManager.Instance.smallGhosts[i].transform.position, transform.position) < scareRange
                && GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                Vector3 dirToGhost = (GhostManager.Instance.smallGhosts[i].transform.position - transform.position).normalized;
                float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                //print(angleBetweenPlayerandGhost);

                if (angleBetweenPlayerandGhost < viewAngle / 2)
                {
                    if (Physics.Linecast(transform.position, GhostManager.Instance.smallGhosts[i].transform.position))
                    {
                        GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().Bansish();
                    }
                }
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
}

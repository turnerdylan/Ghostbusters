using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public enum PLAYER_STATE
{
    NORMAL,
    WITH_BAG,
    STUNNED,
};

public class Player : MonoBehaviour
{
    //references
    private Rigidbody rb;
    private Interactable currentInteraction;
    private Animator anim;

    //serializables
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] private float _scareRange = 5f;
    [SerializeField] private float _stunTime = 3f;
    [SerializeField] private float _rotationSpeed = 10f;

    //private variables
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _inputMoveVector = Vector2.zero;
    private Vector3 _inputLookVector = Vector3.zero;

    private float _storedLookValue;
    PLAYER_STATE currentState = PLAYER_STATE.NORMAL;

    //public
    public Transform testTransform; //delete this later

    private void Awake()
    {
        GetComponentInChildren<Light>().spotAngle = _viewAngle;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SetMoveDirection();
        SetLookDirection();
    }



    //TODO: delete this at some point
    private int TestForGhostsInRange()
    {
        int numberOfGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, gameObject.transform.position) < _scareRange
                && GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                numberOfGhosts++;
            }
        }
        return numberOfGhosts;
    }

    public void SetMoveVector(Vector2 direction)
    {
        _inputMoveVector = direction;
    }

    public void Interact()
    {
        if (currentInteraction.transform.name == "Ghost_Trap")
            currentInteraction.Interact(this);
        else
            currentInteraction.Interact();
    }

    public void SetInteraction(Interactable interaction)
    {
        currentInteraction = interaction;
    }

    public void RemoveInteraction()
    {
        currentInteraction = null;
    }

    public void GetBag()
    {
        if(currentState == PLAYER_STATE.NORMAL)
        {
            if (Vector3.Distance(Bag.Instance.gameObject.transform.position, transform.position) < Bag.Instance.GetInteractionRadius())
            {
                //set player to holding bag state
                //set player holding bag animation
                //disable player scaring controls
                //parent bag to the player
                Bag.Instance.transform.parent = transform;
                Bag.Instance.transform.position = testTransform.position;
                Bag.Instance.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        //StartCoroutine(QuickTestDelay(1));
        currentState = PLAYER_STATE.WITH_BAG;
    }

    IEnumerator QuickTestDelay(int state)
    {
        yield return new WaitForSeconds(0.2f);
        if (state == 1) currentState = PLAYER_STATE.WITH_BAG;
        else if (state == 2) currentState = PLAYER_STATE.NORMAL;
    }

    public void DropBag()
    {
        if(currentState == PLAYER_STATE.WITH_BAG)
        {
            if(Vector3.Distance(transform.position, Van.Instance.GetBagStoredPosition()) < Van.Instance.GetInteractionRadius())
            {
                Bag.Instance.transform.parent = Van.Instance.transform;
                Bag.Instance.transform.position = Van.Instance.GetBagStoredPosition();
            }
            else
            {
                //drop bag on the ground
                Bag.Instance.transform.parent = null;
                //Bag.Instance.transform.position = transform.position + Vector3.back + Vector3.up * 3;
                Bag.Instance.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        //StartCoroutine(QuickTestDelay(2));
        currentState = PLAYER_STATE.NORMAL;
    }

    public void SwingBag()
    {
        if(currentState == PLAYER_STATE.WITH_BAG)
        {
            print("swung bag");
        }
        //anim.SetBool();
        //activate bag collider
        //trigger animation
    }

    public void Scare()
    {
        //TODO fix this logic so there is less repeating code
        anim.SetBool("Scare", true);

        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if(GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, transform.position) <= _scareRange)
                {
                    print("test1");
                    Vector3 dirToGhost = (GhostManager.Instance.bigGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                    //print(angleBetweenPlayerandGhost);

                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        print("test2");
                        if (Physics.Linecast(transform.position, GhostManager.Instance.bigGhosts[i].transform.position))
                        {
                            GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().AddPlayerScare(this);
                            //TODO fix this logic
                            print("test");
                            //GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().SplitApart();
                        }
                    }
                }
            }            
        }

        for (int i = 0; i < GhostManager.Instance.maxMediumGhosts; i++)
        {
            if(GhostManager.Instance.mediumGhosts[i].activeSelf)
            {
                if (Vector3.Distance(GhostManager.Instance.mediumGhosts[i].transform.position, gameObject.transform.position) <= _scareRange)
                {
                    Vector3 dirToGhost = (GhostManager.Instance.mediumGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                    //print(angleBetweenPlayerandGhost);

                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        if (Physics.Linecast(transform.position, GhostManager.Instance.mediumGhosts[i].transform.position))
                        {
                            if (GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().GetScarable())
                                GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().SplitApart();
                        }
                    }
                }
            }
            
        }

        for (int i = 0; i < GhostManager.Instance.maxSmallGhosts; i++)
        {
            if(GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                if (Vector3.Distance(GhostManager.Instance.smallGhosts[i].transform.position, gameObject.transform.position) <= _scareRange)
                {
                    Vector3 dirToGhost = (GhostManager.Instance.smallGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                    //print(angleBetweenPlayerandGhost);

                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        if (Physics.Linecast(transform.position, GhostManager.Instance.smallGhosts[i].transform.position))
                        {
                            if (GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().GetScarable())
                                GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().Banish();
                        }
                    }
                }
            }
            
        }
    }

    private void SetMoveDirection()
    {
        if (_inputMoveVector.magnitude > 0.3f)
        {
          anim.SetBool("Walk", true);
            _moveDirection = _inputMoveVector * _moveSpeed;
            //rb.MovePosition(transform.position + new Vector3(moveDirection.x, 0, moveDirection.y));
            rb.velocity = new Vector3(_moveDirection.x, rb.velocity.y, _moveDirection.y);
        }
        else{
          anim.SetBool("Walk", false);
        }
    }

    private void SetLookDirection()
    {
        if (_inputMoveVector.magnitude > 0.3f)
        {
            _storedLookValue = Mathf.Atan2(_inputMoveVector.x, _inputMoveVector.y);
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(_inputMoveVector.x, 0, _inputMoveVector.y) );
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, _storedLookValue * Mathf.Rad2Deg, 0);
        }
    }

    public PLAYER_STATE GetPlayerState()
    {
        return currentState;
    }

    public void TriggerStun()
    {
        StartCoroutine(StunPlayer());
    }

    public IEnumerator StunPlayer()
    {
        enabled = false;
        currentState = PLAYER_STATE.STUNNED;
        yield return new WaitForSeconds(_stunTime);
        enabled = true;
        currentState = PLAYER_STATE.NORMAL;
    }
}


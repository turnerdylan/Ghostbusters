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

public enum BUTTON_PRESS
{
    None,
    Up,
    Down,
    Left,
    Right
}
public class Player : MonoBehaviour
{
    //references
    private Rigidbody rb;
    private Interactable currentInteraction;
    private Animator anim;
    private Light spotlight;

    //serializables
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] private float _rotationSpeed = 10f;

    //private variables
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _inputMoveVector = Vector2.zero;
    private Vector3 _inputLookVector = Vector3.zero;

    private float _storedLookValue;
    public PLAYER_STATE currentState = PLAYER_STATE.NORMAL;

    //public
    public Transform testTransform; //delete this later
    public float _scareRange = 5f;
    public float _stunTime = 3f;
    public BUTTON_PRESS _buttonPressed = BUTTON_PRESS.None;
    public Transform handTransform;

    private void Awake()
    {
        spotlight = GetComponentInChildren<Light>();
        spotlight.spotAngle = _viewAngle;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetMoveDirection();
        SetLookDirection();
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

    public void PickupBag()
    {
        if(currentState == PLAYER_STATE.NORMAL)
        {
            anim.SetBool("Hold", true);
            if (Vector3.Distance(Bag.Instance.gameObject.transform.position, transform.position) < Bag.Instance.GetInteractionRadius())
            {
                currentState = PLAYER_STATE.WITH_BAG;
                Bag.Instance.transform.parent = testTransform;
                Bag.Instance.transform.localPosition = Vector3.zero;
                //Bag.Instance.transform.localRotation = Quaternion.Euler(Vector3.zero);
                Destroy(Bag.Instance.GetComponent<Rigidbody>());
            }
        }
        Bag.Instance.SetBagState(Bag.BAG_STATE.PICKED_UP);

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
            anim.SetTrigger("Catch");
            print("swung bag");
            
        }
    }

    public void SwingBagStart()
    {
        Bag.Instance.GetComponent<CapsuleCollider>().isTrigger = true;
    }

    public void SwingBagEnd()
    {
        Bag.Instance.GetComponent<CapsuleCollider>().isTrigger = false;
    }

    public void Scare()
    {
        //TODO fix this logic so there is less repeating code
        anim.SetBool("Scare", true);
        
        StartCoroutine(ChangeSpotlightColor());

        /*for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if(GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, transform.position) <= _scareRange)
                {
                    Vector3 dirToGhost = (GhostManager.Instance.bigGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                    //print(angleBetweenPlayerandGhost);
                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        if (GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().CheckIfScarable())
                        {
                            print("test");
                            //GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().AddPlayerScare(this);
                        }
                    }
                }
            }            
        }*/

        // for (int i = 0; i < GhostManager.Instance.maxMediumGhosts; i++)
        // {
        //     if(GhostManager.Instance.mediumGhosts[i].activeSelf)
        //     {
        //         if (Vector3.Distance(GhostManager.Instance.mediumGhosts[i].transform.position, gameObject.transform.position) <= _scareRange)
        //         {
        //             Vector3 dirToGhost = (GhostManager.Instance.mediumGhosts[i].transform.position - transform.position).normalized;
        //             float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
        //             //print(angleBetweenPlayerandGhost);

        //             if (angleBetweenPlayerandGhost < _viewAngle / 2)
        //             {
        //                 if (GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().CheckIfScarable())
        //                 {
        //                     GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().AddPlayerScare(this);
        //                 }
        //             }
        //         }
        //     }
        // }

        // for (int i = 0; i < GhostManager.Instance.maxSmallGhosts; i++)
        // {
        //     if(GhostManager.Instance.smallGhosts[i].activeSelf)
        //     {
        //         if (Vector3.Distance(GhostManager.Instance.smallGhosts[i].transform.position, gameObject.transform.position) <= _scareRange)
        //         {
        //             Vector3 dirToGhost = (GhostManager.Instance.smallGhosts[i].transform.position - transform.position).normalized;
        //             float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
        //             //print(angleBetweenPlayerandGhost);

        //             if (angleBetweenPlayerandGhost < _viewAngle / 2)
        //             {
        //                 if (GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().GetScarable())
        //                     GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().Banish();
        //             }
        //         }
        //     }
        // }
    }

    private IEnumerator ChangeSpotlightColor()
    {
        spotlight.color = Color.red;
        yield return new WaitForSeconds(.5f); //change this to player scare cooldown later
        spotlight.color = Color.white;
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

    #region Getters & Setters
    public PLAYER_STATE GetPlayerState()
    {
        return currentState;
    }

    public BUTTON_PRESS GetButtonPress()
    {
        return _buttonPressed;
    }

    public void SetButtonPress(BUTTON_PRESS state)
    {
        _buttonPressed = state;
    }
    #endregion


    public void TriggerStun()
    {
        StartCoroutine(StunPlayer(_stunTime));
    }

    public IEnumerator StunPlayer(float stunTime)
    {
        anim.SetTrigger("Stunned");
        enabled = false;
        currentState = PLAYER_STATE.STUNNED;
        yield return new WaitForSeconds(stunTime);
        enabled = true;
        currentState = PLAYER_STATE.NORMAL;
    }
    public void UpScare()
    {
        //Debug.Log("Up added");
        _buttonPressed = BUTTON_PRESS.Up;
        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if(GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                //Debug.Log("is active");
                if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, transform.position) <= _scareRange)
                {
                    //Debug.Log("in range");
                    Vector3 dirToGhost = (GhostManager.Instance.bigGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                    //print(angleBetweenPlayerandGhost);

                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        //Debug.Log("angle");
                        GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().AddPlayerScare(this);
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
                        if (GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().CheckIfScarable())
                        {
                            GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().AddPlayerScare(this);
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
                        if (GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().GetScarable())
                            GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().AddPlayerScare(this);
                    }
                }
            }
        }
    }
    public void DownScare()
    {
        _buttonPressed = BUTTON_PRESS.Down;
        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if(GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, transform.position) <= _scareRange)
                {
                    Vector3 dirToGhost = (GhostManager.Instance.bigGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);

                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().AddPlayerScare(this);
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
                        if (GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().CheckIfScarable())
                        {
                            GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().AddPlayerScare(this);
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
                        if (GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().GetScarable())
                            GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().AddPlayerScare(this);
                    }
                }
            }
        }
    }
    public void LeftScare()
    {
        _buttonPressed = BUTTON_PRESS.Left;
        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if(GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, transform.position) <= _scareRange)
                {
                    Vector3 dirToGhost = (GhostManager.Instance.bigGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);

                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().AddPlayerScare(this);
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
                        if (GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().CheckIfScarable())
                        {
                            GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().AddPlayerScare(this);
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
                        if (GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().GetScarable())
                            GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().AddPlayerScare(this);
                    }
                }
            }
        }
    }
    public void RightScare()
    {
        _buttonPressed = BUTTON_PRESS.Right;
        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            if(GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, transform.position) <= _scareRange)
                {
                    Vector3 dirToGhost = (GhostManager.Instance.bigGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);

                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().AddPlayerScare(this);
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
                        if (GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().CheckIfScarable())
                        {
                            GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().AddPlayerScare(this);
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
                        if (GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().GetScarable())
                            GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().AddPlayerScare(this);
                    }
                }
            }
        }
    }
}


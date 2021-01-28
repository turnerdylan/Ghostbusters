using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //references
    private Rigidbody rb;
    private Interactable currentInteraction;
    private WeaponsController weapons;
    private Animator anim;

    //serializables
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] private float _scareRange = 5f;
    [SerializeField] private float _stunTime = 3f;
    [SerializeField] private bool _isStunned = false;

    //private variables
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _inputMoveVector = Vector2.zero;
    private Vector3 _inputLookVector = Vector3.zero;
    private float _storedLookValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        weapons = GetComponentInChildren<WeaponsController>();
        anim = GetComponent<Animator>();
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
        anim.SetBool("Scare", true);

        for (int i = 0; i < GhostManager.Instance.maxBigGhosts; i++)
        {
            //check if ghost is close enough and is active
            if (Vector3.Distance(GhostManager.Instance.bigGhosts[i].transform.position, transform.position) < _scareRange
                && GhostManager.Instance.bigGhosts[i].activeSelf)
            {
                Vector3 dirToGhost = (GhostManager.Instance.bigGhosts[i].transform.position - transform.position).normalized;
                float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                //print(angleBetweenPlayerandGhost);

                if(angleBetweenPlayerandGhost  < _viewAngle / 2)
                {
                    if(Physics.Linecast(transform.position, GhostManager.Instance.bigGhosts[i].transform.position)){
                        //GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().AddPlayerScare(this);
                        //TODO fix this logic
                        GhostManager.Instance.bigGhosts[i].GetComponent<BigGhost>().SplitApart();
                    }
                }
            }
        }

        for (int i = 0; i < GhostManager.Instance.maxMediumGhosts; i++)
        {
            //check if ghost is close enough and is active
            if (Vector3.Distance(GhostManager.Instance.mediumGhosts[i].transform.position, transform.position) < _scareRange
                && GhostManager.Instance.mediumGhosts[i].activeSelf)
            {
                Vector3 dirToGhost = (GhostManager.Instance.mediumGhosts[i].transform.position - transform.position).normalized;
                float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                //print(angleBetweenPlayerandGhost);

                if (angleBetweenPlayerandGhost < _viewAngle / 2)
                {
                    if (Physics.Linecast(transform.position, GhostManager.Instance.mediumGhosts[i].transform.position))
                    {
                        if(GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().GetScarable())
                            GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().SplitApart();
                    }
                }
            }
        }

        for (int i = 0; i < GhostManager.Instance.maxSmallGhosts; i++)
        {
            //check if ghost is close enough and is active
            if (Vector3.Distance(GhostManager.Instance.smallGhosts[i].transform.position, transform.position) < _scareRange
                && GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                Vector3 dirToGhost = (GhostManager.Instance.smallGhosts[i].transform.position - transform.position).normalized;
                float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                //print(angleBetweenPlayerandGhost);

                if (angleBetweenPlayerandGhost < _viewAngle / 2)
                {
                    if (Physics.Linecast(transform.position, GhostManager.Instance.smallGhosts[i].transform.position))
                    {
                        if(GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().GetScarable())
                            GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhost>().Banish();
                    }
                }
            }
        }
    }

    private void SetMoveDirection()
    {
        if (_inputMoveVector.magnitude > 0.5f)
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
        if (_inputMoveVector.magnitude > 0.7f)
        {
            _storedLookValue = Mathf.Atan2(_inputMoveVector.x, _inputMoveVector.y);
            transform.rotation = Quaternion.Euler(0, _storedLookValue * Mathf.Rad2Deg, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, storedLookValue * Mathf.Rad2Deg, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, _storedLookValue * Mathf.Rad2Deg, 0);
            //transform.rotation = Quaternion.Slerp(storedLookValue, storedLookValue * Mathf.Rad2Deg, 0.1f);
        }
    }

    public bool GetStunState()
    {
        return _isStunned;
    }

    public void StunTest()
    {
        StartCoroutine(StunPlayer());
    }

    public IEnumerator StunPlayer()
    {
        enabled = false;
        _isStunned = true;
        yield return new WaitForSeconds(_stunTime);
        enabled = true;
        _isStunned = false;
    }
}


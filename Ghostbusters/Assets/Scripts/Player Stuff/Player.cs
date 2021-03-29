using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public enum PLAYER_STATE
{
    NORMAL,
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
    private Animator anim;
    private PeekabooGhost peekaboo;

    //serializables
    [Header("Player Movement")]
    [SerializeField] public float _moveSpeed = 10f; // wrong spot
    [SerializeField] private float _rotationSpeed = 10f;

    [Header("Scaring")]
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] float _scareRange = 5f;

    [Header("Sprites and other")]
    public GameObject redX;
    public float _stunTime = 3f;
    public int score;
    public Sprite characterSprite;

    //private variables
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _inputMoveVector = Vector2.zero;
    private float _storedLookValue;
    private bool icy = false;

    private int _numberOfHeldGhosts;
    private bool canDive = true;
    private bool backwardsControls = false;
    private PLAYER_STATE currentState = PLAYER_STATE.NORMAL;
    private BUTTON_PRESS _buttonPressed = BUTTON_PRESS.None;
    private bool canMove = true;
    private String[] ahSounds = new String[] {"Ah1", "Ah2", "Ah3", "Ah4"};
    private String[] wahSounds = new String[] {"Wah1", "Wah2", "Wah3", "Wah4"};
    private String[] heySounds = new String[] {"Hey1", "Hey2", "Hey3", "Hey4"};
    private String[] booSounds = new String[] {"Boo1", "Boo2", "Boo3", "Boo4"};
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        peekaboo = FindObjectOfType<PeekabooGhost>();
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            SetMoveDirection();
            SetLookDirection();
        }
    }

    private void OnTriggerEnter(Collider other) //on a capture
    {
        if(other.GetComponent<SmallGhost>())
        {
            GhostManager.Instance.smallGhostsInScene.Remove(other.gameObject);
            Destroy(other.gameObject);
            _numberOfHeldGhosts++;
            UIManager.Instance.UpdateHeldGhosts();
        }
    }
    public void SetMoveVector(Vector2 direction)
    {
        _inputMoveVector = direction;
        if(backwardsControls)
            _inputMoveVector = direction * -1;
    }

    public void FlashX()
    {
        StartCoroutine(DisplayX());
    }

    IEnumerator DisplayX()
    {
        redX.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        redX.SetActive(false);
    }

    public void DepositGhosts()
    {
        if(Vector3.Distance(transform.position, Van.Instance.transform.position) < Van.Instance.GetInteractionRadius())
        {
            score += _numberOfHeldGhosts;
            Van.Instance.DepositGhosts(_numberOfHeldGhosts);
            _numberOfHeldGhosts = 0;
            UIManager.Instance.UpdateHeldGhosts();
        }        
    }

    public void DropGhosts()
    {
        InitiateDisableTrigger(2.0f);

        for (int i = 0; i < _numberOfHeldGhosts; i++)
        {
            var newSmallGhost = Instantiate(GhostManager.Instance.smallGhostPrefab, transform.position, Quaternion.identity);
            newSmallGhost.transform.position = transform.position + new Vector3(Random.value, Random.value, Random.value).normalized * 0.5f;
        }

        GhostManager.Instance.mediumGhostsInScene.Remove(this.gameObject);
        _numberOfHeldGhosts = 0;
        UIManager.Instance.UpdateHeldGhosts();
    }

    public void Dive()
    {
        if(canDive)
        {
            AudioManager.Instance.Play("Dive");
            anim.SetTrigger("Dive");
            TriggerDisableMovement(0.5f);
            canDive = false;
            StartCoroutine(DashSpeed());
        }
    }

    IEnumerator DashSpeed()
    {
        // float speedModifier = 2;
        // _moveSpeed *= speedModifier;
        rb.AddForce(transform.forward*3000);
        //yield return new WaitForSeconds(0.2f);
        // _moveSpeed /= speedModifier;
        StartCoroutine(CantDashDelay());
        yield return null;
    }

    private IEnumerator CantDashDelay()
    {
        yield return new WaitForSeconds(0.8f);
        canDive = true;
    }

    public void TriggerDisableMovement(float time)
    {
        StartCoroutine(DisableMovement(time));
    }

    public IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void SetMoveDirection()
    {
        if (_inputMoveVector.magnitude > 0.3f)
        {
          anim.SetBool("Walk", true);
            _moveDirection = _inputMoveVector * _moveSpeed;
            //rb.MovePosition(transform.position + new Vector3(moveDirection.x, 0, moveDirection.y));

            if(icy)
            {
                rb.AddForce(new Vector3(_moveDirection.x, rb.velocity.y, _moveDirection.y));
            }
            else
            {
                rb.velocity = new Vector3(_moveDirection.x, rb.velocity.y, _moveDirection.y);
            }
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

    public void SetBackwardsControls(bool state)
    {
        backwardsControls = state;
    }

    public void SetIcy(bool state)
    {
        icy = state;
    }

    public float GetScareRange()
    {
        return _scareRange;
    }

    public int GetNumberOfHeldGhosts()
    {
        return _numberOfHeldGhosts;
    }

    #endregion


    public void TriggerStun()
    {
        StartCoroutine(StunPlayer(_stunTime));
    }

    public IEnumerator StunPlayer(float stunTime)
    {
        DropGhosts();
        anim.SetTrigger("Stunned");
        TriggerDisableMovement(1.0f);
        currentState = PLAYER_STATE.STUNNED;
        yield return new WaitForSeconds(stunTime);
        _moveSpeed = 25;
        currentState = PLAYER_STATE.NORMAL;
    }

    public void InitiateDisableTrigger(float time)
    {
        StartCoroutine(DisableTrigger(time));
    }
    public IEnumerator DisableTrigger(float time)
    {
        GetComponent<SphereCollider>().isTrigger = false;
        yield return new WaitForSeconds(time);
        GetComponent<SphereCollider>().isTrigger = true;
    }
    void PlayRandomScare(String[] sounds) => AudioManager.Instance.Play(sounds[UnityEngine.Random.Range(0, sounds.Length)]);
    public void Scare(BUTTON_PRESS buttonDirection)
    {
        if (currentState != PLAYER_STATE.NORMAL) return;

        switch (buttonDirection)
        {
            case BUTTON_PRESS.Up:
                anim.SetBool("ScareUp", true);
                PlayRandomScare(wahSounds);
                break;
            case BUTTON_PRESS.Down:
                anim.SetBool("ScareDown", true);
                PlayRandomScare(ahSounds);
                break;
            case BUTTON_PRESS.Left:
                anim.SetBool("ScareLeft", true);
                PlayRandomScare(booSounds);
                break;
            case BUTTON_PRESS.Right:
                anim.SetBool("ScareRight", true);
                PlayRandomScare(heySounds);
                break;
        }

        if (Vector3.Distance(peekaboo.transform.position, transform.position) <= peekaboo.GetInteractRange())
        {
            peekaboo.SummonGhost();
            return;
        }

        _buttonPressed = buttonDirection;

        for (int i = 0; i < GhostManager.Instance.goldenGhostsInScene.Count; i++)
        {
            if (Vector3.Distance(GhostManager.Instance.goldenGhostsInScene[i].transform.position, transform.position) <= _scareRange)
            {
                Vector3 dirToGhost = (GhostManager.Instance.goldenGhostsInScene[i].transform.position - transform.position).normalized;
                float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);

                if (angleBetweenPlayerandGhost < _viewAngle / 2)
                {
                    GhostManager.Instance.goldenGhostsInScene[i].GetComponent<GoldenGhost>().AddPlayerScare(this);
                }
            }
        }

        for (int i = 0; i < GhostManager.Instance.mediumGhostsInScene.Count; i++)
        {
            if (Vector3.Distance(GhostManager.Instance.mediumGhostsInScene[i].transform.position, gameObject.transform.position) <= _scareRange)
            {
                //print("Distance");
                Vector3 dirToGhost = (GhostManager.Instance.mediumGhostsInScene[i].transform.position - transform.position).normalized;
                float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                //print(angleBetweenPlayerandGhost);

                if (angleBetweenPlayerandGhost < _viewAngle / 2)
                {
                    //print("Angle");
                    GhostManager.Instance.mediumGhostsInScene[i].GetComponent<MediumGhost>().AddPlayerScare(this);
                }
            }
        }
    }
}


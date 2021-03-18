using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private PeekabooGhost peekaboo;

    //serializables
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private int _playerHealth = 3;
    [SerializeField] Sprite heartFilled;
    [SerializeField] Sprite heartEmpty;
    [SerializeField] List<SpriteRenderer> hearts = new List<SpriteRenderer>();
    [SerializeField] private int _numberOfHeldGhosts;

    //private variables
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _inputMoveVector = Vector2.zero;
    private Vector3 _inputLookVector = Vector3.zero;
    private bool canDive = true;
    private float _storedLookValue;
    public PLAYER_STATE currentState = PLAYER_STATE.NORMAL;

    //public
    public Transform testTransform; //delete this later
    public float _scareRange = 5f;
    public float _stunTime = 3f;
    public BUTTON_PRESS _buttonPressed = BUTTON_PRESS.None;
    public Transform handTransform;
    public int score;
    public TextMeshProUGUI heldGhostsText;
    bool canMove = true;

    private void Awake()
    {
        spotlight = GetComponentInChildren<Light>();
        spotlight.spotAngle = _viewAngle;
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
            other.gameObject.SetActive(false);
            _numberOfHeldGhosts++;
            heldGhostsText.text = _numberOfHeldGhosts.ToString();
        }
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
        DropBag();
        // if (currentState == PLAYER_STATE.WITH_BAG) DropBag();

        // else if(currentState == PLAYER_STATE.NORMAL)
        // {
        //     if (Vector3.Distance(Bag.Instance.gameObject.transform.position, transform.position) < Bag.Instance.GetInteractionRadius())
        //     {
        //         Vector3 dirToBag = (Bag.Instance.gameObject.transform.position - transform.position).normalized;
        //         float angleBetweenPlayerandBag = Vector3.Angle(transform.forward, dirToBag);

        //         if (angleBetweenPlayerandBag < _viewAngle / 2)
        //         {
        //             anim.SetBool("Hold", true);
        //             currentState = PLAYER_STATE.WITH_BAG;
        //             Bag.Instance.transform.parent = testTransform;
        //             Bag.Instance.transform.localPosition = Vector3.zero;
        //             Bag.Instance.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //             Destroy(Bag.Instance.GetComponent<Rigidbody>());
        //         }
        //     }
        // }
        // Bag.Instance.SetBagState(Bag.BAG_STATE.PICKED_UP);

    }

    public void DropBag()
    {
        if(Vector3.Distance(transform.position, Van.Instance.transform.position) < Van.Instance.GetInteractionRadius())
        {
            score += _numberOfHeldGhosts;
            Van.Instance.DepositGhosts(_numberOfHeldGhosts);
            _numberOfHeldGhosts = 0;
            heldGhostsText.text = _numberOfHeldGhosts.ToString();
            //Bag.Instance.DepositAllGhosts();
        }
        // else if(currentState == PLAYER_STATE.WITH_BAG)
        // {
        //     anim.SetBool("Hold", false);
        //     Bag.Instance.transform.parent = null;
        //     Bag.Instance.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //     Bag.Instance.gameObject.AddComponent<Rigidbody>();
        //     Bag.Instance.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        //     currentState = PLAYER_STATE.NORMAL;
        // }
        
    }

    public void DropGhosts()
    {
        InitiateDisableTrigger(2.0f);
        int spawnedGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.smallGhosts.Count; i++)
        {
            if (spawnedGhosts >= _numberOfHeldGhosts) break;
            if (!GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                GhostManager.Instance.smallGhosts[i].SetActive(true);
                GhostManager.Instance.smallGhosts[i].transform.position = this.transform.position + new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value).normalized * 0.5f;
                spawnedGhosts++;
            }
        }
        _numberOfHeldGhosts = 0;
        heldGhostsText.text = _numberOfHeldGhosts.ToString();
    }
    public void SwingBag()
    {
        if(currentState == PLAYER_STATE.WITH_BAG)
        {
            anim.SetTrigger("Catch");           
        }
    }

    public void Dive()
    {
        if(canDive)
        {
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
        yield return new WaitForSeconds(0.5f);
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
    public void SwingBagStart()
    {
        //Bag.Instance.GetComponent<CapsuleCollider>().isTrigger = true;
    }

    public void SwingBagEnd()
    {
        //Bag.Instance.GetComponent<CapsuleCollider>().isTrigger = false;
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


    public void LoseHP()
    {
        _playerHealth--;
        hearts[_playerHealth].sprite = heartEmpty;
        if(_playerHealth <= 0)
        {
            StartCoroutine(StunPlayer(_stunTime));
        }
    }

    public void TriggerStun()
    {
        StartCoroutine(StunPlayer(_stunTime));
    }

    public IEnumerator StunPlayer(float stunTime)
    {
        DropBag();
        anim.SetTrigger("Stunned");
        GetComponent<PlayerInputHandler>().enabled = false;
        currentState = PLAYER_STATE.STUNNED;
        yield return new WaitForSeconds(stunTime);
        GetComponent<PlayerInputHandler>().enabled = true;
        _playerHealth = 3;
        hearts[0].sprite = heartFilled;
        hearts[1].sprite = heartFilled;
        hearts[2].sprite = heartFilled;
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
    public void Scare(BUTTON_PRESS buttonDirection)
    {
        if (currentState != PLAYER_STATE.NORMAL) return;

        switch (buttonDirection)
        {
            case BUTTON_PRESS.Up:
                anim.SetBool("ScareUp", true);
                break;
            case BUTTON_PRESS.Down:
                anim.SetBool("ScareDown", true);
                break;
            case BUTTON_PRESS.Left:
                anim.SetBool("ScareLeft", true);
                break;
            case BUTTON_PRESS.Right:
                anim.SetBool("ScareRight", true);
                break;
        }
        
        //StartCoroutine(ChangeSpotlightColor());

        if (Vector3.Distance(peekaboo.transform.position, transform.position) <= peekaboo.GetInteractRange())
        {
            peekaboo.SummonGhost();
            print("test");
            return;
        }

        // foreach(Tower tower in TowerManager.Instance.towers)
        // {
        //     if(Vector3.Distance(tower.transform.position, transform.position) <= _scareRange)
        //     {
        //         tower.LoadScare(buttonDirection);
        //         return;
        //     }
        // }

        // if(Vector3.Distance(TowerManager.Instance.transform.position, transform.position) <= _scareRange && buttonDirection == BUTTON_PRESS.Down)
        // {
        //     TowerManager.Instance.PressButton();
        // }

        _buttonPressed = buttonDirection;
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
                    //print("Distance");
                    Vector3 dirToGhost = (GhostManager.Instance.mediumGhosts[i].transform.position - transform.position).normalized;
                    float angleBetweenPlayerandGhost = Vector3.Angle(transform.forward, dirToGhost);
                    //print(angleBetweenPlayerandGhost);

                    if (angleBetweenPlayerandGhost < _viewAngle / 2)
                    {
                        //print("Angle");
                        GhostManager.Instance.mediumGhosts[i].GetComponent<MediumGhost>().AddPlayerScare(this);
                    }
                }
            }
        }
    }
}


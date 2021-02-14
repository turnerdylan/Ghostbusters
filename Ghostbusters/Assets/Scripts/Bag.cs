using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static Bag Instance
    {
        get
        {
            return instance;
        }
    }

    private static Bag instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    //bag state
    //on ground
    //can be picked up - []
    //in hand
    //can be dropped - O
    //can be dropped on accident
    //can attack (ghosts) - this can be its own button, trigger maybe?
    //can be put on car - O
    //on car
    //can be taken off - []


    //drop and place on car are the same depending on where you are - let that be the O button (east)
    //pick up and remove from car are the same depending on location - call that the square [] button

    public enum BAG_STATE
    {
        ON_GROUND,
        PICKED_UP,
        ON_CAR,
    };


    [SerializeField] private float _interactionRadius = 3f;
    [SerializeField] private int _numberOfHeldGhosts;
    [SerializeField] private int _maxNumberOfGhostsHeld = 10;
    public List<Vector3> caughtGhostSpritePositions = new List<Vector3>(); //offset is + 1 in the y direction
    Vector3 firstListPos;
    [SerializeField] GameObject ghostSprite;

    SpriteRenderer buttonSprite;
    Rigidbody rb;
    BAG_STATE bagState = BAG_STATE.ON_GROUND;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        buttonSprite = GetComponentInChildren<SpriteRenderer>();

        for (int i=0; i<_maxNumberOfGhostsHeld; i++)
        {
            caughtGhostSpritePositions.Add(transform.position + new Vector3(0, i + 2, 0)); //make this parented to the player
        }
        
    }

    private void Update()
    {
        if(bagState != BAG_STATE.PICKED_UP)
        {
            foreach (Player player in PlayerManager.Instance.players)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < _interactionRadius && !rb.isKinematic)
                {
                    buttonSprite.enabled = true;
                }
                else
                {
                    buttonSprite.enabled = false;
                }
            }
        }
        else
        {
            buttonSprite.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) //on a capture
    {
        if(other.GetComponent<SmallGhost>())
        {
            other.gameObject.SetActive(false);
            _numberOfHeldGhosts++;
            Instantiate(ghostSprite, caughtGhostSpritePositions[_numberOfHeldGhosts], Quaternion.identity);
        }
    }

    public void DropBagDueToGhostHit()
    {

    }

    public float GetInteractionRadius()
    {
        return _interactionRadius;
    }

    public void SetBagState(BAG_STATE state)
    {
        bagState = state;
    }

}

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

    [SerializeField] private float _interactionRadius = 3f;
    [SerializeField] private int _numberOfHeldGhosts;

    SpriteRenderer buttonSprite;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        buttonSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        foreach(Player player in PlayerManager.Instance.players)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < _interactionRadius && !rb.isKinematic)
            {
                buttonSprite.enabled = true;
            }
            else
            {
                buttonSprite.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) //on a capture
    {
        if(other.GetComponent<SmallGhost>())
        {
            other.gameObject.SetActive(false);
            _numberOfHeldGhosts++;
        }
    }

    public void GetBag(Player player)
    {
        //set player to holding bag state
        //set player holding bag animation
        //disable player scaring controls
        //parent bag to the player
        transform.parent = player.transform;
        transform.position = player.testTransform.position;
    }

    //
    public void DropBag() //reason: 1=on purpose, 3, placed on car
    {
        //if player is near the car, drop bag on car
        //otherwise drop on ground near player
    }

    public void DropBagOnGround()
    {
        //spawn all ghosts in bag
        for(int i=0; i<_numberOfHeldGhosts; i++)
        {
            int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.smallGhosts);
            GhostManager.Instance.smallGhosts[index].gameObject.SetActive(true);
            GhostManager.Instance.smallGhosts[index].transform.position = transform.position;
        }


    }

    public void DropBagDueToGhostHit()
    {

    }

    public float GetInteractionRadius()
    {
        return _interactionRadius;
    }

}

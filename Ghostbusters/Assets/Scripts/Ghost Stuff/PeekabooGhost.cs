﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekabooGhost : MonoBehaviour
{
    ////references
    //3 ghosts sizes?

    ////variables
    //list of transforms
    //interaction range
    //timer for changing locations
    [SerializeField] List<Transform> locations = new List<Transform>();
    [SerializeField] float interactionRange;
    [SerializeField] float cantTeleportTimerMax = 3f;
    float cantTeleportTimer;

    Animator anim;
    
    int currentLocationIndex;
    bool canTeleport = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        currentLocationIndex = Random.Range(0, locations.Count);
        transform.position = locations[currentLocationIndex].position;
        cantTeleportTimer = cantTeleportTimerMax;
    }

    private void Update()
    {
        cantTeleportTimer -= Time.deltaTime;
        //look at player
        var target = PlayerManager.Instance.GetClosestPlayer();
        Vector3 targetPostition = new Vector3(target.position.x, transform.position.y, target.position.z);
        this.transform.LookAt(targetPostition);

        //
        if (Vector3.Distance(transform.position, target.position) < 10 && cantTeleportTimer < 0)
        {
            ChangeLocations();
        }
    }

    private IEnumerator GoDown()
    {
        //get some values
        float timeElapsed = 0;
        float timeToGoDown = 50;
        Vector3 goToPosition = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);


        //lerp the alpha
        while (timeElapsed < timeToGoDown)
        {
            transform.position = Vector3.Lerp(transform.position, goToPosition, timeElapsed / timeToGoDown);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    ////functions
    //change locations
    //interact, get scared out of peekaboo state
    //summon ghost
    private void ChangeLocations()
    {

        int tempIndex = Random.Range(0, locations.Count);
        while(tempIndex == currentLocationIndex)
        {
            tempIndex = Random.Range(0, locations.Count);
        }
        currentLocationIndex = tempIndex;
        transform.position = locations[currentLocationIndex].position;
        cantTeleportTimer = cantTeleportTimerMax;
    }

    public void SummonGhost()
    {
        //int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.bigGhosts);
        int index = Random.Range(0, GhostManager.Instance.mediumGhosts.Count);
        GhostManager.Instance.mediumGhosts[index].SetActive(true);
        GhostManager.Instance.mediumGhosts[index].transform.position = transform.position;
    }

    public float GetInteractRange()
    {
        return interactionRange;
    }




}
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
    [SerializeField] float cantTeleportTimerMax = 5f;
    float cantTeleportTimer;
    [SerializeField] float timeToLerp = 2.0f;
    public float distanceToMove = 2.0f;
    public float changeLocationsTime;

    [Range(0, 100)]
    public float randomGoldenWeight = 80f;

    private bool isLerping;
    private bool shouldMoveDown = false;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timeStartedLerping;
    public GameObject particles;
    
    int currentLocationIndex;
    private bool moveParticles;
    private int lastLocationIndex;


    private void Start()
    {
        currentLocationIndex = Random.Range(0, locations.Count);
        transform.position = locations[currentLocationIndex].position;
        cantTeleportTimer = cantTeleportTimerMax;
        particles.GetComponent<ParticleSystem>().Stop();
        StartLerping();
    }

    void FixedUpdate()
    {
        var target = PlayerManager.Instance.GetClosestPlayer();
        if(target)
        {
            Vector3 targetPostition = new Vector3(target.position.x, transform.position.y, target.position.z);
            this.transform.LookAt(targetPostition);
        }
        if(isLerping)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;

            if(moveParticles) // for lerping particles
            {
                float percentageComplete = timeSinceStarted / changeLocationsTime;
                particles.transform.position = Vector3.Lerp(startPos, endPos, percentageComplete);

                if(percentageComplete >= 1.0f)
                {
                    particles.GetComponent<ParticleSystem>().Stop();
                    isLerping = false;
                }
            }
            else //for lerping ghost, either up or down
            {
                float percentageComplete = timeSinceStarted / timeToLerp;
                transform.position = Vector3.Lerp(startPos, endPos, percentageComplete);

                if(percentageComplete >= 1.0f)
                {
                    isLerping = false;
                    if(!shouldMoveDown)
                    {
                        ChangeLocations();
                    }
                }
            }
        }
        else
        {
            cantTeleportTimer -= Time.deltaTime;
            if(target)
            {
                if (Vector3.Distance(transform.position, target.position) < 25 && cantTeleportTimer < 0)
                {
                    StartLerping();
                }
            }
        }
    }

    void StartLerping()
    {
        isLerping = true;
        timeStartedLerping = Time.time;

        if(moveParticles)
        {
            particles.GetComponent<ParticleSystem>().Play();
            startPos = locations[lastLocationIndex].position + new Vector3(0, 3.78f, 0);
            endPos = locations[currentLocationIndex].position + new Vector3(0, 3.78f, 0);
        }
        else
        {  
            startPos = transform.position;
            if(shouldMoveDown)
            {
                AudioManager.Instance.Play("Peekaboo Down");
                endPos = transform.position - Vector3.up*distanceToMove; //make endPos below current pos
                shouldMoveDown = false;
            }
            else
            {
                AudioManager.Instance.Play("Peekaboo Up");
                endPos = transform.position + Vector3.up*distanceToMove; //make endPos above current pos
                shouldMoveDown = true;
            }
        }
    }
    private void ChangeLocations()
    {
        lastLocationIndex = currentLocationIndex;
        int tempIndex = Random.Range(0, locations.Count);
        while (tempIndex == currentLocationIndex)
        {
            tempIndex = Random.Range(0, locations.Count);
        }
        currentLocationIndex = tempIndex;
        transform.position = locations[currentLocationIndex].position;
        cantTeleportTimer = cantTeleportTimerMax;
        moveParticles = true;
        StartLerping();
        StartCoroutine(WaitToLerp());
    }

    public void SummonGhost()
    {
        print("scared!");
        int ghostRange = Random.Range(0, 100);
        if(ghostRange < randomGoldenWeight)//typically 0.8?
        {
            //int randomIndex = Random.Range(0, 5);
            int randomIndex = 3;

            GameObject newGhost = Instantiate(GhostManager.Instance.mediumGhostPrefabs[randomIndex], transform.position, Quaternion.identity);
            GhostManager.Instance.mediumGhostsInScene.Add(newGhost);
            newGhost.GetComponent<MediumGhost>().GenerateSequence();
        }
        else
        {
            //int randomIndex = Random.Range(0, 5);

            int randomIndex = 3;
            GameObject newGhost = Instantiate(GhostManager.Instance.goldenGhostPrefabs[randomIndex], transform.position, Quaternion.identity);
            GhostManager.Instance.goldenGhostsInScene.Add(newGhost);
            newGhost.GetComponent<GoldenGhost>().GenerateSequence();
        }
        ChangeLocations();
    }

    public float GetInteractRange()
    {
        return interactionRange;
    }

    IEnumerator WaitToLerp()
    {
        yield return new WaitForSeconds(changeLocationsTime);
        moveParticles = false;
        StartLerping();
    }




}

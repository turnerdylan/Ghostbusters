using System.Collections;
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
    private bool isLerping;
    private bool shouldMoveDown = false;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timeStartedLerping;
    public GameObject particles;
    Animator anim;
    
    int currentLocationIndex;
    bool canTeleport = false;
    private bool moveParticles;
    private int lastLocationIndex;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
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
            print("particle start");
            particles.GetComponent<ParticleSystem>().Play();
            startPos = locations[lastLocationIndex].position + new Vector3(0, 3.78f, 0);
            endPos = locations[currentLocationIndex].position + new Vector3(0, 3.78f, 0);
        }
        else
        {  
            print("ghost lerp start");
            startPos = transform.position;
            if(shouldMoveDown)
            {
                endPos = transform.position - Vector3.up*distanceToMove; //make endPos below current pos
                shouldMoveDown = false;
            }
            else
            {
                endPos = transform.position + Vector3.up*distanceToMove; //make endPos above current pos
                shouldMoveDown = true;
            }
        }
    }

    // private IEnumerator GoDown()
    // {
    //     //get some values
    //     float timeElapsed = 0;
    //     float timeToGoDown = 50;
    //     Vector3 goToPosition = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);

    //     while (timeElapsed < timeToGoDown)
    //     {
    //         transform.position = Vector3.Lerp(transform.position, goToPosition, timeElapsed / timeToGoDown);
    //         timeElapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     yield return null;
    // }

    // IEnumerator LerpFunction()
    // {
    //     print("test1");
    //     float time = 0;


    //     while (time < timeToLerp)
    //     {
    //         gameObject.transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0, 50, 0), time / timeToLerp);

    //         time += Time.deltaTime;
    //         yield return null;
    //     }
    //     transform.position = transform.position - new Vector3(0, 50, 0);
    // }

    ////functions
    //change locations
    //interact, get scared out of peekaboo state
    //summon ghost
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

        //int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.bigGhosts);
        int ghostRange = Random.Range(0, GhostManager.Instance.mediumGhosts.Count+GhostManager.Instance.bigGhosts.Count);
        if(ghostRange < GhostManager.Instance.mediumGhosts.Count)
        {
            int index = Random.Range(0, GhostManager.Instance.mediumGhosts.Count);
            GhostManager.Instance.mediumGhosts[index].SetActive(true);
            GhostManager.Instance.mediumGhosts[index].transform.position = transform.position;
        }
        else
        {
            int index = Random.Range(0, GhostManager.Instance.bigGhosts.Count);
            GhostManager.Instance.bigGhosts[index].SetActive(true);
            GhostManager.Instance.bigGhosts[index].transform.position = transform.position;
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

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
    [SerializeField] float changeLocationsTimerMax = 5f;
    Animator anim;
    [SerializeField] float animDuration;
    [SerializeField] float startValue = 0;
    [SerializeField] float endValue = -3;
    
    int currentLocationIndex;
    public float changeLocationsTimer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        changeLocationsTimer = changeLocationsTimerMax;
        currentLocationIndex = Random.Range(0, locations.Count);
        transform.position = locations[currentLocationIndex].position;
    }

    private void Update()
    {
        var target = PlayerManager.Instance.GetClosestPlayer();
        Vector3 targetPostition = new Vector3(target.position.x, transform.position.y, target.position.z);
        this.transform.LookAt(targetPostition);

        changeLocationsTimer -= Time.deltaTime;
        if(changeLocationsTimer < 0)
        {
            //StartCoroutine(GoDown());
            changeLocationsTimer = changeLocationsTimerMax;
            ChangeLocations();
        }
    }

    /*private IEnumerator GoDown()
    {
        float timeElapsed = 0;

        while (timeElapsed < animDuration)
        {
            transform.position.y = Mathf.Lerp(startValue, endValue, timeElapsed / animDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        valueToLerp = endValue;
    }*/

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
    }

    public void SummonGhost()
    {
        int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.bigGhosts);
        GhostManager.Instance.bigGhosts[index].SetActive(true);
        GhostManager.Instance.bigGhosts[index].transform.position = transform.position;
    }

    public float GetInteractRange()
    {
        return interactionRange;
    }




}

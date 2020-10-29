using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public enum AI_GHOST_STATE
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK,
    RUNNING,
    CAUGHT
};

[RequireComponent(typeof(Rigidbody))]
public class Ghost : MonoBehaviour
{
    AI_GHOST_STATE currentState = AI_GHOST_STATE.IDLE;

    public NavMeshAgent agent;
    private TextMeshPro _stateText;
    private PortalParent portal;

    private float _timeToBreakFree = 5f;
    private float _timer;
    private float _speed = 5f;
    [SerializeField]
    private float _distanceToNoticePlayer = 5f;
    private float _attackDelay;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        _stateText = GetComponentInChildren<TextMeshPro>();
        player = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(State_Idle());
        portal = FindObjectOfType<PortalParent>();
    }

    private void Update()
    {
        print(currentState);
    }

    public void ResetTimer()
    {
        _timer = _timeToBreakFree;
    }

    public void SetStateText(string input)
    {
        _stateText.text = input;
    }

    public IEnumerator State_Idle()
    {
        currentState = AI_GHOST_STATE.IDLE;
        _stateText.text = "idle";
        //anim.settrigger(idle)

        while(currentState == AI_GHOST_STATE.IDLE)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < _distanceToNoticePlayer)
            {
                //StartCoroutine(State_Chase());
                //StartCoroutine(State_Running());
                StartCoroutine(State_Patrol());
                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator State_Chase()
    {
        currentState = AI_GHOST_STATE.CHASE;
        _stateText.text = "chase";
        agent.isStopped = false;
        agent.stoppingDistance = 3;
        //TODO anim.settrigger(chase)

        while(currentState == AI_GHOST_STATE.CHASE)
        {
            agent.destination = (player.transform.position);

            while (true)
            {
                agent.destination = (player.transform.position);
                yield return null;

                //if player is outside of range
                if (Vector3.Distance(transform.position, player.transform.position) > _distanceToNoticePlayer)
                {
                    StartCoroutine(State_Idle());
                    break;
                }
                else break;
                

            }
            if (Vector3.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                StartCoroutine(State_Attack());
                yield break;
            }

                yield return null;
        }
    }

    private IEnumerator State_Attack()
    {
        currentState = AI_GHOST_STATE.ATTACK;
        _stateText.text = "attack";
        //set anim trigger

        agent.isStopped = true;

        float elapsedTime = 0f;

        while (currentState == AI_GHOST_STATE.ATTACK)
        {
            //timer for attack
            elapsedTime += Time.deltaTime;

            if (Vector3.Distance(transform.position, player.transform.position) > agent.stoppingDistance)
            {
                StartCoroutine(State_Chase());
                yield break;
            }

            if(elapsedTime >= _attackDelay)
            {
                elapsedTime = 0;

                print("attacked");
            }

            yield return null;
        }
    }

    public IEnumerator State_Running()
    {
        currentState = AI_GHOST_STATE.RUNNING;
        _stateText.text = "running";
        //anim.settrigger(running)

        Transform randomWaypoint = portal.portals[Random.Range(0, portal.portals.Count)].transform;

        agent.destination = (randomWaypoint.position);
        agent.stoppingDistance = 0;

        while (currentState == AI_GHOST_STATE.RUNNING)
        {
            
            if(agent.remainingDistance <= 2f)
            {
                StartCoroutine(State_Idle());
                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator State_Patrol()
    {
        currentState = AI_GHOST_STATE.PATROL;
        _stateText.text = "patrol";
        //anim.settrigger(patrol)

        int currentIndex = GetNewWaypointIndex();
        Transform randomWaypoint = portal.portals[currentIndex].transform;

        agent.destination = (randomWaypoint.position);
        agent.stoppingDistance = 0;
        agent.isStopped = false;

        while (currentState == AI_GHOST_STATE.PATROL)
        {
            if(Vector3.Distance(transform.position, agent.destination) <= 1)
            {
                //StartCoroutine(State_Idle());
                int newIndex = GetNewWaypointIndex();
                while (newIndex == currentIndex)
                {
                    newIndex = GetNewWaypointIndex();
                }
                currentIndex = newIndex;
                agent.destination = portal.portals[currentIndex].transform.position;
            }

            yield return null;
        }

        yield return null;
    }

    private int GetNewWaypointIndex()
    {
        return Random.Range(0, portal.portals.Count);
    }


    public IEnumerator State_Caught()
    {
        currentState = AI_GHOST_STATE.CAUGHT;
        _stateText.text = "caught";
        //anim.settrigger(caught)

        agent.isStopped = true;

        while (currentState == AI_GHOST_STATE.CAUGHT)
        {
            //if(condition to change state)
            //startcoroutine(state_newstate)
            //yield break

            //if we reach the new destination {
            //change state to idle
            //yield break }

            yield return null;
        }

        yield return null;
    }




    public void SetState(AI_GHOST_STATE state)
    {
        currentState = state;
    }




}

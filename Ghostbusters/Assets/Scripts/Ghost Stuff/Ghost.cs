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
    private PortalController portal;

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
        portal = FindObjectOfType<PortalController>();
    }

    private void Update()
    {
        print(currentState);
        //agent.destination = player.transform.position;
    }

    // Update is called once per frame
    /*void Update()
    {
        if(isInNet)
        {
            _timer -= Time.deltaTime;
            SetStateText("In net! " +_timer.ToString("F0"));
            if(_timer < 0)
            {
                currentNet.ReleaseGhost(this);
            }
        }
        else if(isInTrap)
        {
            ResetTimer();
        } else if(isInLasso)
        {

        }
    }*/

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

        agent.isStopped = true;

        while(currentState == AI_GHOST_STATE.IDLE)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < _distanceToNoticePlayer)
            {
                StartCoroutine(State_Chase());
                //StartCoroutine(State_Running());
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


        yield return null;
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

        int randomPortalIndex = Random.Range(0, portal.portals.Count);

        agent.isStopped = false;

        while (currentState == AI_GHOST_STATE.RUNNING)
        {
            agent.destination = portal.portals[randomPortalIndex].transform.position;
            if(agent.remainingDistance <= 2f)
            {
                print("made it");
                gameObject.transform.position = portal.portals[Random.Range(0, portal.portals.Count)].transform.position;
                StartCoroutine(State_Idle());
            }
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

    public IEnumerator State_Patrol()
    {
        currentState = AI_GHOST_STATE.PATROL;
        _stateText.text = "patrol";
        //anim.settrigger(patrol)

        //pick a random waypoint to patrol to
        //set ai navmesh to move to it



        while (currentState == AI_GHOST_STATE.PATROL)
        {
            //if(condition to change state)
            //startcoroutine(state_newstate)
            //yield break

            //if we reach the new destination {
            //change state to idle
            //yield break }

            yield return null;
        }
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

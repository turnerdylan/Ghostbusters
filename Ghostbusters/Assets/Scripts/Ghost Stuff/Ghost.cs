using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public NetController currentNet;
    private float _timeToBreakFree = 5f;
    private float _timer;
    private float _speed = 5f;
    private TextMeshPro _stateText;


    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        _stateText = GetComponentInChildren<TextMeshPro>();
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

        //anim.settrigger(idle)

        //stop movement

        while(currentState == AI_GHOST_STATE.IDLE)
        {
            //if(condition to change state)
            //startcoroutine(state_newstate)
            //yield break

            yield return null;
        }
    }

    public IEnumerator State_Patrol()
    {
        currentState = AI_GHOST_STATE.PATROL;

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




}

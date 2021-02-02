using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    //references
    private NavMeshAgent agent;
    private Animator anim;

    //private serializables
    [SerializeField] private GameObject boxTest;
    [SerializeField] private float _attackRange = 8;

    //private variables
    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(PlayerManager.Instance.players.Length > 0)
            agent.SetDestination(GetClosestPlayer(PlayerManager.Instance.players).gameObject.transform.position);

        if (Vector3.Distance(transform.position, GetClosestPlayer(PlayerManager.Instance.players).gameObject.transform.position) < _attackRange)
        {
            anim.SetBool("Attack", true);
        }
    }

    public void EndAnimation(string name)
    {
        anim.SetBool(name, false);
    }

    private void OnDisable()
    {
        if(agent)
        {
            boxTest.gameObject.SetActive(false);
            agent.speed = 5;
        }
        
    }

Transform GetClosestPlayer(Player[] players)
    {
        Transform tMin = null;
        float distanceToClosestPlayer = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Player t in players)
        {
            if(t.GetPlayerState() != PLAYER_STATE.STUNNED)
            {
                float currentCheckDistance = Vector3.Distance(t.transform.position, currentPos);
                if (currentCheckDistance < distanceToClosestPlayer)
                {
                    tMin = t.transform;
                    distanceToClosestPlayer = currentCheckDistance;
                }
            }
            
        }
        if (tMin == null) return tMin;
        return tMin;
    }
}
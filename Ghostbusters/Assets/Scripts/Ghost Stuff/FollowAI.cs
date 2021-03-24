using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    //references
    private NavMeshAgent agent;
    private Animator anim;
    private Rigidbody rb;
    public GameObject hitBox;

    //private serializables
    [SerializeField] private float _attackRange = 8;

    //private variables
    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (rb.velocity.magnitude > 0) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);

        if (PlayerManager.Instance.GetPlayerArray().Count > 0 && agent)
            agent.SetDestination(GetClosestPlayer(PlayerManager.Instance.GetPlayerArray()).gameObject.transform.position);

        if (Vector3.Distance(transform.position, GetClosestPlayer(PlayerManager.Instance.GetPlayerArray()).gameObject.transform.position) < _attackRange)
        {
            anim.SetBool("Attack", true);
            hitBox.SetActive(true);
            StartCoroutine(EndAttack());
        }
    }

    public IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("Attack", false);
        hitBox.SetActive(false);
    }

    private void OnDisable()
    {
        if(agent)
        {
            agent.speed = 5;
        }
        
    }

Transform GetClosestPlayer(List<Player> players)
    {
        if (PlayerManager.Instance.CheckIfAllPlayersAreStunned())
        {
            agent.ResetPath();
            return null;
        }

        Transform closestPlayerTransform = null;
        float distanceToClosestPlayerTemp = Mathf.Infinity;

        foreach (Player player in players)
        {
            if(player.GetPlayerState() != PLAYER_STATE.STUNNED)
            {
                float currentCheckDistance = Vector3.Distance(player.transform.position, transform.position);
                if (currentCheckDistance < distanceToClosestPlayerTemp)
                {
                    closestPlayerTransform = player.transform;
                    distanceToClosestPlayerTemp = currentCheckDistance;
                }
            }
            
        }
        if (closestPlayerTransform == null) return closestPlayerTransform;
        return closestPlayerTransform;
    }
}
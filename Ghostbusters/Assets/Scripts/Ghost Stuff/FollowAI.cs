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

    //private serializables
    [SerializeField] private float _speed = 10f;

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
            agent.SetDestination(PlayerManager.Instance.GetClosestPlayer().position);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSwingAttack : MonoBehaviour
{
    public float _attackRange = 8;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerManager.Instance.GetClosestPlayer().position) < _attackRange)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    public void TriggerHeadAttack(Player player)
    {
        player.TriggerStun();
    }
}

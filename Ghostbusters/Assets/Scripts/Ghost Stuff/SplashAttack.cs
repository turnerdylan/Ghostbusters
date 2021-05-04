using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : MonoBehaviour
{
    public float _attackRange = 8;
    public float _splashRange = 8;
    private Animator anim;
    private bool canAttack = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerManager.Instance.GetClosestPlayer(transform).position) < _attackRange && canAttack)
        {
            AudioManager.Instance.Play(AudioManager.Instance.sounds[UnityEngine.Random.Range(18, 40)].name);
            anim.SetTrigger("Attack");
            canAttack = false;
        }
    }

    public void TriggerSplashAttack()
    {
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            if (Vector3.Distance(transform.position, player.transform.position) < _splashRange)
            {
                player.TriggerStun();
            }
        }
        canAttack = true;
    }

    
}

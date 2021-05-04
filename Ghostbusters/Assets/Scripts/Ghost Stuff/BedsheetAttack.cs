using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedsheetAttack : MonoBehaviour
{
    public float _attackRange = 8;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerManager.Instance.GetClosestPlayer(transform).position) < _attackRange)
        {
            AudioManager.Instance.Play(AudioManager.Instance.sounds[UnityEngine.Random.Range(18, 40)].name);
            anim.SetTrigger("Attack");
        }
    }
}

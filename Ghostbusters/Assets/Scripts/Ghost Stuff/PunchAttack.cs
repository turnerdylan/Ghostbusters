using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    public GameObject hand;
    private Animator anim;
    [SerializeField] private float attackTimerMax;
    private float _attackTimer;
    private bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _attackTimer = attackTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        _attackTimer -= Time.deltaTime;
        if(_attackTimer < 0 && !attacking)
        {
            attacking = true;
            StartCoroutine(GetComponent<LegGhostMovement>().State_Attack());
            anim.SetTrigger("Attack");
            hand.SetActive(true);
            StartCoroutine(EndAttack());
        }
    }

    public IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(1.0f);
        //anim.SetBool("Attack", false);
        hand.SetActive(false);
        _attackTimer = attackTimerMax;
        attacking = false;
        StartCoroutine(GetComponent<LegGhostMovement>().State_Follow());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private float attackTimer;
    [SerializeField] private float force;
    private float _attackTimer;
    private bool attacking;
    void Start()
    {
        _attackTimer = attackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        _attackTimer -= Time.deltaTime;
        if(_attackTimer < 0 && !attacking)
        {
            attacking = true;
            AudioManager.Instance.Play(AudioManager.Instance.sounds[UnityEngine.Random.Range(18, 40)].name);
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        GameObject go = Instantiate(projectile, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * force);
        _attackTimer = attackTimer;
        attacking = false;
    }
}

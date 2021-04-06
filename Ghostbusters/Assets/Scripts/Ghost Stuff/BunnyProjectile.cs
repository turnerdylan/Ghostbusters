using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyProjectile : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    [SerializeField] private float explosionRange;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            foreach(Player player in PlayerManager.Instance.GetPlayerArray())
            {
                if(Vector3.Distance(transform.position, player.transform.position) <= explosionRange)
                {
                    player.DropGhosts();
                }
            }
            Destroy(gameObject);
        }
    }
}

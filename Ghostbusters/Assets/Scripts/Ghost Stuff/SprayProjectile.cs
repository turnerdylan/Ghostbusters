using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayProjectile : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            other.GetComponent<Player>().DropGhosts();
            Destroy(this.gameObject);
        }
    }
}

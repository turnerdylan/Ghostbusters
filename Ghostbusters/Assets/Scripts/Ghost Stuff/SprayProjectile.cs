using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayProjectile : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            other.GetComponent<Player>().TriggerStun();
            Destroy(this.gameObject);
        }
    }
}

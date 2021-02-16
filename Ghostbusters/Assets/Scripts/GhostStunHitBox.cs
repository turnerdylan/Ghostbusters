using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStunHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("triggered");
        if(other.GetComponent<Player>())
        {
            print("stunned");
            other.GetComponent<Player>().TriggerStun();
            PlayerManager.Instance.CheckIfAllPlayersAreStunned();
        }
    }
}

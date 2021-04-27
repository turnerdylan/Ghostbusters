using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            other.gameObject.transform.position = respawnPoint.position;
        }
    }
}

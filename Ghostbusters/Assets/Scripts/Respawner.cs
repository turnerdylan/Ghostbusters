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
            other.GetComponent<Player>()._numberOfHeldGhosts = 0;
            UIManager.Instance.UpdateHeldGhosts();
            other.gameObject.transform.position = respawnPoint.position;
        }
    }
}

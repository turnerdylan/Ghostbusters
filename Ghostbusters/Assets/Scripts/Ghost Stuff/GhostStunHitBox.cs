using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStunHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            AudioManager.Instance.Play(AudioManager.Instance.sounds[UnityEngine.Random.Range(18, 40)].name);
            other.GetComponent<Player>().TriggerStun();
        }
    }
}

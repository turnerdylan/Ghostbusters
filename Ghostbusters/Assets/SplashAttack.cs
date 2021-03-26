using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : MonoBehaviour
{

    public void TriggerSplashAttack()
    {
        foreach(Player player in PlayerManager.Instance.players)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < 6)
            {
                player.TriggerStun();
            }
        }
    }
}

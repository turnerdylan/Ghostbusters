using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSwingAttack : MonoBehaviour
{
    public void TriggerHeadAttack(Player player)
    {
        player.TriggerStun();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStunHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            other.GetComponent<Player>().TriggerStun();
        }
    }
}

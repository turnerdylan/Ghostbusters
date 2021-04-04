﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            other.GetComponent<Player>().DropGhosts();
        }
    }
}

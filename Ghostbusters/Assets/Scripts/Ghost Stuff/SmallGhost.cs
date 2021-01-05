using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGhost : MonoBehaviour
{
    private void Start()
    {
        GhostManager.Instance.smallGhosts.Add(gameObject);
    }
}

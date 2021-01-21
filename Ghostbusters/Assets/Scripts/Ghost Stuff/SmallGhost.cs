using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGhost : MonoBehaviour
{
    public int listIndex = -1;

    bool canTransform = false;
    public float transformDelay;
    public float transformTimer;

    private void Start()
    {
        transformTimer = transformDelay;
    }

    private void Update()
    {
        if(canTransform)
        {
            transformTimer -= Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        canTransform = true;
        transformTimer = transformDelay;
    }

    public void Bansish()
    {
        this.gameObject.SetActive(false);
    }
}

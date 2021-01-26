using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGhost : MonoBehaviour
{
    public int listIndex = -1;

    bool canTransform = false;
    public bool scarable;
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
        scarable = false;
        StartCoroutine(ScareInvincibility());
    }

    public void Banish()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator ScareInvincibility()
    {
        yield return new WaitForSeconds(1.75f);
        scarable = true;
    }
}

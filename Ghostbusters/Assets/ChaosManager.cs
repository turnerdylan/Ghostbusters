using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChaosManager : MonoBehaviour
{
    [SerializeField] GameObject smokeBomb;

    private void Update()
    {
        if(Keyboard.current.kKey.wasPressedThisFrame)
        {
            PickChaosEvent(0);
        }
    }

    public void PickChaosEvent(int eventKey)
    {
        switch (eventKey)
        {
            case 0:
                StartCoroutine(SuperSpeed());
                break;
            case 1:
                StartCoroutine(BackwardsControls());
                break;
            case 2:
                StartCoroutine(LightsOut()); //not implemented yet
                break;
            case 3:
                StartCoroutine(IcyFloor());
                break;
            case 4:
                SmokeBomb();
                break;
        }
    }

    private IEnumerator SuperSpeed()
    {
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player._moveSpeed = 100;
        }
        yield return new WaitForSeconds(5);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player._moveSpeed = 25;
        }
    }

    private IEnumerator BackwardsControls()
    {
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.SetBackwardsControls(true);
        }
        yield return new WaitForSeconds(5);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.SetBackwardsControls(false);
        }
    }

    private IEnumerator LightsOut()
    {
        print("not implemented yet");
        yield return null;
    }

    private IEnumerator IcyFloor()
    {
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.GetComponent<Rigidbody>().drag = .5f;
        }
        yield return new WaitForSeconds(5);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.GetComponent<Rigidbody>().drag = 5f;
        }
    }

    private void SmokeBomb()
    {
        Instantiate(smokeBomb, transform.position, Quaternion.identity);
    }
}

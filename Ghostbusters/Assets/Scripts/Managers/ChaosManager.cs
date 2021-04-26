using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChaosManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static ChaosManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static ChaosManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] GameObject smokeBomb;
    [SerializeField] float chaosEventTime = 5f;
    //backwards, invis, slippery, smoke, speed
    [SerializeField] List<Sprite> chaosSprites = new List<Sprite>();
    [SerializeField] GameObject chaosEventUI;

    public void PickChaosEvent(int eventKey, Vector3 pos)
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
                StartCoroutine(Invisibility());
                break;
            case 3:
                StartCoroutine(IcyFloor());
                break;
            case 4:
                StartCoroutine(SmokeBomb(pos));
                break;
        }
    }

    private IEnumerator SuperSpeed()
    {
        chaosEventUI.SetActive(true);
        //chaosEventUI.GetComponent<Image>().sprite = chaosSprites[4]. sprite;
        chaosEventUI.GetComponent<Animator>().SetTrigger("ChaosEvent");
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player._moveSpeed = 100;
        }
        yield return new WaitForSeconds(chaosEventTime);
        chaosEventUI.GetComponent<Image>().sprite = null;
        chaosEventUI.SetActive(false);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player._moveSpeed = 25;
        }
    }

    private IEnumerator BackwardsControls()
    {
        chaosEventUI.SetActive(true);
        chaosEventUI.GetComponent<Image>().sprite = chaosSprites[0];
        chaosEventUI.GetComponent<Animator>().SetTrigger("ChaosEvent");
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.SetBackwardsControls(true);
        }
        yield return new WaitForSeconds(chaosEventTime);
        chaosEventUI.GetComponent<Image>().sprite = null;
        chaosEventUI.SetActive(false);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.SetBackwardsControls(false);
        }
    }

    private IEnumerator Invisibility()
    {
        chaosEventUI.SetActive(true);
        chaosEventUI.GetComponent<Image>().sprite = chaosSprites[1];
        chaosEventUI.GetComponent<Animator>().SetTrigger("ChaosEvent");
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        yield return new WaitForSeconds(chaosEventTime);

        chaosEventUI.GetComponent<Image>().sprite = null;
        chaosEventUI.SetActive(false);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    private IEnumerator IcyFloor()
    {
        chaosEventUI.SetActive(true);
        chaosEventUI.GetComponent<Image>().sprite = chaosSprites[2];
        chaosEventUI.GetComponent<Animator>().SetTrigger("ChaosEvent");
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.SetIcy(true);
            player.GetComponent<Rigidbody>().drag = 0;
        }
        yield return new WaitForSeconds(chaosEventTime);

        chaosEventUI.GetComponent<Image>().sprite = null;
        chaosEventUI.SetActive(false);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.SetIcy(false);
            player.GetComponent<Rigidbody>().drag = 5;
        }
    }

    private IEnumerator SmokeBomb(Vector3 smokePosition)
    {
        chaosEventUI.SetActive(true);
        chaosEventUI.GetComponent<Image>().sprite = chaosSprites[3];
        chaosEventUI.GetComponent<Animator>().SetTrigger("ChaosEvent");
        Instantiate(smokeBomb, smokePosition, Quaternion.identity);
        yield return new WaitForSeconds(chaosEventTime);
        chaosEventUI.GetComponent<Image>().sprite = null;
        chaosEventUI.SetActive(false);
    }
}

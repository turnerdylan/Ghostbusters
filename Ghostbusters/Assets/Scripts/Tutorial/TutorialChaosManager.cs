using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialChaosManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static TutorialChaosManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static TutorialChaosManager instance = null;

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
            case 4:
                StartCoroutine(SmokeBomb(pos));
                break;
        }
    }

    private IEnumerator SmokeBomb(Vector3 smokePosition)
    {
        chaosEventUI.SetActive(true);
        chaosEventUI.GetComponent<Image>().sprite = chaosSprites[0];
        chaosEventUI.GetComponent<Animator>().SetTrigger("ChaosEvent");
        Instantiate(smokeBomb, smokePosition, Quaternion.identity);
        yield return new WaitForSeconds(chaosEventTime);
        chaosEventUI.GetComponent<Image>().sprite = null;
        chaosEventUI.SetActive(false);
    }
}

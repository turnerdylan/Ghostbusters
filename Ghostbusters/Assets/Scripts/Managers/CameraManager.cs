﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum CAMERA_POSITION
{ 
    NORMAL,
    MAP,
    PLAYERS,
    SETTINGS
};


public class CameraManager : MonoBehaviour
{
    SpriteRenderer fadeOutSprite;
    Color startColor = new Color(0, 0, 0, 255);
    Color targetColor = new Color(0, 0, 0, 0);
    public float timeToLerp = 5;
    public CAMERA_POSITION cameraState;

    public Color selectedColor;

    public List<Transform> cameraPositions = new List<Transform>();
    public List<TextMeshPro> textItems = new List<TextMeshPro>();

    public int textItemIndex = 0; // 0 is players, 1 is map, 2 is exit, 3 is credits, 4 is settings

    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        cameraState = CAMERA_POSITION.NORMAL;
        fadeOutSprite = Camera.main.GetComponentInChildren<SpriteRenderer>();
        //StartCoroutine(ColorLerp(targetColor, timeToLerp));
        textItems[0].color = selectedColor;
    }

    private void Update()
    {

        switch (cameraState)
        {
            case CAMERA_POSITION.NORMAL:

                //sets text green when you press button
                if (Gamepad.all[0].dpad.right.wasPressedThisFrame)
                {
                    textItemIndex++;
                    if (textItemIndex == textItems.Count)
                    {
                        textItemIndex = 0;
                    }
                    foreach (TextMeshPro text in textItems)
                    {
                        text.color = Color.white;
                    }
                    AudioManager.Instance.Play("Click");
                    textItems[textItemIndex].color = selectedColor;
                }
                else if (Gamepad.all[0].dpad.left.wasPressedThisFrame)
                {
                    textItemIndex--;
                    if (textItemIndex < 0)
                    {
                        textItemIndex = textItems.Count - 1;
                    }
                    foreach (TextMeshPro text in textItems)
                    {
                        text.color = Color.white;
                    }
                    AudioManager.Instance.Play("Click");
                    textItems[textItemIndex].color = selectedColor;
                }

                if (Gamepad.all[0].buttonSouth.wasPressedThisFrame)
                {
                    
                    if (textItemIndex == 0)
                    {
                        StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[0].position, CAMERA_POSITION.PLAYERS));
                    }
                    else if (textItemIndex == 1)
                    {
                        DataSelectManager.Instance.UpdatePlayerData();
                        StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[1].position, CAMERA_POSITION.MAP));
                    }
                    else if (textItemIndex == 2)
                    {
                        print("quit game");
                        Application.Quit();
                    }
                    else if (textItemIndex == 3)
                    {
                        credits.gameObject.SetActive(true);
                        print("access credits");
                    }
                    else if (textItemIndex == 4)
                    {
                        StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[2].position, CAMERA_POSITION.SETTINGS));
                        Camera.main.transform.rotation = cameraPositions[2].rotation;
                    }
                } else if(Gamepad.all[0].buttonEast.wasPressedThisFrame)
                {
                    credits.gameObject.SetActive(false);
                }
                break;
            case CAMERA_POSITION.MAP:
                if (Gamepad.all[0].buttonEast.wasPressedThisFrame)
                {
                    StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[3].position, CAMERA_POSITION.NORMAL));
                }
                    break;
            case CAMERA_POSITION.PLAYERS:
                if (Gamepad.all[0].buttonEast.wasPressedThisFrame)
                {
                    StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[3].position, CAMERA_POSITION.NORMAL));
                    DataSelectManager.Instance.UpdatePlayerData();
                }
                break;
            case CAMERA_POSITION.SETTINGS:
                if (Gamepad.all[0].buttonEast.wasPressedThisFrame)
                {
                    StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[3].position, CAMERA_POSITION.NORMAL));
                    Camera.main.transform.rotation = cameraPositions[3].rotation;
                }
                break;
            default:
                // code block
                break;
        }
    }


    /*IEnumerator ColorLerp(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = startColor;

        while (time < duration)
        {
            fadeOutSprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        fadeOutSprite.color = endValue;
    }*/

    IEnumerator LerpCameraPos(Vector3 startValue, Vector3 endValue, CAMERA_POSITION pos)
    {
        float time = 0;

        while (time < timeToLerp)
        {
            Camera.main.transform.position = Vector3.Lerp(startValue, endValue, time / timeToLerp);
            time += Time.deltaTime;
            yield return null;
        }
        cameraState = pos;
        Camera.main.transform.position = endValue;
    }
}

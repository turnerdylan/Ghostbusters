using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


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

    public int textItemIndex = 0; // 0 is players, 1 is map, 2 is settings

    // Start is called before the first frame update
    void Start()
    {
        cameraState = CAMERA_POSITION.NORMAL;
        fadeOutSprite = Camera.main.GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(ColorLerp(targetColor, timeToLerp));
        textItems[0].color = selectedColor;
    }

    private void Update()
    {
        if(cameraState == CAMERA_POSITION.NORMAL)
        {
            if (Gamepad.current.rightShoulder.wasPressedThisFrame)
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
                textItems[textItemIndex].color = selectedColor;
            }
            else if (Gamepad.current.leftShoulder.wasPressedThisFrame)
            {
                textItemIndex--;
                if (textItemIndex < 0)
                {
                    textItemIndex = textItems.Count;
                }
                foreach (TextMeshPro text in textItems)
                {
                    text.color = Color.white;
                }
                textItems[textItemIndex].color = selectedColor;
            }
        }

        switch (cameraState)
        {
            case CAMERA_POSITION.NORMAL:
                if (Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    if (textItemIndex == 0)
                    {
                        StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[0].position));
                        cameraState = CAMERA_POSITION.PLAYERS;
                    }
                    else if (textItemIndex == 1)
                    {
                        StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[1].position));
                        cameraState = CAMERA_POSITION.MAP;
                    }
                    else if (textItemIndex == 2)
                    {
                        StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[2].position));
                        Camera.main.transform.rotation = cameraPositions[2].rotation;
                        cameraState = CAMERA_POSITION.SETTINGS;
                    }
                }
                else if(Gamepad.current.buttonEast.wasPressedThisFrame)
                {

                }
                break;
            case CAMERA_POSITION.MAP:
                if (Gamepad.current.buttonEast.wasPressedThisFrame)
                {
                    StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[3].position));
                    cameraState = CAMERA_POSITION.NORMAL;
                }
                    break;
            case CAMERA_POSITION.PLAYERS:
                if (Gamepad.current.buttonEast.wasPressedThisFrame)
                {
                    StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[3].position));
                    cameraState = CAMERA_POSITION.NORMAL;
                }
                break;
            case CAMERA_POSITION.SETTINGS:
                if (Gamepad.current.buttonEast.wasPressedThisFrame)
                {
                    StartCoroutine(LerpCameraPos(Camera.main.transform.position, cameraPositions[3].position));
                    Camera.main.transform.rotation = cameraPositions[3].rotation;
                    cameraState = CAMERA_POSITION.NORMAL;
                }
                break;
            default:
                // code block
                break;
        }
    }


    IEnumerator ColorLerp(Color endValue, float duration)
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
    }

    IEnumerator LerpCameraPos(Vector3 startValue, Vector3 endValue)
    {
        float time = 0;

        while (time < timeToLerp)
        {
            Camera.main.transform.position = Vector3.Lerp(startValue, endValue, time / timeToLerp);
            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = endValue;
    }
}

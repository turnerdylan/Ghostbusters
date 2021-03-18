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

    public List<Transform> cameraPositions = new List<Transform>();
    public List<TextMeshPro> textItems = new List<TextMeshPro>();

    public int textItemIndex = 0; // 0 is players, 1 is map, 2 is settings

    // Start is called before the first frame update
    void Start()
    {
        cameraState = CAMERA_POSITION.NORMAL;
        fadeOutSprite = Camera.main.GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(LerpFunction(targetColor, timeToLerp));
        textItems[0].color = Color.green;
    }

    private void Update()
    {
        if(Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            textItemIndex++;
            if(textItemIndex == textItems.Count)
            {
                textItemIndex = 0;
            }
            foreach(TextMeshPro text in textItems)
            {
                text.color = Color.white;
            }
            textItems[textItemIndex].color = Color.green;
        }



        switch (cameraState)
        {
            case CAMERA_POSITION.NORMAL:
                if (Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    if (textItemIndex == 0)
                    {
                        Camera.main.transform.position = cameraPositions[0].position;
                        cameraState = CAMERA_POSITION.PLAYERS;
                    }
                    else if (textItemIndex == 1)
                    {
                        Camera.main.transform.position = cameraPositions[1].position;
                        cameraState = CAMERA_POSITION.MAP;
                    }
                    else if (textItemIndex == 2)
                    {
                        Camera.main.transform.position = cameraPositions[2].position;
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
                    Camera.main.transform.position = cameraPositions[3].position;
                    cameraState = CAMERA_POSITION.NORMAL;
                }
                    break;
            case CAMERA_POSITION.PLAYERS:
                if (Gamepad.current.buttonEast.wasPressedThisFrame)
                {
                    Camera.main.transform.position = cameraPositions[3].position;
                    cameraState = CAMERA_POSITION.NORMAL;
                }
                break;
            case CAMERA_POSITION.SETTINGS:
                if (Gamepad.current.buttonEast.wasPressedThisFrame)
                {
                    Camera.main.transform.position = cameraPositions[3].position;
                    cameraState = CAMERA_POSITION.NORMAL;
                }
                break;
            default:
                // code block
                break;
        }

        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if(textItemIndex == 0)
            {
                Camera.main.transform.position = cameraPositions[0].position;
            }
            else if (textItemIndex == 1)
            {
                //go to player select
            }
            else if (textItemIndex == 2)
            {
                //go to player select
            }
        }
    }


    IEnumerator LerpFunction(Color endValue, float duration)
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
}

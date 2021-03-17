using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    CAMERA_POSITION cameraState;


    // Start is called before the first frame update
    void Start()
    {
        cameraState = CAMERA_POSITION.NORMAL;
        fadeOutSprite = Camera.main.GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(LerpFunction(targetColor, timeToLerp));
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

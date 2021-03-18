using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputChecker : MonoBehaviour
{
    Transform cameraStart;
    public Transform cameraEnd;
    public float timeToLerp = 4;
    SpriteRenderer fadeOutSprite;
    Color targetColor = new Color(0, 0, 0, 255);

    // Start is called before the first frame update
    void Start()
    {
        cameraStart = Camera.main.transform;
        fadeOutSprite = Camera.main.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current.buttonSouth.isPressed)
        {
            StartCoroutine(LerpFunction(targetColor));

        }
        if (Vector3.Distance(Camera.main.transform.position, cameraEnd.position) < .5)
        {
            print("1");
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator LerpFunction(Color endValue)
    {
        float time = 0;
        Color startValue = fadeOutSprite.color;

        while (time < timeToLerp)
        {
            fadeOutSprite.color = Color.Lerp(startValue, endValue, time / timeToLerp);
            Camera.main.transform.position = Vector3.Lerp(cameraStart.position, cameraEnd.position, time / timeToLerp);

            time += Time.deltaTime;
            yield return null;
        }
        fadeOutSprite.color = endValue;
    }
}

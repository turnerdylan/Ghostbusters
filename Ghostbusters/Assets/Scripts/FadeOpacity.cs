using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOpacity : MonoBehaviour
{
    private float nextTimeCall;
    private Material _myMaterial;
    public Coroutine coroutine;
    void Start()
    {
        _myMaterial = GetComponentInChildren<Renderer>().material;
        nextTimeCall = Time.time + 5f;
    }
    void Update()
    {
        if(Time.time >= nextTimeCall)
        {
            if(_myMaterial.color.a == 1f)
                coroutine = StartCoroutine(FadeTo(_myMaterial, 0f, 3f));
            else
                coroutine = StartCoroutine(FadeTo(_myMaterial, 1f, 3f));

            nextTimeCall += 5f;
        }
    }

    IEnumerator FadeTo(Material material, float targetOpacity, float duration)
    {

        // Cache the current color of the material, and its initiql opacity.
        Color color = material.color;
        float startOpacity = color.a;

        // Track how many seconds we've been fading.
        float t = 0;

        while(t < duration) 
        {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            material.color = color;

            // Wait one frame, and repeat.
            yield return null;
        }
    }
}

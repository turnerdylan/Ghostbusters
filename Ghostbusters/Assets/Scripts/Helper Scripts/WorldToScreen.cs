using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreen : MonoBehaviour
{
    public GameObject icon;
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        icon.transform.position = new Vector3(screenPos.x, Mathf.Clamp(screenPos.y, 0, Screen.height-115), screenPos.z);
    }
}

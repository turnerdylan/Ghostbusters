using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    GameObject child;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    public GameObject GetChild()
    {
        return child;
    }
}

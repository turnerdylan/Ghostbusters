using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public GameObject child;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
    }
}

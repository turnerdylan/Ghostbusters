using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfGrounded : MonoBehaviour
{
    Collider grounded;
    bool isGrounded;

    private void Awake()
    {
        grounded = GetComponent <Collider>();
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}

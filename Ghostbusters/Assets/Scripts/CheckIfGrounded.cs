using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfGrounded : MonoBehaviour
{
    CapsuleCollider grounded;
    bool isGrounded;

    private void Awake()
    {
        grounded = GetComponent <CapsuleCollider>();
    }

    public bool GetIsGrounded()
    {
        print(isGrounded);
        return isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        print("ready");
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestGhostAttack : MonoBehaviour
{
    public GameObject ghostBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SprayAttack();
        }
    }

    private void SprayAttack()
    {
        transform.Rotate(new Vector3(0, -45, 0));
        Instantiate(ghostBall, transform.position, Quaternion.identity);

        transform.Rotate(new Vector3(0, 0, 0));
        Instantiate(ghostBall, transform.position, Quaternion.identity);

        transform.Rotate(new Vector3(0, 45, 0));
        Instantiate(ghostBall, transform.position, Quaternion.identity);

    }
}

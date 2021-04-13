using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimationController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);
    }


}

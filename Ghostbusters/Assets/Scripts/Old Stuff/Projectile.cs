using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += 5 * Vector3.back * Time.deltaTime;
    }
}

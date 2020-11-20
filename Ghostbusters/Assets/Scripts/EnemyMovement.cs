using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform pointA;
    public Transform pointB;

     void Update()
     {
         transform.position = Vector3.Lerp(pointA.position, pointB.position, Mathf.PingPong(Time.time*speed, 1));
     }
}

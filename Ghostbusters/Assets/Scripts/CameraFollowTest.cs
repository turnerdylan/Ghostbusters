using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTest : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        if(target != null)
        {
            Vector3 offset = target.transform.position - transform.position;
            transform.position = (target.transform.position + offset);
        }
    }
}

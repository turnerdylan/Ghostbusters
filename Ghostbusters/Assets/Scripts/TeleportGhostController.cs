using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportGhostController : MonoBehaviour
{
    [SerializeField]
    float teleportRange = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 9)
        {
            Vector3 randomDirection = Vector3.zero;
            randomDirection = Random.insideUnitSphere * teleportRange;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, teleportRange, 1);
            Vector3 finalPos = hit.position;

            transform.position = finalPos;
        }
    }
}

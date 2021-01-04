using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{
    public List<Node> AdjacentNodes;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (Node node in AdjacentNodes)
        {
            //Debug.DrawLine(this.transform.position, node.transform.position, Color.blue, 1f);
            Gizmos.DrawRay(this.transform.position, (node.transform.position - this.transform.position) / 2.5f);
            Gizmos.DrawWireSphere(transform.position, .2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    private GameObject[] fencePosts;
    private bool isFence1 = false;
    public bool fenceConnected = false;
    private RaycastHit hitInfo;
    public float waitTime = 10f;

    public LineRenderer[] lr;
    // Start is called before the first frame update
    void Awake()
    {
        fencePosts = GameObject.FindGameObjectsWithTag("Fence"); //searches the scene for all fence objects
        if (fencePosts.Length == 1) //if this is the first fence post to be placed
        {
            isFence1 = true;
        }
        else if (fencePosts.Length == 2)
        {
            isFence1 = false;
            ConnectFence();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(fenceConnected && !(isFence1))
        {
            DrawFence();
            if (Physics.Linecast(fencePosts[0].transform.position, fencePosts[1].transform.position, out hitInfo))
            {
                if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ghost"))
                {
                    //hitInfo.collider.gameObject.Stun();
                    //Debug.Log("Ghost entered linecast");
                    hitInfo.collider.gameObject.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
                }
            }
        }
        
    }

    public void ConnectFence()
    {
        fenceConnected = true;
        fencePosts[0].GetComponent<FenceController>().fenceConnected = true;
        Debug.Log("Fence Connected");
        StartCoroutine(Wait());
        print("Coroutine started");
    }

    void DrawFence()
    {
        lr[0].SetPosition(0, fencePosts[0].transform.position + new Vector3(0, 1, 0));
        lr[0].SetPosition(1, fencePosts[1].transform.position + new Vector3(0, 1, 0));
        lr[1].SetPosition(0, fencePosts[0].transform.position);
        lr[1].SetPosition(1, fencePosts[1].transform.position);
        lr[2].SetPosition(0, fencePosts[0].transform.position + new Vector3(0, -1, 0));
        lr[2].SetPosition(1, fencePosts[1].transform.position + new Vector3(0, -1, 0));
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        //fenceConnected = false;
        //fencePosts[0].GetComponent<FenceController>().fenceConnected = false;
        print("Coroutine ended: " + Time.time + " seconds");
        Destroy(fencePosts[0]);
        Destroy(gameObject);
    }
}

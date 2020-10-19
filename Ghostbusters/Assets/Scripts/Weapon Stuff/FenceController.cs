using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FenceController : Weapon
{
    private GameObject[] fencePosts;
    public bool isFence1 = false;
    public bool fenceConnected = false;
    private RaycastHit hitInfo;
    private Transform fenceParent;
    public float waitTime = 10f;

    public LineRenderer[] lr;
    // Start is called before the first frame update
    void Awake()
    {
        fenceParent = transform.parent;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(fenceConnected)
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

    public override void Use()
    {
        base.Use();
        gameObject.tag = "Fence";
        transform.parent = null;
        fencePosts = GameObject.FindGameObjectsWithTag("Fence"); //checks to see if another fence post has already been placed
        if(fencePosts.Length == 2) //if both fence posts have been placed
            ConnectFence();
    }


    public void ConnectFence()
    {
        if(fencePosts.Length == 2)
        {
            fenceConnected = true;
            Debug.Log("Fence Connected");
            StartCoroutine(Wait());
            print("Coroutine started");
        }
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
        transform.parent = fenceParent;
        fenceConnected = false;
        print("Coroutine ended: " + Time.time + " seconds");
    }
}

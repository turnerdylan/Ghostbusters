using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceSpawner : Weapon
{
    public GameObject fencePrefab;
    Player player;
    GameObject fencePost;
    bool canUse = true;
    Renderer rend;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rend = GetComponentInChildren<Renderer>();
    }
    public override void Use()
    {
        if(canUse)
        {
            base.Use();
            canUse = false;
            fencePost = Instantiate(fencePrefab, transform.position, Quaternion.identity);
            rend.enabled = false;
        }
    }

    private void Update()
    {
        if (fencePost == null)
            Reset();
    }

    public void Reset()
    {
        canUse = true;
        rend.enabled = true;
    }
}

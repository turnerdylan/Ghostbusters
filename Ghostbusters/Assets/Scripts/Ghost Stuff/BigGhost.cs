using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigGhost : MonoBehaviour
{
    public GameObject mediumGhost;

    public float ghostSpawnOffset = 0.5f;

    private void Start()
    {
        GhostManager.Instance.bigGhosts.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.sKey.wasPressedThisFrame)
        {
            SplitApart();
        }
    }

    public void SplitApart()
    {
        Instantiate(mediumGhost, this.transform.position + new Vector3(ghostSpawnOffset,0,ghostSpawnOffset), Quaternion.identity);
        Instantiate(mediumGhost, this.transform.position + new Vector3(-ghostSpawnOffset,0,-ghostSpawnOffset), Quaternion.identity);
        GhostManager.Instance.bigGhosts.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}

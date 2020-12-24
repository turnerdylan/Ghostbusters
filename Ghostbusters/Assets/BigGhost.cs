using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigGhost : MonoBehaviour
{
    public GameObject mediumGhost;

    public float ghostSpawnOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.wKey.wasPressedThisFrame)
        {
            SplitApart();
        }
    }

    public void SplitApart()
    {
        Instantiate(mediumGhost, this.transform.position + new Vector3(ghostSpawnOffset,0,ghostSpawnOffset), Quaternion.identity);
        Instantiate(mediumGhost, this.transform.position + new Vector3(-ghostSpawnOffset,0,-ghostSpawnOffset), Quaternion.identity);
        Destroy(this.gameObject);
    }
}

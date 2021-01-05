using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MediumGhost : MonoBehaviour
{
    public GameObject smallGhost;

    public float ghostSpawnOffset = 0.5f;

    private void Start()
    {
        GhostManager.Instance.mediumGhosts.Add(gameObject);
    }

    private void OnEnable()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SplitApart();
        }
    }

    public void SplitApart()
    {
        //active and deactivate method
/*        smallghost1.SetActive(true);
        smallghost1.transform.position = this.transform.position + new Vector3(ghostSpawnOffset, 0, ghostSpawnOffset);
        smallghost2.SetActive(true);
        smallghost2.transform.position = this.transform.position + new Vector3(ghostSpawnOffset, 0, ghostSpawnOffset);*/
        /*this.gameObject.SetActive(false);
        GhostManager.Instance.mediumGhosts.Remove(this.gameObject);*/


        //delete and instatiate method
        Instantiate(smallGhost, this.transform.position + new Vector3(ghostSpawnOffset, 0, ghostSpawnOffset), Quaternion.identity);
        Instantiate(smallGhost, this.transform.position + new Vector3(-ghostSpawnOffset, 0, -ghostSpawnOffset), Quaternion.identity);
        GhostManager.Instance.mediumGhosts.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}

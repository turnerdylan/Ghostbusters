using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTest : MonoBehaviour
{
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
        if(other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().enabled = false;
            StartCoroutine(StunPlayer(2, other.gameObject));
        }
    }

    IEnumerator StunPlayer(float stunTime, GameObject player)
    {
        yield return new WaitForSeconds(stunTime);
        player.GetComponent<Player>().enabled = false;
    }
}

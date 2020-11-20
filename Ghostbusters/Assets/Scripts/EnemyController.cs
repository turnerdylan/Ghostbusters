using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    int playerCount = 0;
    GameObject[] players;
    public float delay = 1.0f;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Update()
    {

        if(playerCount == 2)
        {
            StartCoroutine(Capture());
        }
        //if(players == 1)
            //Attack();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerCount++;
            other.gameObject.GetComponent<CheckDistance>().ghostNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            playerCount--;
            other.gameObject.GetComponent<CheckDistance>().ghostNear = false;
    }

    void Die()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        Destroy(gameObject, delay);
    }

    IEnumerator Capture()
    {
        Die();
        yield return new WaitForSeconds(delay - 0.1f);
        foreach(GameObject player in players)
        {
            player.GetComponent<CheckDistance>().ghostNear = false;
        }
    }
}

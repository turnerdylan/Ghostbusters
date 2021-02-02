using UnityEngine;
using System.Collections;

public class CheckDistance : MonoBehaviour
{
    public GameObject p2;
    public GameObject explosivePrefab;
    public bool ghostNear = false;


    void Update()
    {
        float dist = Vector3.Distance(p2.transform.position, transform.position);
        if(dist <= 2.5)
        {
            Debug.Log("close");
            if(!ghostNear || !p2.GetComponent<CheckDistance>().ghostNear)
            {
                Instantiate(explosivePrefab, (p2.transform.position+transform.position)/2, Quaternion.identity);               
            }
        }
    }
}

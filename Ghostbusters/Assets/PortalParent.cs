using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalParent : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> portals = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<PortalController>())
            {
                portals.Add(child.gameObject);
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammoType;
    public Material current;
    //red is 1
    //blue is 2
    //yellow is 3
    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        current = mesh.material;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacher : MonoBehaviour
{
    Battery battery;

    // Start is called before the first frame update
    void Start()
    {
        battery = GetComponentInParent<Battery>();
    }

    private void OnTriggerStay(Collider other)
    {
        print("collided");
        if (other.GetComponent<Barrel>())
        {
            battery.barrel = other.GetComponent<Barrel>();
            battery.barrelAttached = true;

            other.transform.position = transform.position;
            other.attachedRigidbody.isKinematic = true;
        }
        else if(other.GetComponent<Ammo>())
        {
            battery.ammo = other.GetComponent<Ammo>();
            battery.ammoAttached = true;

            other.transform.position = transform.position;
            other.attachedRigidbody.isKinematic = true;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Barrel>())
        {
            battery.barrel = null;
            battery.barrelAttached = false;


        }
        else if (other.GetComponent<Ammo>())
        {
            battery.ammo = null;
            battery.ammoAttached = false;
        }
    }
}

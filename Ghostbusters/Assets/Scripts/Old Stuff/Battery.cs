using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Battery : MonoBehaviour
{
    public Transform barrelAttachment;
    public Transform ammoAttachment;

    public Barrel barrel;
    public Ammo ammo;

    public bool barrelAttached = false;
    public bool ammoAttached = false;

    public GameObject projectile;


    void Update()
    {
        if (projectile) projectile.GetComponent<MeshRenderer>().material = ammo.current;

        if (barrel.barrelType == 1 && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            print("case1");
            barrel.ShootProjectileShotgun(projectile);
                
            //Instantiate()
        }
        else if (barrel.barrelType == 2 && Keyboard.current.anyKey.isPressed)
        {
            print("case2");
            StartCoroutine(delay());
            barrel.ShootProjectile(projectile);
        }
        else if (barrel.barrelType == 3 && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            print("case3");
            barrel.ShootProjectile(projectile);
        }
        else if (barrel.barrelType == 4 && Keyboard.current.anyKey.isPressed)
        {
            print("case4");
            barrel.ShootRay(projectile);
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
    }

    public void AssignProjColor(Material mat)
    {
        projectile.GetComponent<MeshRenderer>().material = mat;
    }
}

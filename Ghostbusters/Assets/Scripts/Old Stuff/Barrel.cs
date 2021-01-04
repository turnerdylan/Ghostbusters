using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public int barrelType = 0;
    public Transform shotPoint;
    //shotgun is 1
    //auto is 2
    //single is 3

    public void ShootProjectile(GameObject proj)
    {
        Instantiate(proj, shotPoint.position, Quaternion.identity);
    }

    public void ShootProjectileShotgun(GameObject proj)
    {
        Transform temp = shotPoint;

        //temp.transform.Rotate();
        Instantiate(proj, temp.position, Quaternion.Euler(new Vector3(0, -30, 0)));

        temp.transform.Rotate(0, -90, 0);
        Instantiate(proj, temp.position, Quaternion.identity);

        temp.transform.Rotate(0, 0, 0);
        Instantiate(proj, temp.position, temp.rotation);

        temp.transform.Rotate(0, 15, 0);
        Instantiate(proj, temp.position, temp.rotation);

        temp.transform.Rotate(0, 30, 0);
        Instantiate(proj, temp.position, temp.rotation);

    }


    public void ShootRay(GameObject proj)
    {
        RaycastHit hit;
        Physics.Raycast(shotPoint.position, Vector3.up, out hit, Mathf.Infinity);
        Debug.DrawRay(shotPoint.position, transform.up * 100, Color.red);
    }



}

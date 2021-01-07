using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class Explosive : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;

    void Awake()
    {
        Explode();
    }

    void Explode()
    {
        Debug.Log("boom");
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius, 1 << 9);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 1.5f);
        }
        Destroy(gameObject, 0.5f);
    }
}
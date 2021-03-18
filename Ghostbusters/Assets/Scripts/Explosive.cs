using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class Explosive : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public LayerMask layer;

    void Awake()
    {
        Explode();
    }

    void Explode()
    {
        Debug.Log("boom");
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius, layer);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                //rb.gameObject.GetComponent<Player>().TriggerStun();
                rb.GetComponent<Player>().TriggerDisableMovement(0.2f);
                rb.AddExplosionForce(power, explosionPos, 0, 0.0f);
            }
        }
        Destroy(gameObject, 0.5f);
    }
}
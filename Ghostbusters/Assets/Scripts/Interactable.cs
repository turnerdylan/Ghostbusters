using UnityEngine;

public class Interactable : MonoBehaviour
{
    float radius = 2f;

    public bool canInteract = false;
    Transform player;


    public virtual void Interact()
    {
        print("interacting with " + transform.name);
    }

    public virtual void Interact(PlayerController pc)
    {
        print("interacting with " + transform.name);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            canInteract = false;
        }
    }
}

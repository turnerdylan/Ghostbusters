using UnityEngine;

public class Interactable : MonoBehaviour
{
    float radius = 2f;

    public bool canInteract = false;
    Transform player;
    public PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    public virtual void Interact()
    {
        print("interacting with " + transform.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            canInteract = true;
        }
        pc = other.GetComponent<PlayerController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            canInteract = false;
        }
        pc = null;
    }
}

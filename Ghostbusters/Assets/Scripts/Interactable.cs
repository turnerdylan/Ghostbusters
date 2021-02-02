using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactionRadius = 3f;

    public bool isInteracting = false;
    Transform player;
    public Player currentPlayer;

    private void Update()
    {
        if(isInteracting)
        {

        }
    }

    public virtual void Interact()
    {
        print("interacting with " + transform.name);
    }

    public virtual void Interact(Player pc)
    {
        print("interacting with " + transform.name);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        //display button UI

        if (other.gameObject.layer == 9)
        {
            isInteracting = true;
        }
        currentPlayer = other.GetComponent<Player>();
    }

    public void OnInteractedWith(Player player)
    {
        isInteracting = true;
        currentPlayer = player;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerSelect : MonoBehaviour
{
    public List<Material> playerImages = new List<Material>();
    int imageIndex = 0;

    public int playerIndex;

    private Vector3 moveDirection;
    private Vector3 inputVector;
    private float cursorMoveSpeed;
    private MeshRenderer mesh;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        UpdateTextures();
    }

    // Update is called once per frame
    void Update()
    {
        SetMoveDirection();
    }

    public void OnMove(CallbackContext context)
    {
        
    }

    public void OnRight(CallbackContext context)
    {
        if (context.performed)
        {
            print("right");
            imageIndex++;
            if(imageIndex >= playerImages.Count)
            {
                imageIndex = 0;
            }
            UpdateTextures();
        }
    }

    public void OnLeft(CallbackContext context)
    {
        if (context.performed)
        {
            print("left");
            imageIndex--;
            if (imageIndex <= -1)
            {
                imageIndex = playerImages.Count - 1;
            }
            UpdateTextures();
        }
    }

    public void OnSelect(CallbackContext context)
    {
        if (context.performed)
        {
            
        }
    }

    public void SetMoveVector(Vector2 direction)
    {
        inputVector = direction;
    }

    private void SetMoveDirection()
    {
        moveDirection = inputVector * cursorMoveSpeed;
        
    }

    public void UpdateTextures()
    {
        mesh.material = playerImages[imageIndex];
    }
}

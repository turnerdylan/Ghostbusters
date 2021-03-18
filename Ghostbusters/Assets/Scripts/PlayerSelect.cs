using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerSelect : MonoBehaviour
{
    public List<Material> playerImages = new List<Material>();
    CameraManager cameraManager;
    int imageIndex = 0;

    public int playerIndex;

    private Vector3 moveDirection;
    private Vector3 inputVector;
    private float cursorMoveSpeed;
    private MeshRenderer mesh;


    // Start is called before the first frame update
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        mesh = GetComponent<MeshRenderer>();
        UpdateTextures();
    }

    public void OnRight(CallbackContext context)
    {
        if (context.performed && cameraManager.cameraState == CAMERA_POSITION.PLAYERS)
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
        if (context.performed && cameraManager.cameraState == CAMERA_POSITION.PLAYERS)
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

    public void UpdateTextures()
    {
        mesh.material = playerImages[imageIndex];
    }
}

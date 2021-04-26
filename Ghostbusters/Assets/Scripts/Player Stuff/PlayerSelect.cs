using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerSelect : MonoBehaviour
{
    public List<Material> playerImages = new List<Material>();
    CameraManager cameraManager;
    public int imageIndex = 0;

    private Vector3 moveDirection;
    private Vector3 inputVector;
    private float cursorMoveSpeed;
    private MeshRenderer mesh;
    private bool selected = false;
    private bool canClick = true;

    private Color common;


    // Start is called before the first frame update
    void Start()
    {
        common = GetComponent<MeshRenderer>().material.color;
        cameraManager = FindObjectOfType<CameraManager>();
        mesh = GetComponent<MeshRenderer>();
        UpdateTextures();
    }

    public void OnRight(CallbackContext context)
    {
        if (context.performed && cameraManager.cameraState == CAMERA_POSITION.PLAYERS && !selected && canClick)
        {
            AudioManager.Instance.Play("Click");
            imageIndex++;
            if(imageIndex >= playerImages.Count)
            {
                imageIndex = 0;
            }
            //Trigger();

            UpdateTextures();
        }
    }

    private void Trigger()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        canClick = false;
        print("hey");
        yield return new WaitForSeconds(0.2f);
        print("yo");
        canClick = true;
    }

    public void OnLeft(CallbackContext context)
    {
        if (context.performed && cameraManager.cameraState == CAMERA_POSITION.PLAYERS && !selected && canClick)
        {
            AudioManager.Instance.Play("Click");
            imageIndex--;
            if (imageIndex <= -1)
            {
                imageIndex = playerImages.Count - 1;
            }
            //Trigger();

            UpdateTextures();
        }
    }

    //this doesnt work yet
    /*public void OnSelect(CallbackContext context)
    {
        if (context.performed && cameraManager.cameraState == CAMERA_POSITION.PLAYERS && !selected)
        {
            print("yes");
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            GetComponent<MeshRenderer>().material.color = Color.green;

            selected = true;
        }
    }

    public void OnBack(CallbackContext context)
    {
        if(context.performed && cameraManager.cameraState == CAMERA_POSITION.PLAYERS && selected)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<MeshRenderer>().material.color = common;
            selected = false;
        }
    }*/



    public void UpdateTextures()
    {
        mesh.material = playerImages[imageIndex];
    }
}

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
    private bool canClick = true;
    public bool selected = false;

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

            int tempIndex = imageIndex;

            while(DataSelectManager.Instance.playersSelected[tempIndex] == true)
            {
                tempIndex++;
                if(tempIndex >= 7)
                {
                    tempIndex = 0;
                }
            }
            DataSelectManager.Instance.playersSelected[imageIndex] = false;
            DataSelectManager.Instance.playersSelected[tempIndex] = true;

            imageIndex = tempIndex;

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
            int tempIndex = imageIndex;

            while (DataSelectManager.Instance.playersSelected[tempIndex] == true)
            {
                tempIndex--;
                if (tempIndex <= -1)
                {
                    tempIndex = 6;
                }
            }
            DataSelectManager.Instance.playersSelected[imageIndex] = false;
            DataSelectManager.Instance.playersSelected[tempIndex] = true;

            imageIndex = tempIndex;

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

            if(DataSelectManager.Instance.CheckIfAllPlayersAreReady())
            {
                cameraManager.ExitPlayerSelect();
            }
        }
    }

    public void OnBack(CallbackContext context)
    {
        if (context.performed && cameraManager.cameraState == CAMERA_POSITION.PLAYERS && selected)
        {
            Unready();
        }
    }*/

    public void Unready()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<MeshRenderer>().material.color = common;
        selected = false;
    }



    public void UpdateTextures()
    {
        mesh.material = playerImages[imageIndex];
    }
}

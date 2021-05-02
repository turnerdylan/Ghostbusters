using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static NavigationManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static NavigationManager instance = null;

    private void Awake()
    {

        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
    #endregion



    public NavigationNode currentSelection;
    private CameraManager cameraManager;
    private Color32 orange = new Color32(255, 177, 16, 255);

    private void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        currentSelection = cameraManager.textItems[0].GetComponent<NavigationNode>();
        //currentSelection.GetComponent<Pin>().GetChild().SetActive(true);
    }

    private void Update()
    {
        if(cameraManager.cameraState == CAMERA_POSITION.MAP)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.right.wasPressedThisFrame || Gamepad.all[i].leftStick.right.wasPressedThisFrame)
                {
                    GoEast(0);
                }
                else if (Gamepad.all[i].dpad.up.wasPressedThisFrame || Gamepad.all[i].leftStick.up.wasPressedThisFrame)
                {
                    GoNorth(0);
                }
                else if (Gamepad.all[i].dpad.down.wasPressedThisFrame || Gamepad.all[i].leftStick.down.wasPressedThisFrame)
                {
                    GoSouth(0);
                }
                else if (Gamepad.all[i].dpad.left.wasPressedThisFrame || Gamepad.all[i].leftStick.left.wasPressedThisFrame)
                {
                    GoWest(0);
                }
            }
        }
        else if(cameraManager.cameraState == CAMERA_POSITION.NORMAL)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.right.wasPressedThisFrame || Gamepad.all[i].leftStick.right.wasPressedThisFrame)
                {
                    GoEast(1);
                    print("test");
                }
                else if (Gamepad.all[i].dpad.up.wasPressedThisFrame || Gamepad.all[i].leftStick.up.wasPressedThisFrame)
                {
                    GoNorth(1);
                }
                else if (Gamepad.all[i].dpad.down.wasPressedThisFrame || Gamepad.all[i].leftStick.down.wasPressedThisFrame)
                {
                    GoSouth(1);
                }
                else if (Gamepad.all[i].dpad.left.wasPressedThisFrame || Gamepad.all[i].leftStick.left.wasPressedThisFrame)
                {
                    GoWest(1);
                }
            }
        }

        
    }

    public void GoNorth(int indicator)
    {
        if (NavigationManager.Instance.currentSelection.northNav != null && NavigationManager.Instance.currentSelection.northNav.index < DataSelectManager.Instance.furthestUnlockedLevel)
        {
            currentSelection = currentSelection.northNav;
            AudioManager.Instance.Play("Click");

            if (indicator == 0)
            {
                DataSelectManager.Instance.SetAllPinTextInactive();
                currentSelection.GetComponent<Pin>().GetChild().SetActive(true);
            }
            else
            {
                cameraManager.MakeAllTextWhite();
                currentSelection.GetComponent<TextMeshPro>().color = orange;
            }
        }
    }

    public void GoSouth(int indicator)
    {
        if (NavigationManager.Instance.currentSelection.southNav != null && NavigationManager.Instance.currentSelection.southNav.index < DataSelectManager.Instance.furthestUnlockedLevel)
        {
            currentSelection = currentSelection.southNav;
            AudioManager.Instance.Play("Click");

            if (indicator == 0)
            {
                DataSelectManager.Instance.SetAllPinTextInactive();
                currentSelection.GetComponent<Pin>().GetChild().SetActive(true);
            }
            else
            {
                cameraManager.MakeAllTextWhite();
                currentSelection.GetComponent<TextMeshPro>().color = orange;
            }
        }
    }

    public void GoEast(int indicator)
    {
        if (NavigationManager.Instance.currentSelection.eastNav != null && NavigationManager.Instance.currentSelection.eastNav.index < DataSelectManager.Instance.furthestUnlockedLevel)
        {
            currentSelection = currentSelection.eastNav;
            AudioManager.Instance.Play("Click");

            if (indicator == 0)
            {
                DataSelectManager.Instance.SetAllPinTextInactive();
                currentSelection.GetComponent<Pin>().GetChild().SetActive(true);
            }
            else
            {
                cameraManager.MakeAllTextWhite();
                currentSelection.GetComponent<TextMeshPro>().color = orange;
            }
        }
    }

    public void GoWest(int indicator)
    {
        if (NavigationManager.Instance.currentSelection.westNav != null && NavigationManager.Instance.currentSelection.westNav.index < DataSelectManager.Instance.furthestUnlockedLevel)
        {
            currentSelection = currentSelection.westNav;
            AudioManager.Instance.Play("Click");

            if (indicator == 0)
            {
                DataSelectManager.Instance.SetAllPinTextInactive();
                currentSelection.GetComponent<Pin>().GetChild().SetActive(true);
            }
            else
            {
                cameraManager.MakeAllTextWhite();
                currentSelection.GetComponent<TextMeshPro>().color = orange;
            }
        }
    }
}

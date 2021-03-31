using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private Player player;

    private float scareInput;
    private float getBagInput;
    private float swingBagInput;
    private float dropBagInput;

    private void Awake()
    {
        player = GetComponent<Player>();
        //controls = new PlayerControls();
    }

    public void OnMove(CallbackContext context)
    {
        if (player != null)
            player.SetMoveVector(context.ReadValue<Vector2>());
    }

    public void DepositGhosts(CallbackContext context)
    {
        if(context.performed)
        {
            player.DepositGhosts();
        }
    }

    public void Dive(CallbackContext context)
    {
        if (context.performed)
        {
            player.Dive();
        }
    }

    public void OnPause(CallbackContext context)
    {
        if(context.performed && LevelManager.Instance.GetLevelState() == LEVEL_STATE.STARTED)
        {
            LevelManager.Instance.Pause();
        }
    }

    public void OnUpUI(CallbackContext context)
    {
        if (context.performed && SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (LevelManager.Instance.GetLevelState() == LEVEL_STATE.PAUSED)
            {
                LevelManager.Instance.ChangePauseUIIndex(-1);
            }
            else if(LevelManager.Instance.GetLevelState() == LEVEL_STATE.ENDED)
            {
                LevelManager.Instance.ChangeEndUIIndex(-1);
            }
        }
    }

    public void OnDownUI(CallbackContext context)
    {
        if (context.performed && SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (LevelManager.Instance.GetLevelState() == LEVEL_STATE.PAUSED)
            {
                LevelManager.Instance.ChangePauseUIIndex(1);
            }
            if (LevelManager.Instance.GetLevelState() == LEVEL_STATE.ENDED)
            {
                LevelManager.Instance.ChangeEndUIIndex(1);
            }
        }
    }

    public void SelectUI(CallbackContext context)
    {
        if (context.performed && SceneManager.GetActiveScene().buildIndex != 2)
        {
            if (LevelManager.Instance.GetLevelState() == LEVEL_STATE.PAUSED)
            {
                LevelManager.Instance.SelectPauseUI();
            } else if (LevelManager.Instance.GetLevelState() == LEVEL_STATE.ENDED)
            {
                LevelManager.Instance.SelectEndUI();
            }
        }
    }


    public void UpScare(CallbackContext context)
    {
        if(player != null && context.performed)
            player.Scare(BUTTON_PRESS.Up);
    }
    public void DownScare(CallbackContext context)
    {
        if(player != null && context.performed)
            player.Scare(BUTTON_PRESS.Down);
    }
    public void LeftScare(CallbackContext context)
    {
        if(player != null && context.performed)
            player.Scare(BUTTON_PRESS.Left);
    }
    public void RightScare(CallbackContext context)
    {
        if(player != null && context.performed)
            player.Scare(BUTTON_PRESS.Right);
    }
}

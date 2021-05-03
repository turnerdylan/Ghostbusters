using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static TutorialUIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static TutorialUIManager instance = null;

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

    public List<UIElement> UIElements = new List<UIElement>();
    public List<Sprite> playerImages = new List<Sprite>();
    // yellow, orange, bird, fish, red, robot, candy

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < UIElements.Count; i++)
        {
            UIElements[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            UIElements[i].gameObject.SetActive(true);

            //int playerSpriteIndex = DataSelectManager.Instance.players[i].imageIndex;
            UIElements[i].playerImage.sprite = TutorialPlayerManager.Instance.GetPlayerArray()[i].characterSprite;

        }
    }

    // Update is called once per frame
    public void UpdateHeldGhosts()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            UIElements[i].heldGhostsValue.text = TutorialPlayerManager.Instance.GetPlayerArray()[i].GetNumberOfHeldGhosts().ToString();
        }
    }
}

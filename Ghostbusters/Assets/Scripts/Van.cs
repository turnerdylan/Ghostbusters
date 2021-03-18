using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Van : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static Van Instance
    {
        get
        {
            return instance;
        }
    }

    private static Van instance = null;

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

    [SerializeField] private float _interactionRadius = 6f;
    [SerializeField] private TextMeshPro counterText;
    public int numberOfStoredGhosts = 0;

    SpriteRenderer buttonSprite;

    private void Start()
    {
        buttonSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public float GetInteractionRadius()
    {
        return _interactionRadius;
    }

    public void DepositGhosts(int storedGhosts)
    {
        print("deposit ghosts");
        counterText.text = numberOfStoredGhosts.ToString();
        numberOfStoredGhosts += storedGhosts;
        PlayerManager.Instance.CalculateScore();
    }
}

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
    [SerializeField] private int numberOfStoredGhosts = 0;

    SpriteRenderer buttonSprite;

    private void Start()
    {
        buttonSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        counterText.text = numberOfStoredGhosts.ToString();

        foreach (Player player in PlayerManager.Instance.players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < _interactionRadius && !GetComponentInChildren<Bag>())
            {
                buttonSprite.enabled = true;
            }
            else
            {
                buttonSprite.enabled = false;
            }
        }
    }

    public float GetInteractionRadius()
    {
        return _interactionRadius;
    }

    public void DepositGhosts(int storedGhosts)
    {
        numberOfStoredGhosts += storedGhosts;
    }
}

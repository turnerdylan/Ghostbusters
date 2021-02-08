using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] Transform bagStoredPosition = null;

    SpriteRenderer buttonSprite;

    private void Start()
    {
        buttonSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        foreach (Player player in PlayerManager.Instance.players)
        {
            if (Vector3.Distance(bagStoredPosition.position, player.transform.position) < _interactionRadius && !GetComponentInChildren<Bag>())
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

    public Vector3 GetBagStoredPosition()
    {
        return bagStoredPosition.position;
    }
}

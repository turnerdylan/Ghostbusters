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
    Animator anim;
    public int numberOfStoredGhosts = 0;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    public float GetInteractionRadius()
    {
        return _interactionRadius;
    }

    public void DepositGhosts(int storedGhosts)
    {
        anim.SetTrigger("Deposit");
        numberOfStoredGhosts += storedGhosts;
        PlayerManager.Instance.CalculateScore();
    }
}

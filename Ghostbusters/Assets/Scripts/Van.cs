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
    public int numberOfStoredGhosts = 0;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public float GetInteractionRadius()
    {
        return _interactionRadius;
    }

    public void DepositGhosts(int storedGhosts)
    {
        AudioManager.Instance.Play("Ding");
        anim.SetTrigger("Deposit");
        numberOfStoredGhosts += storedGhosts;
        PlayerManager.Instance.CalculateScore();
    }
}

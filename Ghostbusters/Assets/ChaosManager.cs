using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static ChaosManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static ChaosManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void PickRandomChaosEvent()
    {
        StartCoroutine(SuperSpeed());

    }

    private IEnumerator SuperSpeed()
    {
        foreach (Player player in PlayerManager.Instance.players)
        {
            player._moveSpeed = 100;
        }
        yield return new WaitForSeconds(5);
        foreach (Player player in PlayerManager.Instance.players)
        {
            player._moveSpeed = 25;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

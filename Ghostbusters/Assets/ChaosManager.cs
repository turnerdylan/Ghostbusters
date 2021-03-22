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
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                StartCoroutine(SuperSpeed());
                break;
            case 1:
                StartCoroutine(BackwardsControls());
                break;
            case 2:
                TeleportPlayers(); //this only teleports the first two players
                break;
        }
    }

    private void TeleportPlayers()
    {
        var p1 = PlayerManager.Instance.GetPlayerArray()[0];
        var p2 = PlayerManager.Instance.GetPlayerArray()[1];

        var position1 = p1.transform.position;
        var position2 = p2.transform.position;

        p1.transform.position = position2;
        p2.transform.position = position1;
    }

    private IEnumerator BackwardsControls()
    {
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.SetBackwardsControls(true);
        }
        yield return new WaitForSeconds(5);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player.SetBackwardsControls(false);
        }
    }

    private IEnumerator SuperSpeed()
    {
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player._moveSpeed = 100;
        }
        yield return new WaitForSeconds(5);
        foreach (Player player in PlayerManager.Instance.GetPlayerArray())
        {
            player._moveSpeed = 25;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    //players are spawned in awake
    #region Singleton Setup and Awake
    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static PlayerManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;

        if (testMode)
        {
            Debug.LogWarning("TEST MODE IS ON");
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var currentPlayer = Instantiate(playerSkins[i], playerSpawns[i].position, Quaternion.identity);
                players.Add(currentPlayer.GetComponent<Player>());
            }
        }
        else
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                int playerskin = DataSelectManager.Instance.playerIndexes[i];//this is the info for what skin they picked
                var currentPlayer = Instantiate(playerSkins[playerskin], playerSpawns[i].position, Quaternion.identity);
                players.Add(currentPlayer.GetComponent<Player>());
            }
        }
    }
    #endregion
    public bool testMode = false;
    //Player[] players = new Player[4];      //maybe get the number of players from somewhere else??
    [SerializeField] private List<Player> players = new List<Player>();
    public List<GameObject> playerSkins;
    public List<Transform> playerSpawns;

    public int totalScore;
    public TextMeshProUGUI scoreText;

    public void SetAllPlayerControls(bool state)
    {
        foreach(Player player in players)
        {
            player.enabled = state;
        }
    }

    public bool CheckIfAllPlayersAreStunned()
    {
        foreach(Player player in players)
        {
            if(player.GetPlayerState() != PLAYER_STATE.STUNNED || player == null)
            {
                return false;
            }
        }
        //LevelManager.Instance.EndLevel();
        return true;
    }

    public void CalculateScore()
    {
        totalScore = 0;
        foreach(Player player in players)
        {
            totalScore += player.score;
        }
        scoreText.text = totalScore.ToString();
    }

    public List<Player> GetPlayerArray()
    {
        return players;
    }
    
    public Transform GetClosestPlayer()
    {
        Transform closestPlayerTransform = null;
        float distanceToClosestPlayerTemp = Mathf.Infinity;

        if (players == null) return null;

        foreach (Player player in players)
        {
            if (player.GetPlayerState() != PLAYER_STATE.STUNNED)
            {
                float currentCheckDistance = Vector3.Distance(player.transform.position, transform.position);
                if (currentCheckDistance < distanceToClosestPlayerTemp)
                {
                    closestPlayerTransform = player.transform;
                    distanceToClosestPlayerTemp = currentCheckDistance;
                }
            }
        }
        return closestPlayerTransform;
    }
}

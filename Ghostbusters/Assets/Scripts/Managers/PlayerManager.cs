using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
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
        DontDestroyOnLoad(gameObject);

        players = FindObjectsOfType<Player>();
    }
    #endregion

    public Player[] players = new Player[4];      //maybe get the number of players from somewhere else??
    private int totalScore;
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
        scoreText.text = "Score: " + totalScore;
    }
    
    public Transform GetClosestPlayer()
    {
        Transform closestPlayerTransform = null;
        float distanceToClosestPlayerTemp = Mathf.Infinity;

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
        if (closestPlayerTransform == null) return closestPlayerTransform;
        return closestPlayerTransform;
    }
}

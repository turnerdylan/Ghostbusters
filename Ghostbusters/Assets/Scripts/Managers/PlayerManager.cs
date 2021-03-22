﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            int playerskin = DataSelectManager.Instance.players[i].imageIndex;//this is the info for what skin they picked
            Instantiate(playerSkins[playerskin], playerSpawns[i].position, Quaternion.identity);
        }
    }
    #endregion

    Player[] players = new Player[4];      //maybe get the number of players from somewhere else??
    public List<GameObject> playerSkins;
    public List<Transform> playerSpawns;

    private int totalScore;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        
    }

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

    public Player[] GetPlayerArray()
    {
        return players;
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

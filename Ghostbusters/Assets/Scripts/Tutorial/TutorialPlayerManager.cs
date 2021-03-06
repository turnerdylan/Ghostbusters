﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialPlayerManager : MonoBehaviour
{
    //players are spawned in awake
    #region Singleton Setup and Awake
    public static TutorialPlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static TutorialPlayerManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var currentPlayer = Instantiate(playerSkins[i], playerSpawns[i].position, Quaternion.identity);
            players.Add(currentPlayer.GetComponent<TutorialPlayer>());
        }

    }
    #endregion
    public List<TutorialPlayer> players = new List<TutorialPlayer>();
    public List<GameObject> playerSkins;
    public List<Transform> playerSpawns;

    public int totalScore;
    public TextMeshProUGUI scoreText;
    public TutorialPeekaboo peekaboo;
    private bool initiatedStep;

    void Update()
    {
        int count = 0;
        foreach(TutorialPlayer player in players)
        {
            count += player._numberOfHeldGhosts;
        }
        if(count == 6 && !initiatedStep) 
        {
            initiatedStep = true;
            TutorialManager.Instance.TriggerWait(0.35f);
        }
    }
    public void SetAllPlayerControls(bool state)
    {
        foreach(TutorialPlayer player in players)
        {
            player.enabled = state;
        }
    }

    public bool CheckIfAllPlayersAreStunned()
    {
        foreach(TutorialPlayer player in players)
        {
            if(player.GetPlayerState() != TUTORIAL_PLAYER_STATE.STUNNED || player == null)
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
        foreach(TutorialPlayer player in players)
        {
            totalScore += player.score;
        }
        scoreText.text = totalScore.ToString();
        if(totalScore == 6)
        {
            TutorialManager.Instance.peekabooGhost.SetActive(true);
            TutorialManager.Instance.peekabooGhost.GetComponentInChildren<TutorialPeekaboo>().peekabooOne = false;
        }
        if(totalScore == 18)
        {
            TutorialManager.Instance.TriggerWait(0.35f);
        }
    }

    public List<TutorialPlayer> GetPlayerArray()
    {
        return players;
    }
    
    public Transform GetClosestPlayer(Transform target)
    {
        Transform closestPlayerTransform = null;
        float distanceToClosestPlayerTemp = Mathf.Infinity;

        if (players == null) return null;

        foreach (TutorialPlayer player in players)
        {
            if (player.GetPlayerState() != TUTORIAL_PLAYER_STATE.STUNNED)
            {
                float currentCheckDistance = Vector3.Distance(player.transform.position, target.position);
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

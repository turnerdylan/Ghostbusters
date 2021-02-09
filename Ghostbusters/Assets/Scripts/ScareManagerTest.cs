using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScareManagerTest : MonoBehaviour
{
    private List<BUTTON_PRESS> targetBtnList = new List<BUTTON_PRESS>();
    private List<BUTTON_PRESS> btnList = new List<BUTTON_PRESS>();
    public List<Player> players = new List<Player>();

    public Text[] buttonTexts = new Text[4];
    public Image background;
    private int arrayLength;
    public bool scareInitiated;
    public float timer = 10f;
    private float _timer;
    [SerializeField] private int[] btnCount = new int[4];   
    [SerializeField] private int[] targetBtnCount = new int[4];
    
    #region Singleton Setup and Awake
    public static ScareManagerTest Instance
    {
        get
        {
            return instance;
        }
    }

    private static ScareManagerTest instance = null;

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    void Start()
    {
        //arrayLength = PlayerManager.Instance.players.Length;
        _timer = timer;
        StartScare();

    }

    // Update is called once per frame
    void Update()
    {
        if(scareInitiated)
        {
            _timer -= Time.deltaTime;
            if(_timer > 0)
            {
                if(btnCount[0] > targetBtnCount[0] || btnCount[1] > targetBtnCount[1] || btnCount[2] > targetBtnCount[2] || btnCount[3] > targetBtnCount[3])
                {
                    //fail
                    background.color = Color.red;
                    ResetScare();
                }
                else if(btnCount[0] == targetBtnCount[0] && btnCount[1] == targetBtnCount[1] && btnCount[2] == targetBtnCount[2] && btnCount[3] == targetBtnCount[3])
                {
                    //success
                    background.color = Color.green;
                    ResetScare();
                }
            }
            else
            {
                //fail
                background.color = Color.red;
                ResetScare();
            }
        }
    }

    void ResetScare()
    {
        scareInitiated = false;
        _timer = timer;
        Array.Clear(btnCount, 0, btnCount.Length);
        Array.Clear(targetBtnCount, 0, targetBtnCount.Length);
        targetBtnList.Clear();
        btnList.Clear();
        players.Clear();
        foreach(Player player in players)
        {
            player.SetButtonPress(BUTTON_PRESS.None);
        }
    }
    void StartScare()
    {
        scareInitiated = true;
        GenerateSequence(targetBtnList);
        CountElements(targetBtnCount, targetBtnList);
        for (int i=0; i<buttonTexts.Length; i++)
        {
            switch(targetBtnList[i])
            {
                case BUTTON_PRESS.Up:
                    buttonTexts[i].text = "U";
                    break;
                case BUTTON_PRESS.Down:
                    buttonTexts[i].text = "D";
                    break;
                case BUTTON_PRESS.Left:
                    buttonTexts[i].text = "L";
                    break;
                case BUTTON_PRESS.Right:
                    buttonTexts[i].text = "R";
                    break;
            }
        }
    }
    public void AddPlayerScare(Player player)
    {
        // if(!scareInitiated)
        // {
        //     StartScare();
        // }
        // if(!players.Contains(player)) //prevents same player from being able to give multiple inputs
        // {
        //     players.Add(player);
        //     btnList.Add(player._buttonPressed);
        // }
        players.Add(player);
        btnList.Add(player.GetButtonPress());
        switch(player.GetButtonPress())
        {
            case BUTTON_PRESS.Up:
                btnCount[0]++;
                break;
            case BUTTON_PRESS.Down:
                btnCount[1]++;
                break;
            case BUTTON_PRESS.Left:
                btnCount[2]++;
                break;
            case BUTTON_PRESS.Right:
                btnCount[3]++;
                break;
        }

    }

    void CountElements(int[] btnCount, List<BUTTON_PRESS> btnList)
    {
        foreach(BUTTON_PRESS btnPressed in btnList)
        {
            switch(btnPressed)
            {
                case BUTTON_PRESS.Up:
                    btnCount[0]++;
                    break;
                case BUTTON_PRESS.Down:
                    btnCount[1]++;
                    break;
                case BUTTON_PRESS.Left:
                    btnCount[2]++;
                    break;
                case BUTTON_PRESS.Right:
                    btnCount[3]++;
                    break;
            }
        }
    }

    void GenerateSequence(List<BUTTON_PRESS> targetBtnList)
    {
        for(int i = 0; i<4; i++)// i < players.Length
        {
            int btn = UnityEngine.Random.Range(1,5);
            switch(btn)
            {
                case 1:
                    targetBtnList.Add(BUTTON_PRESS.Up);
                    break;
                case 2:
                    targetBtnList.Add(BUTTON_PRESS.Down);
                    break;
                case 3:
                    targetBtnList.Add(BUTTON_PRESS.Left);
                    break;
                case 4:
                    targetBtnList.Add(BUTTON_PRESS.Right);
                    break;
            }
        }
    }
}

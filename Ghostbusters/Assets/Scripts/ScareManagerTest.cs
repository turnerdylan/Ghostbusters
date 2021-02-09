using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScareManagerTest : MonoBehaviour
{
    private List<ButtonPressed> targetBtnList = new List<ButtonPressed>();
    private List<ButtonPressed> btnList = new List<ButtonPressed>();
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
            player._buttonPressed = ButtonPressed.None;
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
                case ButtonPressed.Up:
                    buttonTexts[i].text = "U";
                    break;
                case ButtonPressed.Down:
                    buttonTexts[i].text = "D";
                    break;
                case ButtonPressed.Left:
                    buttonTexts[i].text = "L";
                    break;
                case ButtonPressed.Right:
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
        btnList.Add(player._buttonPressed);
        switch(player._buttonPressed)
        {
            case ButtonPressed.Up:
                btnCount[0]++;
                break;
            case ButtonPressed.Down:
                btnCount[1]++;
                break;
            case ButtonPressed.Left:
                btnCount[2]++;
                break;
            case ButtonPressed.Right:
                btnCount[3]++;
                break;
        }

    }

    void CountElements(int[] btnCount, List<ButtonPressed> btnList)
    {
        foreach(ButtonPressed btnPressed in btnList)
        {
            switch(btnPressed)
            {
                case ButtonPressed.Up:
                    btnCount[0]++;
                    break;
                case ButtonPressed.Down:
                    btnCount[1]++;
                    break;
                case ButtonPressed.Left:
                    btnCount[2]++;
                    break;
                case ButtonPressed.Right:
                    btnCount[3]++;
                    break;
            }
        }
    }

    void GenerateSequence(List<ButtonPressed> targetBtnList)
    {
        for(int i = 0; i<4; i++)// i < players.Length
        {
            int btn = UnityEngine.Random.Range(1,5);
            switch(btn)
            {
                case 1:
                    targetBtnList.Add(ButtonPressed.Up);
                    break;
                case 2:
                    targetBtnList.Add(ButtonPressed.Down);
                    break;
                case 3:
                    targetBtnList.Add(ButtonPressed.Left);
                    break;
                case 4:
                    targetBtnList.Add(ButtonPressed.Right);
                    break;
            }
        }
    }
}

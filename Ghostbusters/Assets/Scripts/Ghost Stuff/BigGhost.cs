using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;

public class BigGhost : MonoBehaviour
{
    //references
    public GameObject explosivePrefab;
    public ParticleSystem hitEffect;
    public ParticleSystem explosionEffect;

    //private serializables
    [SerializeField] private int _ghostsToSpawn = 8;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;  
    [SerializeField] private bool _scareable = true;
    [SerializeField] private float _onScareInvincibilityTime = 2.5f;
    [SerializeField] private int[] btnCount = new int[4];   
    [SerializeField] private int[] targetBtnCount = new int[4];
    
    //private variables
    private bool scareInitiated = false;
    private int _listIndex;
    private float _scareInputsTimer;

    //public variables
    public List<Player> players = new List<Player>();

    public Image[] images = new Image[4];
    public GameObject buttonSequenceSprite;
    public Image timerBar;
    private List<BUTTON_PRESS> targetBtnList = new List<BUTTON_PRESS>();
    private List<BUTTON_PRESS> btnList = new List<BUTTON_PRESS>();
    private bool sequenceGenerated = false;
    private bool inRange;
    public float timer = 10f; //scare timer
    private float _timer; //scare timer
    public int scaresNeeded;
    public Sprite emptySprite;
    public Sprite playerSprite;
    public bool debugging;

    void Start()
    {
        _timer = timer;
    }

    void Update()
    {
        foreach(Player player in PlayerManager.Instance.GetPlayerArray())
        {
            if(Vector3.Distance(transform.position, player.transform.position) < player.GetScareRange())
            {
                inRange = true;
                break;
            }
            else
            {
                inRange = false;
            }
        }

        if(inRange)
        {
            buttonSequenceSprite.SetActive(true);
        }   
        else
        {
            buttonSequenceSprite.SetActive(false);
        }

        if(scareInitiated)
        {
            _timer -= Time.deltaTime;
            timerBar.fillAmount = _timer/timer;
            if(_timer > 0)
            {
                if(btnCount[0] > targetBtnCount[0] || btnCount[1] > targetBtnCount[1] || btnCount[2] > targetBtnCount[2] || btnCount[3] > targetBtnCount[3])
                {
                    //fail
                    ScareFail();
                }
                else if(btnCount[0] == targetBtnCount[0] && btnCount[1] == targetBtnCount[1] && btnCount[2] == targetBtnCount[2] && btnCount[3] == targetBtnCount[3])
                {
                    //success
                    ScareSuccess();
                }
            }
            else
            {
                //fail
                ScareFail();
            }
        }
    }

    private void OnEnable()
    {
        //_scareInputsTimer = _scareInputsTimerMaxTime;
        _scareable = false;
        //StartCoroutine(ScareInvincibility());
    }

    public void SplitApart()
    {
        foreach(Player player in players)
        {
            player.InitiateDisableTrigger(0.75f);
        }
        int spawnedGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.smallGhosts.Count; i++)
        {
            if (spawnedGhosts >= _ghostsToSpawn) break;
            if (!GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                GhostManager.Instance.smallGhosts[i].SetActive(true);
                // GhostManager.Instance.smallGhosts[i].transform.position = this.transform.position; //fix the math here to spawn them in separate locations
                GhostManager.Instance.smallGhosts[i].transform.position = this.transform.position + new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value).normalized * _ghostSpawnOffset;
                spawnedGhosts++;
            }
        }

        //GhostManager.Instance.mediumGhosts[ghost1].GetComponent<MediumGhostMovement>().TriggerSeparate(transform.position);
        //GhostManager.Instance.mediumGhosts[ghost2].GetComponent<MediumGhostMovement>().TriggerSeparate(transform.position);

        gameObject.SetActive(false);
        ChaosManager.Instance.PickRandomChaosEvent();
    }

    private void ScareSuccess()
    {
        SplitApart();
        ResetScare();
        //RandomEvent();
    }

    private void ScareFail()
    {
        ResetScare();
        Instantiate(explosivePrefab, transform.position, Quaternion.identity);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    IEnumerator ScareInvincibility()
    {

        yield return new WaitForSeconds(_onScareInvincibilityTime);
        _scareable = true;
    }

    public bool CheckIfScarable()
    {
        return _scareable;
    }

    public void SetListIndex(int index)
    {
        _listIndex = index;
    }
    void ResetScare()
    {
        scareInitiated = false;
        _timer = timer;
        System.Array.Clear(btnCount, 0, btnCount.Length);
        //System.Array.Clear(targetBtnCount, 0, targetBtnCount.Length);
        //targetBtnList.Clear();
        btnList.Clear();
        players.Clear();
        SetSprites();
        foreach(Player player in players)
        {
            player.SetButtonPress(BUTTON_PRESS.None);
        }
    }
    void StartScare()
    {
        scareInitiated = true;
    }
    public void AddPlayerScare(Player player)
    {
        // bool shouldAdd = false;
        if(!scareInitiated)
        {
            StartScare();
        }
        // MakePressed(player._buttonPressed);
        // switch(PlayerManager.Instance.players.Length)
        // {
        //     case 1:
        //         players.Add(player);
        //         btnList.Add(player._buttonPressed);
        //         shouldAdd = true;
        //         break;
        //     case 2:
        //         if(CountInList(player)<2)
        //         {
        //             players.Add(player);
        //             btnList.Add(player._buttonPressed);
        //             shouldAdd = true;
        //         }
        //         else
        //         {
        //             ScareFail();
        //         }
        //         break;
        //     case 3:
        //         if(CountInList(player)<2)
        //         {
        //             players.Add(player);
        //             btnList.Add(player._buttonPressed);
        //             shouldAdd = true;
        //         }
        //         else
        //         {
        //             ScareFail();
        //         }
        //         break;
        //     case 4:
        //         if(!players.Contains(player))
        //         {
        //             players.Add(player);
        //             btnList.Add(player._buttonPressed);
        //             shouldAdd = true;
        //         }
        //         else
        //         {
        //             ScareFail();
        //         }
        //         break;
        //     default:
        //         print("Invalid number of players");
        //         break;
        // }

        // if(shouldAdd)
        // {
        if(!players.Contains(player) || debugging)
        {
            switch(player.GetButtonPress())
            {
                case BUTTON_PRESS.Up:
                    if(targetBtnCount[0] == 1 && btnCount[0] != 1)
                    {
                        images[0].sprite = player.characterSprite;
                        btnCount[0] = 1;
                        players.Add(player);
                    }
                    else
                    {
                        ScareFail();
                    }
                    break;
                case BUTTON_PRESS.Down:
                    if(targetBtnCount[1] == 1 && btnCount[1] != 1)
                    {
                        images[1].sprite = player.characterSprite;
                        btnCount[1] = 1;
                        players.Add(player);
                    }
                    else
                    {
                        ScareFail();
                    }
                    break;
                case BUTTON_PRESS.Left:
                    if(targetBtnCount[2] == 1 && btnCount[2] != 1)
                    {
                        images[2].sprite = player.characterSprite;
                        btnCount[2] = 1;
                        players.Add(player);
                    }
                    else
                    {
                        ScareFail();
                    }
                    break;
                case BUTTON_PRESS.Right:
                    if(targetBtnCount[3] == 1 && btnCount[3] != 1)
                    {
                        images[3].sprite = player.characterSprite;
                        btnCount[3] = 1;
                        players.Add(player);
                    }
                    else
                    {
                        ScareFail();
                    }
                    break;
                default:
                    print("Invalid button state");
                    break;
            }
        }
        // }

    }

    // void CountElements(int[] btnCount, List<BUTTON_PRESS> btnList)
    // {
    //     foreach(BUTTON_PRESS btnPressed in btnList)
    //     {
    //         switch(btnPressed)
    //         {
    //             case BUTTON_PRESS.Up:
    //                 btnCount[0]++;
    //                 break;
    //             case BUTTON_PRESS.Down:
    //                 btnCount[1]++;
    //                 break;
    //             case BUTTON_PRESS.Left:
    //                 btnCount[2]++;
    //                 break;
    //             case BUTTON_PRESS.Right:
    //                 btnCount[3]++;
    //                 break;
    //             default:
    //                 print("Invalid button state");
    //                 break;
    //         }
    //     }
    // }

    public void GenerateSequence()
    {
        int i = 0;
        while(i<scaresNeeded)
        {
            int num = UnityEngine.Random.Range(0,4);
            if(targetBtnCount[num] != 1)
            {
                targetBtnCount[num] = 1;
                i++;
            }
        }
        SetSprites();
        sequenceGenerated = true;
    }
    void SetSprites()
    {
        for (int i=0; i<4; i++)
        {
            if(targetBtnCount[i] == 1)
            {
                images[i].sprite = playerSprite;
            }
            else
            {
                images[i].sprite = emptySprite;
            }
        }
    }
    
    int CountInList(Player player)
    {
        int count = 0;
        foreach(Player p in players)
        {
            if(p == player)
            {
                count++;
            }
        }
        return count;
    }
}

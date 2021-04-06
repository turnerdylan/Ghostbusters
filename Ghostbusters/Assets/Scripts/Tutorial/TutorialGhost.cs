using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public enum TUTORIAL_MEDIUM_STATE
{
    NORMAL,
    RUNNING,
};


public class TutorialGhost : MonoBehaviour
{
    //private variables
    private bool scareInitiated = false;
    private bool sequenceGenerated = false;
    private bool inRange;
    private float _timer; //scare timer
    private bool debugging;
    [Header("Ghost Spawning")]
    [SerializeField] private int _ghostsToSpawn = 4;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;  

    [Header("Scaring")]
    private int scaresNeeded;
    public float timer = 10f; //scare timer
    public List<TutorialPlayer> players = new List<TutorialPlayer>();
    [SerializeField] private int[] btnCount = new int[4];   
    [SerializeField] private int[] targetBtnCount = new int[4];

    [Header("Sprites")]
    public Sprite emptySprite;
    public Sprite playerSprite;
    public Image[] images = new Image[4];
    public GameObject buttonSequenceSprite;
    public Image timerBar;

    [Header("Effects")]
    private float explosionForce = 7500;
    public ParticleSystem explosionEffect;
    public GameObject puffPrefab;

    private void Start()
    {
        switch(TutorialPlayerManager.Instance.GetPlayerArray().Count)
        {
            case 1:
                debugging = true;
                scaresNeeded = 1;
                break;
            case 2:
                scaresNeeded = 1;
                break;
            case 3:
                scaresNeeded = 2;
                break;
            case 4:
                scaresNeeded = 2;
                break;
            default:
                Debug.Log("Invalid player array size");
                break;
        }
        GenerateSequence();
        _timer = timer;
    }

    private void Update()
    {
        foreach(TutorialPlayer player in TutorialPlayerManager.Instance.GetPlayerArray())
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
                    players[players.Count-1].FlashX(); //if wrong button is pressed or too many of one button then display an x above the last player to scare
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

    public void SplitApart()
    {
        AudioManager.Instance.Play("Pop");
        Instantiate(puffPrefab, transform.position, Quaternion.identity);
        foreach(TutorialPlayer player in players)
        {
            player.InitiateDisableTrigger(0.75f);
        }
        //set 2 medium ghosts active and set their positions to this position + offset
        for (int i = 0; i < _ghostsToSpawn; i++)
        {
            var newSmallGhost = Instantiate(TutorialGhostManager.Instance.smallGhostPrefab, transform.position, Quaternion.identity);
            newSmallGhost.transform.position = transform.position + new Vector3(Random.value, Random.value, Random.value).normalized * _ghostSpawnOffset;
        }
        TutorialManager.Instance.TriggerWait(0.35f);
        TutorialGhostManager.Instance.mediumGhostsInScene.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void AddPlayerScare(TutorialPlayer player)
    {
        if(!scareInitiated)
        {
            StartScare();
        }

        if(!players.Contains(player) || debugging)
        {
            switch(player.GetButtonPress())
            {
                case TUTORIAL_BUTTON_PRESS.Up:
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
                case TUTORIAL_BUTTON_PRESS.Down:
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
                case TUTORIAL_BUTTON_PRESS.Left:
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
                case TUTORIAL_BUTTON_PRESS.Right:
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
    }
    private void ScareSuccess()
    {
        SplitApart();
        ResetScare();
    }
    private void ScareFail()
    {
        ResetScare();
        Blowback();
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    void ResetScare()
    {
        scareInitiated = false;
        _timer = timer;
        System.Array.Clear(btnCount, 0, btnCount.Length);
        players.Clear();
        SetSprites();
        foreach(TutorialPlayer player in players)
        {
            player.SetButtonPress(TUTORIAL_BUTTON_PRESS.None);
        }
    }
    void StartScare()
    {
        scareInitiated = true;
    }

    public void GenerateSequence()
    {
        // int i = 0;
        // while(i<scaresNeeded)
        // {
        //     int num = UnityEngine.Random.Range(0,4);
        //     if(targetBtnCount[num] != 1)
        //     {
        //         targetBtnCount[num] = 1;
        //         i++;
        //     }
        // }
        targetBtnCount[0] = 1;
        targetBtnCount[1] = 1;
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
    void Blowback()
    {
        foreach(TutorialPlayer player in TutorialPlayerManager.Instance.GetPlayerArray())
        {
            if(Vector3.Distance(transform.position, player.transform.position) <= player.GetScareRange())
            {
                Vector3 direction = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized;
                player.TriggerDisableMovement(0.35f);
                player.GetComponent<Rigidbody>().AddForce(direction*explosionForce);
            }

        }
    }
}

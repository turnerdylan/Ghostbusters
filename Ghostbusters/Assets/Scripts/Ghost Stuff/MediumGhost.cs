using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public enum MEDIUM_STATE
{
    NORMAL,
    RUNNING,
};


public class MediumGhost : MonoBehaviour
{
    //private variables
    private bool scareInitiated = false;
    private bool sequenceGenerated = false;
    private bool inRange;
    private float _timer; //scare timer
    [SerializeField] private bool debugging;
    [Header("Ghost Spawning")]
    [SerializeField] private int _ghostsToSpawn = 4;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;  

    [Header("Scaring")]
    [SerializeField] private int scaresNeeded;
    public float timer = 10f; //scare timer
    public List<Player> players = new List<Player>();
    [SerializeField] private int[] btnCount = new int[4];   
    [SerializeField] private int[] targetBtnCount = new int[4];

    [Header("Sprites")]
    public Sprite emptySprite;
    public Sprite playerSprite;
    public Image[] images = new Image[4];
    public GameObject buttonSequenceSprite;
    public Image timerBar;

    [Header("Effects")]
    public GameObject explosivePrefab;
    public ParticleSystem explosionEffect;
    public GameObject puffPrefab;

    private void Start()
    {
        switch(PlayerManager.Instance.GetPlayerArray().Count)
        {
            case 1:
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
            //timerBar.fillAmount = _timer/timer;
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
        foreach(Player player in players)
        {
            player.InitiateDisableTrigger(0.75f);
        }
        //set 2 medium ghosts active and set their positions to this position + offset
        for (int i = 0; i < _ghostsToSpawn; i++)
        {
            var newSmallGhost = Instantiate(GhostManager.Instance.smallGhostPrefab, transform.position, Quaternion.identity);
            newSmallGhost.transform.position = transform.position + new Vector3(Random.value, Random.value, Random.value).normalized * _ghostSpawnOffset;
        }

        GhostManager.Instance.mediumGhostsInScene.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void AddPlayerScare(Player player)
    {
        if(!scareInitiated)
        {
            StartScare();
        }

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
    }
    private void ScareSuccess()
    {
        SplitApart();
        ResetScare();
    }
    private void ScareFail()
    {
        ResetScare();
        Instantiate(explosivePrefab, transform.position, Quaternion.identity);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    void ResetScare()
    {
        scareInitiated = false;
        _timer = timer;
        System.Array.Clear(btnCount, 0, btnCount.Length);
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
}

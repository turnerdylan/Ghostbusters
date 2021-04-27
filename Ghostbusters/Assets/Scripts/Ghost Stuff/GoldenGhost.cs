using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class GoldenGhost : MonoBehaviour
{    
    //private variables
    private bool scareInitiated = false;
    private bool sequenceGenerated = false;
    private bool inRange;
    private float _timer; //scare timer

    [Header("Ghost Spawning")]
    [SerializeField] private int _ghostsToSpawn = 8;
    private float ghostSpawnRadius = 3.0f;
    
    [Header("Scaring")]
    public int scaresNeeded;
    public float timer = 10f; //scare timer
    private int[] btnCount = new int[4];   
    private int[] targetBtnCount = new int[4];
    List<Player> players = new List<Player>();
    private bool debugging;

    [Header("Chaos event: 0=speed, 1=controls, 2=lights, 3=ice, 4=smoke,")]
    [Range(0, 4)]
    public int chaosEventIndex;

    [Header("Sprites")]
    public Sprite emptySprite;
    public Sprite playerSprite;
    public Image timerBar;
    public GameObject buttonSequenceSprite;
    public Image[] images = new Image[4];

    [Header("Effects")]
    private float explosionForce = 7500;
    public ParticleSystem explosionEffect;
    public GameObject puffPrefab;

    void Start()
    {
        switch(PlayerManager.Instance.GetPlayerArray().Count)
        {
            case 1:
                scaresNeeded = 2;
                debugging = true;
                break;
            case 2:
                scaresNeeded = 2;
                break;
            case 3:
                scaresNeeded = 3;
                break;
            case 4:
                scaresNeeded = 4;
                break;
            default:
                Debug.Log("Invalid player array size");
                break;
        }
        GenerateSequence();
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
                    ScareFail();
                }
                else if(btnCount[0] == targetBtnCount[0] && btnCount[1] == targetBtnCount[1] && btnCount[2] == targetBtnCount[2] && btnCount[3] == targetBtnCount[3])
                {
                    ScareSuccess();
                }
            }
            else
            {
                ScareFail();
            }
        }
    }


    public void SplitApart()
    {
        AudioManager.Instance.Play("Pop");
        Instantiate(puffPrefab, transform.position, Quaternion.identity);
        foreach (Player player in players)
        {
            player.InitiateDisableTrigger(1.5f);
        }
        for (int i = 0; i < _ghostsToSpawn; i++)
        {
            var newSmallGhost = Instantiate(GhostManager.Instance.smallGoldGhostPrefab, transform.position, Quaternion.identity);
            float theta = i * 2 * Mathf.PI / _ghostsToSpawn;
            float x = Mathf.Sin(theta)*ghostSpawnRadius;
            float z = Mathf.Cos(theta)*ghostSpawnRadius;
        
            newSmallGhost.transform.position = transform.position + new Vector3(x, 0, z); 
        }

        GhostManager.Instance.goldenGhostsInScene.Remove(gameObject);
        Destroy(gameObject);
        ChaosManager.Instance.PickChaosEvent(chaosEventIndex, transform.position);
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
    void Blowback()
    {
        foreach(Player player in PlayerManager.Instance.GetPlayerArray())
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MediumGhost : MonoBehaviour
{
    //references
    public GameObject explosivePrefab;
    public GameObject puffPrefab;
    Rigidbody rb;

    //private serializables
    [SerializeField] private int _ghostsToSpawn = 4;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;    
    [SerializeField] private bool formable = true;
    [SerializeField] private float _scareInputsTimerMaxTime = 0.2f;
    [SerializeField] private float _onScareInvincibilityTime = 1.0f;
    [SerializeField] private float _transformTimerMax = 1.5f;
    [SerializeField] private int[] btnCount = new int[4];   
    [SerializeField] private int[] targetBtnCount = new int[4];

    //private variables
    private int _listIndex = -1;
    private float _transformTimer;
    private bool _canTransform = false;
    private bool scareInitiated = false;
    
    
    private float _scareInputsTimer;

    //public variables
    public List<Player> players = new List<Player>();

    // public Sprite[] sprites = new Sprite[4];
    // //public Sprite[] pressedSprites = new Sprite[4];
    // public Sprite checkMark;
    public Image[] images = new Image[4];
    public GameObject buttonSequenceSprite;
    public Image timerBar;
    private List<BUTTON_PRESS> targetBtnList = new List<BUTTON_PRESS>();
    private List<BUTTON_PRESS> btnList = new List<BUTTON_PRESS>();
    private bool sequenceGenerated = false;
    private bool inRange;
    //private int arrayLength;
    public float timer = 10f; //scare timer
    private float _timer; //scare timer
    public int scaresNeeded;
    public Sprite emptySprite;
    public Sprite playerSprite;
    public bool debugging;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _transformTimer = _transformTimerMax;

        _timer = timer;
    }

    private void Update()
    {
        if (_canTransform)
        {
            _transformTimer -= Time.deltaTime;
        }

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

    private void OnEnable()
    {

        _scareInputsTimer = _scareInputsTimerMaxTime;
        _canTransform = true;
        _transformTimer = _transformTimerMax;
        formable = false;
        //StartCoroutine(ScareInvincibilityDelay());
    }

    public void SplitApart()
    {
        foreach(Player player in players)
        {
            player.InitiateDisableTrigger(0.75f);
        }
        //set 2 medium ghosts active and set their positions to this position + offset
        int spawnedGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.smallGhosts.Count; i++)
        {
            if (spawnedGhosts >= _ghostsToSpawn) break;
            if (!GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                GhostManager.Instance.smallGhosts[i].SetActive(true);
                GhostManager.Instance.smallGhosts[i].transform.position = this.transform.position; //fix the math here to spawn them in separate locations
                GhostManager.Instance.smallGhosts[i].transform.position = this.transform.position + new Vector3(Random.value, Random.value, Random.value).normalized * _ghostSpawnOffset;
                spawnedGhosts++;
            }
        }
        Instantiate(puffPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void SetListIndex(int index)
    {
        _listIndex = index;
    }

    public void AddPlayerScare(Player player)
    {
        // bool shouldAdd = false;
        if(!scareInitiated)
        {
            StartScare();
        }
        // MakePressed(player._buttonPressed);
        // if(PlayerManager.Instance.players.Length > 1)
        // {
        //     if(!players.Contains(player))
        //     {
        //         players.Add(player);
        //         btnList.Add(player._buttonPressed);
        //         shouldAdd = true;
        //     }
        //     else
        //     {
        //         ScareFail();
        //     }
        // }
        // else
        // {
        //     players.Add(player);
        //     btnList.Add(player._buttonPressed);
        //     shouldAdd = true;
        // }

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

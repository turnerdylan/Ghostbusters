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
    //references
    public GameObject smallGhost;
    public GameObject explosivePrefab;
    Rigidbody rb;

    //private serializables
    [SerializeField] private int _ghostsToSpawn = 4;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;    
    [SerializeField] private bool formable = true;
    [SerializeField] private int _scaresNeeded = 1;
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

    public Sprite[] sprites = new Sprite[4];
    //public Sprite[] pressedSprites = new Sprite[4];
    public Sprite checkMark;
    public List<Image> images = new List<Image>();
    public GameObject buttonSequenceSprite;
    public Image timerBar;
    private List<BUTTON_PRESS> targetBtnList = new List<BUTTON_PRESS>();
    private List<BUTTON_PRESS> btnList = new List<BUTTON_PRESS>();
    private bool sequenceGenerated = false;
    private bool inRange;
    //private int arrayLength;
    public float timer = 10f; //scare timer
    private float _timer; //scare timer

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
            if(Vector3.Distance(transform.position, player.transform.position) < player._scareRange)
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
        gameObject.SetActive(false);
    }

    IEnumerator ScareInvincibilityDelay()
    {
        yield return new WaitForSeconds(_onScareInvincibilityTime);
        formable = true;
    }

    public float GetTransformTimer()
    {
        return _transformTimer;
    }

    public bool CheckIfFormable()
    {
        return formable;
    }

    public void SetListIndex(int index)
    {
        _listIndex = index;
    }

    public void AddPlayerScare(Player player)
    {
        bool shouldAdd = false;
        if(!scareInitiated)
        {
            StartScare();
        }
        MakePressed(player._buttonPressed);
        if(PlayerManager.Instance.GetPlayerArray().Length > 1)
        {
            if(!players.Contains(player))
            {
                players.Add(player);
                btnList.Add(player._buttonPressed);
                shouldAdd = true;
            }
            else
            {
                ScareFail();
            }
        }
        else
        {
            players.Add(player);
            btnList.Add(player._buttonPressed);
            shouldAdd = true;
        }

        if(shouldAdd)
        {
            switch(player._buttonPressed)
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
            player._buttonPressed = BUTTON_PRESS.None;
        }
    }
    void StartScare()
    {
        scareInitiated = true;
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
                default:
                    print("Invalid button state");
                    break;
            }
        }
    }

    public void GenerateSequence()
    {
        for(int i = 0; i<2; i++)// i < players.Length
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
                default:
                    print("Invalid number generation");
                    break;
            }
        }
        SetSprites();
        CountElements(targetBtnCount, targetBtnList);
        sequenceGenerated = true;
    }
    void SetSprites()
    {
        for (int i=0; i<images.Count; i++)
        {
            switch(targetBtnList[i])
            {
                case BUTTON_PRESS.Up:
                    images[i].sprite = sprites[0];
                    break;
                case BUTTON_PRESS.Down:
                    images[i].sprite = sprites[1];
                    break;
                case BUTTON_PRESS.Left:
                    images[i].sprite = sprites[2];
                    break;
                case BUTTON_PRESS.Right:
                    images[i].sprite = sprites[3];
                    break;
                default:
                    print("Invalid button state");
                    break;
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
    void MakePressed(BUTTON_PRESS button)
    {
        bool pressed = false;
        for(int i = 0; i<targetBtnList.Count; i++)
        {
            if(button == targetBtnList[i] && !pressed)
            {
                if(images[i].sprite != checkMark)
                {
                    images[i].sprite = checkMark;
                    pressed = true;
                }
            }
            if(pressed)
            {
                break;
            }
        }
    }
}

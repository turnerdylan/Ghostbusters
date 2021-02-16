using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGhost : MonoBehaviour
{
    //references

    //private serializables
    [SerializeField] private float _transformTimerMax = 1.5f;
    [SerializeField] private bool _scareable = true;
    [SerializeField] private float _onScareInvincibilityTime = 1.0f;
    [SerializeField] private int[] btnCount = new int[4];   
    [SerializeField] private int[] targetBtnCount = new int[4];

    public List<Player> players = new List<Player>();

    public Sprite[] sprites = new Sprite[4];
    public List<SpriteRenderer> spriteRends = new List<SpriteRenderer>();
    public GameObject buttonSequenceSprite;
    private List<BUTTON_PRESS> targetBtnList = new List<BUTTON_PRESS>();
    private List<BUTTON_PRESS> btnList = new List<BUTTON_PRESS>();
    private bool sequenceGenerated = false;
    private bool inRange;
    //private int arrayLength;
    public float timer = 10f; //scare timer
    private float _timer; //scare timer

    //private variables
    private int _listIndex;
    bool _canTransform = false;
    public float _transformTimer;
    private bool scareInitiated = false;

    //public variables

    private void Start()
    {
        _transformTimer = _transformTimerMax;
        _timer = timer;
    }

    private void Update()
    {
        if(_canTransform)
        {
            _transformTimer -= Time.deltaTime;
        }

        foreach(Player player in PlayerManager.Instance.players)
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
            if(!sequenceGenerated)
            {
                GenerateSequence();
                buttonSequenceSprite.SetActive(true);
            }
        }   
        else
        {
            buttonSequenceSprite.SetActive(false);
            sequenceGenerated = false;
            ResetScare();
        }

        if(scareInitiated)
        {
            _timer -= Time.deltaTime;
            if(_timer > 0)
            {
                if(btnCount[0] > targetBtnCount[0] || btnCount[1] > targetBtnCount[1] || btnCount[2] > targetBtnCount[2] || btnCount[3] > targetBtnCount[3])
                {
                    //fail
                    ScareFail();
                    ResetScare();
                }
                else if(btnCount[0] == targetBtnCount[0] && btnCount[1] == targetBtnCount[1] && btnCount[2] == targetBtnCount[2] && btnCount[3] == targetBtnCount[3])
                {
                    //success
                    ScareSuccess();
                    ResetScare();
                }
            }
            else
            {
                //fail
                ScareFail();
                ResetScare();
            }
        }
    }

    private void OnEnable()
    {
        GenerateSequence();
        _canTransform = true;
        _transformTimer = _transformTimerMax;
        _scareable = false;
        StartCoroutine(ScareInvincibility());
    }

    public void Banish()
    {
        gameObject.SetActive(false);
    }

    IEnumerator ScareInvincibility()
    {
        yield return new WaitForSeconds(_onScareInvincibilityTime);
        _scareable = true;
    }

    public bool GetScarable()
    {
        return _scareable;
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
        switch(PlayerManager.Instance.players.Length)
        {
            case 1:
                players.Add(player);
                btnList.Add(player._buttonPressed);
                shouldAdd = true;
                break;
            case 2:
                if(CountInList(player)<2)
                {
                    players.Add(player);
                    btnList.Add(player._buttonPressed);
                    shouldAdd = true;
                }
                else
                {
                    ScareFail();
                }
                break;
            case 3:
                if(CountInList(player)<2)
                {
                    players.Add(player);
                    btnList.Add(player._buttonPressed);
                    shouldAdd = true;
                }
                else
                {
                    ScareFail();
                }
                break;
            case 4:
                if(CountInList(player)<1)
                {
                    players.Add(player);
                    btnList.Add(player._buttonPressed);
                    shouldAdd = true;
                }
                else
                {
                    ScareFail();
                }
                break;
            default:
                print("Invalid number of players");
                break;
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
        Banish();
    }

    private void ScareFail()
    {
        //Instantiate(explosivePrefab, transform.position, Quaternion.identity);
    }

    void ResetScare()
    {
        scareInitiated = false;
        _timer = timer;
        System.Array.Clear(btnCount, 0, btnCount.Length);
        System.Array.Clear(targetBtnCount, 0, targetBtnCount.Length);
        targetBtnList.Clear();
        btnList.Clear();
        players.Clear();
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

    void GenerateSequence() //get rid of for loop for small ghost
    {
        for(int i = 0; i<1; i++)// i < players.Length
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

        for (int i=0; i<spriteRends.Count; i++)
        {
            switch(targetBtnList[i])
            {
                case BUTTON_PRESS.Up:
                    spriteRends[i].sprite = sprites[0];
                    break;
                case BUTTON_PRESS.Down:
                    spriteRends[i].sprite = sprites[1];
                    break;
                case BUTTON_PRESS.Left:
                    spriteRends[i].sprite = sprites[2];
                    break;
                case BUTTON_PRESS.Right:
                    spriteRends[i].sprite = sprites[3];
                    break;
                default:
                    print("Invalid button state");
                    break;
            }
        }
        CountElements(targetBtnCount, targetBtnList);
        sequenceGenerated = true;
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

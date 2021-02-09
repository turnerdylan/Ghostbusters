using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BigGhost : MonoBehaviour
{
    //references
    [SerializeField] private GameObject mediumGhost = null;
    [SerializeField] private TextMeshPro scareFeedbackText = null;
    public GameObject explosivePrefab;

    //private serializables
    [SerializeField] private int _ghostsToSpawn = 2;
    [SerializeField] private float _scareInputsTimerMaxTime = 0.2f;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;
    [SerializeField] private bool _scareable = true;
    [SerializeField] private int _scaresNeeded = 1;
    [SerializeField] private float _onScareInvincibilityTime = 2.5f;
    [SerializeField] private int[] btnCount = new int[4];   
    [SerializeField] private int[] targetBtnCount = new int[4];
    
    //private variables
    private bool scareInitiated = false;
    private int _listIndex;
    private float _scareInputsTimer;
    private bool _scareable = true;
    private int _scaresNeeded = 1;

    //public variables
    public List<Player> players = new List<Player>();  //TODO: does this need to exist or can we reference the player manager?
    public Sprite[] sprites = new Sprite[4];
    public List<SpriteRenderer> spriteRends = new List<SpriteRenderer>();
    public GameObject buttonSequenceSprite;
    private List<ButtonPressed> targetBtnList = new List<ButtonPressed>();
    private List<ButtonPressed> btnList = new List<ButtonPressed>();
    private bool sequenceGenerated = false;
    private bool inRange;
    //private int arrayLength;
    public float timer = 10f; //scare timer
    private float _timer; //scare timer

    void Start()
    {
        _scaresNeeded = PlayerManager.Instance.players.Length; //it takes all players in the scene to split big ghost
        _timer = timer;
    }

    void Update()
    {
        scareFeedbackText.text = players.Count.ToString();
        print(players.Count);
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
        //_scareInputsTimer = _scareInputsTimerMaxTime;
        _scareable = false;
        StartCoroutine(ScareInvincibility());
    }

    public void SplitApart()
    {
        //set 2 medium ghosts active and set their positions to this position + offset
        int spawnedGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.mediumGhosts.Count; i++)
        {
            if (spawnedGhosts >= _ghostsToSpawn) break;
            if(!GhostManager.Instance.mediumGhosts[i].activeSelf)
            {
                GhostManager.Instance.mediumGhosts[i].SetActive(true);
                GhostManager.Instance.mediumGhosts[i].transform.position = transform.position;
                spawnedGhosts++;
            }
        }
        gameObject.SetActive(false);
    }

    private void ScareSuccess()
    {
        SplitApart();
        //RandomEvent or blow back
    }

    private void ScareFail()
    {
        Instantiate(explosivePrefab, transform.position, Quaternion.identity);
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
        System.Array.Clear(targetBtnCount, 0, targetBtnCount.Length);
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
                break;
            case 3:
                if(CountInList(player)<2)
                {
                    players.Add(player);
                    btnList.Add(player._buttonPressed);
                    shouldAdd = true;
                }
                break;
            case 4:
                if(CountInList(player)<1)
                {
                    players.Add(player);
                    btnList.Add(player._buttonPressed);
                    shouldAdd = true;
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
                default:
                    print("Invalid button state");
                    break;
            }
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
                default:
                    print("Invalid button state");
                    break;
            }
        }
    }

    void GenerateSequence()
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
                default:
                    print("Invalid number generation");
                    break;
            }
        }

        for (int i=0; i<spriteRends.Count; i++)
        {
            switch(targetBtnList[i])
            {
                case ButtonPressed.Up:
                    spriteRends[i].sprite = sprites[0];
                    break;
                case ButtonPressed.Down:
                    spriteRends[i].sprite = sprites[1];
                    break;
                case ButtonPressed.Left:
                    spriteRends[i].sprite = sprites[2];
                    break;
                case ButtonPressed.Right:
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

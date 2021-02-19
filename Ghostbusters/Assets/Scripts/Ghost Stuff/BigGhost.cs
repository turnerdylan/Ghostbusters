using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class BigGhost : MonoBehaviour
{
    //references
    [SerializeField] private TextMeshPro scareFeedbackText = null;
    public GameObject explosivePrefab;
    public ParticleSystem hitEffect;
    public ParticleSystem explosionEffect;

    //private serializables
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
    public Sprite[] sprites = new Sprite[4];
    public Sprite[] pressedSprites = new Sprite[4];
    public List<SpriteRenderer> spriteRends = new List<SpriteRenderer>();
    public GameObject buttonSequenceSprite;
    public Image timerBar;
    private List<BUTTON_PRESS> targetBtnList = new List<BUTTON_PRESS>();
    private List<BUTTON_PRESS> btnList = new List<BUTTON_PRESS>();
    private bool sequenceGenerated = false;
    private bool inRange;
    //private int arrayLength;
    public float timer = 10f; //scare timer
    private float _timer; //scare timer

    void Start()
    {
        _timer = timer;
    }

    void Update()
    {
        scareFeedbackText.text = players.Count.ToString();
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
        StartCoroutine(ScareInvincibility());
    }

    public void SplitApart()
    {
        int ghost1 = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.mediumGhosts);
        GhostManager.Instance.mediumGhosts[ghost1].SetActive(true);
        GhostManager.Instance.mediumGhosts[ghost1].transform.position = transform.position;
        //GhostManager.Instance.mediumGhosts[ghost1].GetComponent<MediumGhost>().TriggerRunAway();

        int ghost2 = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.mediumGhosts);
        //set active
        GhostManager.Instance.mediumGhosts[ghost2].SetActive(true);
        GhostManager.Instance.mediumGhosts[ghost2].transform.position = transform.position;
        
        gameObject.SetActive(false);
    }

    private void ScareSuccess()
    {
        ResetScare();
        SplitApart();
        //RandomEvent or blow back
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
        foreach(Player player in players)
        {
            player._buttonPressed = BUTTON_PRESS.None;
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
        MakePressed(player._buttonPressed);
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

    void MakePressed(BUTTON_PRESS button)
    {
        bool pressed = false;
        for(int i = 0; i<targetBtnList.Count; i++)
        {
            if(button == targetBtnList[i] && !pressed)
            {
                switch(button)
                {
                    case BUTTON_PRESS.Up:
                        if(spriteRends[i].sprite != pressedSprites[0])
                        {
                            spriteRends[i].sprite = pressedSprites[0];
                            pressed = true;
                        }
                        break;
                    case BUTTON_PRESS.Down:
                        if(spriteRends[i].sprite != pressedSprites[1])
                        {
                            spriteRends[i].sprite = pressedSprites[1];
                            pressed = true;
                        }
                        break;
                    case BUTTON_PRESS.Left:
                        if(spriteRends[i].sprite != pressedSprites[2])
                        {
                            spriteRends[i].sprite = pressedSprites[2];
                            pressed = true;
                        }
                        break;
                    case BUTTON_PRESS.Right:
                        if(spriteRends[i].sprite != pressedSprites[3])
                        {
                            spriteRends[i].sprite = pressedSprites[3];
                            pressed = true;
                        }
                        break;
                    default:
                        print("Invalid button state");
                        break;
                }
            }
            if(pressed)
            {
                break;
            }
        }
    }
}

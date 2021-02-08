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
    [SerializeField] private float _onScareInvincibilityTime = 2.5f;
    
    //private variables
    private bool scareInitiated = false;
    private int _listIndex;
    private float _scareInputsTimer;
    private bool _scareable = true;
    private int _scaresNeeded = 1;


    //public variables
    public List<Player> players = new List<Player>();  //TODO: does this need to exist or can we reference the player manager?

    void Start()
    {
        _scaresNeeded = PlayerManager.Instance.players.Length;
    }

    void Update()
    {
        scareFeedbackText.text = players.Count.ToString();
        print(players.Count);

        if(scareInitiated)
        {
            _scareInputsTimer -= Time.deltaTime; 
            if(_scareInputsTimer > 0)
            {
                if(players.Count == _scaresNeeded)
                {
                    Debug.Log("Success!");
                    ScareSuccess();
                    scareInitiated = false;
                    _scareInputsTimer = _scareInputsTimerMaxTime;
                    players.Clear();
                }
            }
            else
            {
                Debug.Log("Fail!");
                //ScareFail();
                scareInitiated = false;
                _scareInputsTimer = _scareInputsTimerMaxTime;
                players.Clear();
            }
        }
    }

    private void OnEnable()
    {
        _scareInputsTimer = _scareInputsTimerMaxTime;
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

    public void AddPlayerScare(Player player)
    {
        print("testing123");
        scareInitiated = true;
        _scareInputsTimer = _scareInputsTimerMaxTime;

        if (!players.Contains(player)) //not sure if this actually works 
        {
            players.Add(player);
        }
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
}

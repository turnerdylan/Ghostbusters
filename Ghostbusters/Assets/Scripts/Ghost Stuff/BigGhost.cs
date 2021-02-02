using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BigGhost : MonoBehaviour
{
    //references
    [SerializeField] private GameObject mediumGhost;
    [SerializeField] private TextMeshPro scareFeedbackText;
    public GameObject explosivePrefab;

    //private serializables
    [SerializeField] private int _ghostsToSpawn = 2;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;
    [SerializeField] private bool _scareable = true;
    [SerializeField] private int _scaresNeeded = 1;
    [SerializeField] private float _scareInputsTimerMaxTime = 0.2f;
    [SerializeField] private float _onScareInvincibilityTime = 2.5f;
    

    //private variables
    private bool scareInitiated = false;
    private int _listIndex;
    private float _scareInputsTimer;

    //public variables
    public List<Player> players = new List<Player>();  //TODO: does this need to exist or can we reference the player manager?

    void Start()
    {
        _scaresNeeded = PlayerManager.Instance.players.Length;
    }
    void Update()
    {
        scareFeedbackText.text = players.Count.ToString();

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
                ScareFail();
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
                Vector3 testposition = gameObject.transform.position + new Vector3(Random.value, Random.value, Random.value).normalized * _ghostSpawnOffset; //fix the math here to spawn them in separate locations
                GhostManager.Instance.mediumGhosts[i].transform.position = testposition;
                spawnedGhosts++;
            }
        }
        gameObject.SetActive(false);
    }

    public void AddPlayerScare(Player player)
    {
        if(!scareInitiated)
        {
            scareInitiated = true;
        }
        if(!players.Contains(player)) //not sure if this actually works 
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

    public bool GetScarable()
    {
        return _scareable;
    }

    public void SetListIndex(int index)
    {
        _listIndex = index;
    }
}

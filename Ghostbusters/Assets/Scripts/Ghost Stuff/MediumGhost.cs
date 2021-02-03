using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MediumGhost : MonoBehaviour
{
    //references
    public GameObject smallGhost;
    public GameObject explosivePrefab;
    //private serializables
    [SerializeField] private int _ghostsToSpawn = 2;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;    
    [SerializeField] private bool _scareable = true;
    [SerializeField] private int _scaresNeeded = 1;
    [SerializeField] private float _scareInputsTimerMaxTime = 0.2f;
    [SerializeField] private float _onScareInvincibilityTime = 1.0f;
    [SerializeField] private float _transformTimerMax = 1.5f;

    //private variables
    private int _listIndex = -1;
    private float _transformTimer;
    private bool _canTransform = false;
    private bool scareInitiated = false;
    
    private float _scareInputsTimer;

    //public variables
    public List<Player> players = new List<Player>();

    private void Start()
    {
        _transformTimer = _transformTimerMax;
        if(PlayerManager.Instance.players.Length == 1)
            _scaresNeeded = 1;
        else
            _scaresNeeded = 2;
    }

    private void Update()
    {
        if (_canTransform)
        {
            _transformTimer -= Time.deltaTime;
        }

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
        _canTransform = true;
        _transformTimer = _transformTimerMax;
        _scareable = false;
        StartCoroutine(ScareInvincibilityDelay());
    }

    public void SplitApart()
    {
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
        _scareable = true;
    }

    public float GetTransformTimer()
    {
        return _transformTimer;
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
        if(!scareInitiated)
        {
            scareInitiated = true;
        }
        if(!players.Contains(player))
        {
            players.Add(player);
        }
    }
    private void ScareSuccess()
    {
        SplitApart();
    }
    private void ScareFail()
    {
        Instantiate(explosivePrefab, transform.position, Quaternion.identity);
    }
}

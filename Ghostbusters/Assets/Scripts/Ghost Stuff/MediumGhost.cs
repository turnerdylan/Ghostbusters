using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MediumGhost : MonoBehaviour
{
    //references
    public GameObject smallGhost;

    //private serializables
    [SerializeField] private int _ghostsToSpawn = 2;
    [SerializeField] private float _ghostSpawnOffset = 0.5f;
    [SerializeField] private bool _scareable = false;
    [SerializeField] private float _onScareInvincibilityTime = 1.5f;
    [SerializeField] private float _transformTimerMax = 1.5f;

    //private variables
    private int _listIndex = -1;
    private float _transformTimer;
    private bool _canTransform = false;

    //public variables

    private void Start()
    {
        _transformTimer = _transformTimerMax;
    }

    private void Update()
    {
        if (_canTransform)
        {
            _transformTimer -= Time.deltaTime;
        }
    }

    private void OnEnable()
    {
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
}

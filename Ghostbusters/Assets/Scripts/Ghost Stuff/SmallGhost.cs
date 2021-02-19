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

    public List<Player> players = new List<Player>();
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
    }

    private void OnEnable()
    {
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
}

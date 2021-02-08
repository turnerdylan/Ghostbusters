using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGhost : MonoBehaviour
{
    //references

    //private serializables
    [SerializeField] private float _transformTimerMax = 1.5f;
    [SerializeField] private bool _scareable = true;

    //private variables
    private int _listIndex;
    bool _canTransform = false;
    public float _transformTimer;

    //public variables

    private void Start()
    {
        _transformTimer = _transformTimerMax;
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
        yield return new WaitForSeconds(1);
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

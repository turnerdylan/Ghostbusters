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
    public float timer = 30f; //scare timer
    private float _timer; //scare timer

    //private variables
    private int _listIndex;
    bool _canTransform = false;
    public float _transformTimer;
    private bool scareInitiated = false;

    public float blinkDuration = 12.0f;
    private float blinkRate = 1.0f;
    private float _blinkRate;
    private float blinkAccel = 0.935f;
    private Material currMat;
    public Material redMat;
    bool isFlashing = false;
    //public variables

    private void Start()
    {
        _transformTimer = _transformTimerMax;
        currMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer > timer && !isFlashing)
        {
            FlashGhost();
        }
        // if(isFlashing)
        // {
        //     _blinkRate -= 0.0009f;
        // }
        // if(_canTransform)
        // {
        //     _transformTimer -= Time.deltaTime;
        // }
    }

    private void OnEnable()
    {
        _blinkRate = blinkRate;
        _timer = 0f;
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

    void FlashGhost()
    {
        isFlashing = true;
        StartCoroutine(Flash());
    }
    public IEnumerator Flash()
    {
        float counter = 0;
        float innerCounter = 0;

        bool isRed = false;

        while(counter < blinkDuration)
        {
            counter += Time.deltaTime;
            innerCounter += Time.deltaTime;

            if(innerCounter > _blinkRate)
            {
                _blinkRate *= blinkAccel;
                isRed = !isRed;
                innerCounter = 0f;
            }

            if(isRed)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().material = redMat;
            }
            else
            {
                GetComponentInChildren<SkinnedMeshRenderer>().material = currMat;
            }

            yield return null;
        }

        GetComponentInChildren<SkinnedMeshRenderer>().material = currMat;
        isFlashing = false;
        gameObject.SetActive(false);
        //explode
    }
}

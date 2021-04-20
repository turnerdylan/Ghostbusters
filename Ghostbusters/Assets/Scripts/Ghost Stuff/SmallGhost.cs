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
    [SerializeField] public int pointValue = 1;

    public List<Player> players = new List<Player>();
    public float timer = 30f; //scare timer
    private float _timer; //scare timer

    //private variables
    private int _listIndex;
    public float _transformTimer;

    public float fadeDuration = 12.0f;
    private Color currColor;
    bool isFading = false;
    //public variables

    private void Start()
    {
        _transformTimer = _transformTimerMax;
        currColor = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer > timer && !isFading)
        {
            FadeGhost();
        }
    }

    private void OnEnable()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(1, 1, 1, 0.83f);
        _timer = 0f;
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

    void FadeGhost()
    {
        isFading = true;
        StartCoroutine(Fade(0.0f, fadeDuration));
    }
    public IEnumerator Fade(float aValue, float aTime)
    {
        float alpha = GetComponentInChildren<SkinnedMeshRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
            GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
            yield return null;
        }
        isFading = false;
        gameObject.SetActive(false);
    }
}

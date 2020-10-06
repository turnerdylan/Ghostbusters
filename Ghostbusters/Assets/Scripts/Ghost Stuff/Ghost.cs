using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(Rigidbody))]
public class Ghost : MonoBehaviour
{
    public NetController currentNet;
    private float _timeToBreakFree = 5f;
    private float _timer;
    private float _speed = 5f;
    public bool isInNet = false;
    public bool isInTrap = false;
    public bool isInLasso = false;
    private TextMeshPro _stateText;


    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        _stateText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if(isInNet)
        {
            _timer -= Time.deltaTime;
            SetStateText("In net! " +_timer.ToString("F0"));
            if(_timer < 0)
            {
                currentNet.ReleaseGhost(this);
            }
        }
        else if(isInTrap)
        {
            ResetTimer();
        } else if(isInLasso)
        {

        }
    }*/

    public void ResetTimer()
    {
        _timer = _timeToBreakFree;
    }

    public void SetStateText(string input)
    {
        _stateText.text = input;
    }
}

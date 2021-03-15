using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    private BUTTON_PRESS scare;
    public bool scareLoaded;
    public float towerTimer = 15.0f;
    private float _timer;
    public Image timerBar;
    Gradient yGrad, gGrad, bGrad, rGrad;
    ParticleSystem.ColorOverLifetimeModule col1, col2;
    private ParticleSystem[] particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        particles[0].Stop();
        particles[1].Stop();
        _timer = towerTimer;

        col1 = particles[0].colorOverLifetime;
        col2 = particles[1].colorOverLifetime;
        yGrad = new Gradient();
        gGrad = new Gradient();
        bGrad = new Gradient();
        rGrad = new Gradient();
        yGrad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.yellow, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(0.75f, 0.22f), new GradientAlphaKey(0.2f, 0.75f), new GradientAlphaKey(0.0f, 1.0f) } );
        gGrad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.green, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(0.75f, 0.22f), new GradientAlphaKey(0.2f, 0.75f), new GradientAlphaKey(0.0f, 1.0f) } );
        bGrad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.blue, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1.0f, 0.22f), new GradientAlphaKey(0.2f, 0.75f), new GradientAlphaKey(0.0f, 1.0f) } );
        rGrad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1.0f, 0.22f), new GradientAlphaKey(0.2f, 0.75f), new GradientAlphaKey(0.0f, 1.0f) } );
    }

    void Update()
    {
        if(scareLoaded)
        {
            _timer -= Time.deltaTime;
            //timerBar.fillAmount = _timer/towerTimer;
            if(_timer <= 0)
            {
                ResetTower();
            }
        }
    }

    public void ResetTower()
    {
        scareLoaded = false;
        particles[0].Stop();
        particles[1].Stop();
        _timer = towerTimer;
    }

    public void LoadScare(BUTTON_PRESS scareToLoad)
    {
        if(!scareLoaded)
        {
            scare = scareToLoad;
            scareLoaded = true;
            //change color of tower
            switch (scare)
            {
                case BUTTON_PRESS.Up:
                    col1.color = yGrad;
                    col2.color = yGrad;
                    break;
                case BUTTON_PRESS.Down:
                    col1.color = gGrad;
                    col2.color = gGrad;
                    break;
                case BUTTON_PRESS.Left:
                    col1.color = bGrad;
                    col2.color = bGrad;
                    break;
                case BUTTON_PRESS.Right:
                    col1.color = rGrad;
                    col2.color = rGrad;
                    break;
            }
            particles[0].Play();
            particles[1].Play();
        }
        // if(TowerManager.Instance.AllLoaded())
        // {
        //     TowerManager.Instance.FreezeGhosts();
        // }
    }

    
}

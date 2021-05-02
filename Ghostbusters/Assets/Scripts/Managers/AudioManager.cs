using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static AudioManager instance = null;
    public Sound[] sounds;
    void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound sound in sounds)
        {
            sound.source.volume = volume;
        }
    }
    #endregion

    [Range(0,1)]
    public float volume;

    public void Play(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name); //looks for sound in sound array with name
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

}

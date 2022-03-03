using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    private string scene;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    void Start()
    {
        Play("Theme");

        scene = SceneManager.GetActiveScene().name;
        if (scene == "ObjectStolen")
        {
            Pause("Theme");       
        }
       
    }


    public void PlayNoOverlay(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        PauseAll();
        if (!s.source.isPlaying)
        {
            s.source.PlayOneShot(s.source.clip);

        } 
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public bool IsPlaying (string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s.source.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Pause (string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            if(!(s.name == "Theme") && !(s.name == "Magic"))
            {
                s.source.Stop();
            }
            
        }
    }

    /// <summary>
    /// Play Sound and wait for seconds
    /// </summary>
    ///  <param name="audio">The audio to be played</param>
    /// <param name="seconds">How many seconds to wait before audio is played</param>
    /// <returns></returns>
  
    public IEnumerator PlayDelay(string sound, int seconds)
    {
        //Debug.Log("sound: " + sound);
        yield return new WaitForSeconds(seconds);
        Sound s = Array.Find(sounds, item => item.name == sound);
  
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.PlayOneShot(s.source.clip);
    }

    public void Mute (string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.volume = 0;
    }


}

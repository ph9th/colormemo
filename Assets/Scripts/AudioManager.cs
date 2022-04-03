using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    private string scene;

    private void Awake()
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

    private void Start()
    {
        Play("Theme");

        scene = SceneManager.GetActiveScene().name;
        if (scene == "ObjectStolen")
        {
            Pause("Theme");
        }
    }

    /// <summary>Plays only one audio clip, pauses the rest.</summary>
    /// <param name="sound">
    ///   <para>
    /// The sound clip name.</para>
    /// </param>
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

    /// <summary>Plays the specified sound.</summary>
    /// <param name="sound">
    ///   <para>
    /// The sound clip name.</para>
    /// </param>
    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.Play();
    }

    /// <summary>Determines whether the specified sound is playing.</summary>
    /// <param name="sound">
    ///   <para>
    /// The sound clip name.</para>
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified sound is playing; otherwise, <c>false</c>.</returns>
    public bool IsPlaying(string sound)
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

    /// <summary>Pauses the specified sound.</summary>
    /// <param name="sound">
    ///   <para>
    /// The sound clip name.</para>
    /// </param>
    public void Pause(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    /// <summary>Pauses all sounds.</summary>
    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            if ((s.name != "Theme") && (s.name != "Magic"))
            {
                s.source.Stop();
            }
        }
    }


    /// <summary>Plays the sound with a delay.</summary>
    /// <param name="sound">The name of the sound clip.</param>
    /// <param name="seconds">The delay in seconds.</param>
    public IEnumerator PlayDelay(string sound, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Sound s = Array.Find(sounds, item => item.name == sound);

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.PlayOneShot(s.source.clip);
    }

    /// <summary>Mutes the specified sound.</summary>
    /// <param name="sound">
    ///   <para>
    /// The sound clip name.</para>
    /// </param>
    public void Mute(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.volume = 0;
    }
}
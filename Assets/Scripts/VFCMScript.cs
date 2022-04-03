using UnityEngine;

/// <summary>Contains the audio play for the VFCM/objectfound scene.</summary>
public class VFCMScript : MonoBehaviour
{
    /// <summary>Gets or sets which player the VFCM task is assigned to.</summary>
    /// <value>The assigned player.</value>
    public static int vfcmTaskAssign { get; set; }
    // Start is called before the first frame update
    /// <summary>Starts the audio clips for the VFCM task.</summary>
    void Start()
    {
        StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("WitchChallenge", 2));
        //Assign task to a player
        if (vfcmTaskAssign == 0)
        {
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("RedBear", 6));
        }
        else if (vfcmTaskAssign == 1)
        {
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("YellowBear", 6));
        }
        else if (vfcmTaskAssign == 2)
        {
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("BlueBear", 6));
        } 
        StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("WhatColor", 7));
    }

}

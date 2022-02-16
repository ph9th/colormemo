using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFCMScript : MonoBehaviour
{
    public static int vfcmTaskAssign = 0;

    // Start is called before the first frame update
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

        //assign next vfcm task to next player
        if (vfcmTaskAssign < 2)
        {
            vfcmTaskAssign++;
        }
        else
        {
            vfcmTaskAssign = 0;
        }

    }

}

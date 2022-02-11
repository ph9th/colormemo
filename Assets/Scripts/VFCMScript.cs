using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFCMScript : MonoBehaviour
{
    public static int vfcmTaskAssign = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (vfcmTaskAssign < 2)
        {
            vfcmTaskAssign++;
        }
        else
        {
            vfcmTaskAssign = 0;
        }


        FindObjectOfType<AudioManager>().PlayNoOverlay("ObjectFound");


        if (vfcmTaskAssign == 0)
        {
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("RedBear", 5));
        }
        else if (vfcmTaskAssign == 1)
        {
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("YellowBear", 5));
        }
        else if (vfcmTaskAssign == 2)
        {
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("BlueBear", 5));
        }
        
        StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("WhatColor", 6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

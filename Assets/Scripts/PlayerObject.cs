using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject
{
    public string name;
    public int maxLevel;

        public float[] green = new float[3]; //index 0: how many times task color was green, index 1: how many times wrong color input, 2: error rate
        public float[] orange = new float[3];
        public float[] purple = new float[3];


    public PlayerObject(string name)
    {

        this.name = name;
        this.maxLevel = 0;

        for (int i = 0; i < 3; i++)
        {
            this.green[i] = 0;
            this.orange[i] = 0;
            this.purple[i] = 0;
        }
    }


}

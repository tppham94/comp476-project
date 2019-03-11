﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Formation")]

public class Formation : ScriptableObject
{
    public Vector3 []offset_from_lead;
    public bool[] taken; //left there for now. Might use if we want units to go for the nearest marker. otherwise, useless 


   
    public GameObject GenerateMarker(Object original, Vector3 pos, Quaternion rot, Transform parent)
    {
        return Instantiate(original, pos, rot, parent) as GameObject;     
    }
}

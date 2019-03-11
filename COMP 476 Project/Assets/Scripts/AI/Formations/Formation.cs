using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Formation")]

public class Formation : ScriptableObject
{
    public Vector3 []offset_from_lead;
    public bool[] taken;


   
    public GameObject GenerateMarker(Object original, int offset_index, Quaternion rot, Transform parent)
    {
        return Instantiate(original, offset_from_lead[offset_index], rot, parent) as GameObject;     
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Transition", menuName = "AI/Transition")]
public   class Transition:ScriptableObject
{
    public Decision decision;
    public State true_state;
    public State false_state;

}

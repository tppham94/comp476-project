using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool pursuit_target = controller.target.gameObject.activeSelf;
        return pursuit_target;
    }
}

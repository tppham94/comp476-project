using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Decisions/NoTarget")]

public class NoTargetDecision : Decision
{
    public override bool Decide(StateController controller)
    {

        EnemyStateController esc;
        //  if (!(controller.GetType() == typeof(EnemyStateController))) return false;

        esc = controller as EnemyStateController;
       return (esc.target == null&&esc.attack_target==null);
    }
}

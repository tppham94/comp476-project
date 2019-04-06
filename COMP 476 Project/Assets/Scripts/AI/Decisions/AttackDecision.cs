using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Decisions/AttackDecision")]
public class AttackDecision : Decision
{
    //ready to attack when movement is done
    public override bool Decide(StateController controller)
    {
        EnemyStateController esc;

        esc = controller as EnemyStateController;
        if (esc == null) return false;
        if (esc.attack_target == null) return false;
        if (!esc.can_attack) return false;
        return ((esc.target.transform.position - esc.transform.position).magnitude <= esc.enemy_stats.arrival_radius);
        throw new System.NotImplementedException();
    }


}

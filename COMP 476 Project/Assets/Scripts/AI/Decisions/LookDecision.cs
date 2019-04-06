using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {

        EnemyStateController esc;
        //  if (!(controller.GetType() == typeof(EnemyStateController))) return false;

        esc = controller as EnemyStateController;
        bool target_visible = Look(esc);
        return target_visible;
    }

    private bool Look(EnemyStateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * controller.enemy_stats.look_range, Color.green);

        if (Physics.SphereCast(controller.transform.position, controller.enemy_stats.look_sphere_cast_radius, controller.transform.forward, out hit, controller.enemy_stats.look_range)
            && hit.collider.CompareTag("Player"))
        {
            Debug.Log(hit.collider.tag);
            controller.target = hit.transform;
            controller.attack_target = hit.transform;
            return true;
        } else
        {
            return false;
        }
    }
}

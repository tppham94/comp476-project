using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool target_visible = Look(controller);
        return target_visible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * controller.enemy_stats.look_range, Color.green);

        if (Physics.SphereCast(controller.transform.position, controller.enemy_stats.look_sphere_cast_radius, controller.transform.forward, out hit, controller.enemy_stats.look_range)
            && hit.collider.CompareTag("Player"))
        {
            Debug.Log(hit.collider.tag);
            controller.target = hit.transform;
            return true;
        } else
        {
            return false;
        }
    }
}

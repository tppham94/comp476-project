using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KinematicSeek", menuName = "AI/Actions/KinematicSeek")]
public class KinematicSeekAction : Action
{
    public override void Act(StateController controller)
    {
        EnemyStateController esc;
        if (!(controller.GetType() == typeof(EnemyStateController))) return;
        if (controller.target != null)
        {
            esc = controller as EnemyStateController;
            if (esc.obstacle_normal != Vector3.zero) Seek(esc, esc.obstacle_normal);
            else Seek(esc);
        }
    }

    // Seek using Steering mode.
    private void Seek(EnemyStateController controller, Vector3 targetpoint)
    {
        // Check for target dist from arrival radius
        Vector3 direction = targetpoint - controller.transform.position;
        float distance = (direction).magnitude;
        if (controller.current_vel.magnitude == 0)
        {
            controller.current_vel = direction;
            controller.current_vel.Normalize();
            controller.current_vel *= controller.enemy_stats.maximum_velocity;
        }
        else
        {
            controller.current_vel = direction.normalized * controller.current_vel.magnitude;
        }

        Vector3 newpos = controller.transform.position + (controller.current_vel * Time.deltaTime);
        controller.transform.position = newpos;
    }
    private void Seek(EnemyStateController controller)
    {
        Seek(controller, controller.target.position);
    }
}

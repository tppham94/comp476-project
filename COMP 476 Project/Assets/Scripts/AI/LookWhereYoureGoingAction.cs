using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LookWhereYerGoin", menuName = "AI/Actions/LookAction")]

public class LookWhereYoureGoingAction : Action
{

    public override void Act(StateController controller)
    {
        EnemyStateController esc;
        if (!(controller.GetType() == typeof(EnemyStateController))) return;
        if (controller.target != null)
        {
            esc = controller as EnemyStateController;
            LookWhereYoureGoing(esc);
        }
    }
    private void LookWhereYoureGoing(EnemyStateController controller)
    {
        Vector3 direction = controller.current_vel.normalized;

        float angle = Vector3.Angle(direction, controller.transform.forward);
        if (angle < controller.enemy_stats.angular_arrival)
        {
            controller.angular_accel = 0;
            controller.angular_vel = 0;
            return;
        }
        controller.goal_angular_vel = (controller.enemy_stats.maximum_angular_velocity) * (angle / controller.enemy_stats.angular_slowdown);

        controller.angular_accel = (controller.goal_angular_vel - controller.angular_vel) / controller.enemy_stats.time_to_target;
        controller.angular_accel = (controller.angular_accel > controller.enemy_stats.maximum_angular_acceleration) ? controller.enemy_stats.maximum_angular_acceleration : controller.angular_accel;

        controller.angular_vel += controller.angular_accel * Time.deltaTime;
        controller.angular_vel = controller.angular_vel > controller.enemy_stats.maximum_angular_velocity ? controller.enemy_stats.maximum_angular_velocity : controller.angular_vel;

        Quaternion align_target = Quaternion.LookRotation(direction, Vector3.up);
        controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, align_target, controller.angular_vel);
    }
}

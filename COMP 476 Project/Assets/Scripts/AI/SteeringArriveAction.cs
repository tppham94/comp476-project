using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrive", menuName = "AI/Actions/ArriveAction")]
public class SteeringArriveAction : Action
{
 

    
    public override void Act(StateController controller)
    {
        EnemyStateController esc;
        if (!(controller.GetType() ==typeof( EnemyStateController))) return;
        if (controller.target != null)
        {
            esc = controller as EnemyStateController;
            Arrive(esc);
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
    private void Arrive(EnemyStateController controller)
    {
        // Check for target dist from arrival radius
        Vector3 direction = controller.target.transform.position - controller.transform.position;
       // Debug.Log(controller.gameObject.name + ": " + controller.current_vel);
        float distance = (direction).magnitude;

        if (distance < controller.enemy_stats.arrival_radius)
        {
            controller.current_vel = Vector3.zero;
            controller.current_accel = Vector3.zero;

            return;
        }
        // Check if within slowdown radius
        if (distance < controller.enemy_stats.slowdown_radius)
        {
            controller.goal_vel = (controller.enemy_stats.maximum_velocity) * (distance / controller.enemy_stats.slowdown_radius) * (direction / distance);

        }
        else
        {
            controller.goal_vel = (controller.enemy_stats.maximum_velocity) * (direction / distance);
        }
        //Set accel
        controller.current_accel = (controller.goal_vel - controller.current_vel) / controller.enemy_stats.time_to_target;
        //cap accel
        if (controller.current_accel.magnitude > controller.enemy_stats.maximum_acceleration)
        {
            controller.current_accel.Normalize();
            controller.current_accel *= controller.enemy_stats.maximum_acceleration;
        }
        controller.current_vel += (controller.current_accel * Time.deltaTime);
        //cap vel
        if (controller.current_vel.magnitude > controller.enemy_stats.maximum_velocity)
        {
            controller.current_vel.Normalize();
            controller.current_vel *= controller.enemy_stats.maximum_velocity;
        }

        Vector3 newpos = controller.transform.position + (controller.current_vel * Time.deltaTime);
        controller.transform.position = newpos;
    }
}


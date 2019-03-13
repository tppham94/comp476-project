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
           
        }
    }

    private void Arrive(EnemyStateController controller)
    {
        Arrive(controller, controller.target.position);
    }
    //for if we want to go for a different point while maintaining target
    private void Arrive(EnemyStateController controller,Vector3 targetpoint)
    {
        // Check for target dist from arrival radius
         Vector3 direction = targetpoint - controller.transform.position;
   // Vector3 direction = controller.nav_agent.steeringTarget - controller.transform.position;
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : StateController
{
    public Vector3 current_vel = Vector3.zero;
    public Vector3 current_accel = Vector3.zero;
    public Vector3 goal_vel = Vector3.zero;
    public float angular_vel = 0;
    public float angular_accel = 0;
    public float goal_angular_vel = 0;

    public Vector3 obstacle_normal = Vector3.zero;
    private void Update()
    {
        
        current_state.UpdateState(this);
    }
    private void OnDrawGizmos()
    {
        if (current_state != null)
        {
            Gizmos.color = current_state.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, enemy_stats.look_sphere_cast_radius);

        }
    }

}

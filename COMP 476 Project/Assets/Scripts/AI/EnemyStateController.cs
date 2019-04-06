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
    public Transform attack_target = null;
    public Vector3 obstacle_normal = Vector3.zero;
    public bool can_attack = true;
    public GameObject homing_missile_prefab;
    private void Update()
    {
        
        current_state.UpdateState(this);
        Debug.DrawRay(transform.position + (transform.rotation * new Vector3(.5f, 0.1f, 0))*transform.localScale.x, Quaternion.Euler(0,20,0)*(transform.forward)*25, Color.green);
        Debug.DrawRay(transform.position + (transform.rotation*new Vector3(-.5f, 0.1f, 0)) * transform.localScale.x, Quaternion.Euler(0, -20, 0) * (transform.forward)*25, Color.green);

    }
    private void OnDrawGizmos()
    {
        if (current_state != null)
        {
            Gizmos.color = current_state.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, enemy_stats.look_sphere_cast_radius);

            Gizmos.DrawWireSphere(obstacle_normal, 4);

        }
    }

}

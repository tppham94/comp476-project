using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : StateController
{
    public Vector3 current_vel = Vector3.zero;
    public Vector3 current_accel = Vector3.zero;
    public Vector3 goal_vel = Vector3.zero;
    public float angular_vel = 0;
    public float angular_accel = 0;
    public float goal_angular_vel = 0;

    private void Update()
    {
        
        current_state.UpdateState(this);
    }
}

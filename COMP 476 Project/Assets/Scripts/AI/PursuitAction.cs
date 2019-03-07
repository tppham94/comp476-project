using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( menuName = "AI/Actions/Pursuit")]
public class PursuitAction : Action
{
    float maximum_velocity = 15.0f;
    float current_rotation_velocity = 0.0f;
    float maximum_rotation_acceleration = 0.01f;
    float maximum_rotation_velocity = 2.0f;
    float current_velocity = 0.05f;
    float maximum_acceleration = 0.05f;
    Vector3 direction;
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        // Steering Seek.
        direction = (controller.target.transform.position - controller.transform.position);
        direction.Normalize();
        current_rotation_velocity = Mathf.Min(current_rotation_velocity + maximum_rotation_acceleration, maximum_rotation_velocity);
        current_velocity = Mathf.Min(current_rotation_velocity + maximum_acceleration, maximum_velocity);
        Quaternion target_rotation = Quaternion.LookRotation(direction);
        controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, target_rotation, current_rotation_velocity * Time.deltaTime);
        Vector3 new_position = controller.transform.position + (current_velocity * Time.deltaTime) * controller.transform.forward.normalized;
        controller.transform.position = new_position;
    }
}

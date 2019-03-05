using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Wander")]
public class WanderAction : Action
{
    float maximum_rotation_velocity = 2.0f;
    //float maximum_velocity = 10.0f;
    float angle;
    public override void Act(StateController controller)
    {
        Wander(controller);
    }

    private void Wander(StateController controller)
    {
        float rotation_direction = Random.Range(-0.5f, 0.5f);
        angle = angle + maximum_rotation_velocity * rotation_direction * Time.deltaTime;
        if(angle > 3.5f)
        {
            angle = 3.5f;
        } else if(angle < -3.5f)
        {
            angle = -3.5f;
        }
        controller.transform.Rotate(0, angle, 0);
        Vector3 next_position = controller.transform.position + controller.enemy_stats.maximum_velocity * Time.deltaTime * controller.transform.forward.normalized;
        controller.transform.position = next_position;
        
    }
}

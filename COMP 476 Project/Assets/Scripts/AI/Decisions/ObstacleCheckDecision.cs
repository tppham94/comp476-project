using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/Decisions/ObstacleCheck")]
public class ObstacleCheckDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        EnemyStateController esc;
        //  if (!(controller.GetType() == typeof(EnemyStateController))) return false;

        esc = controller as EnemyStateController;
        return (CheckObstacle(esc)&&esc.current_vel.magnitude!=0);
    }

    private bool CheckObstacle(EnemyStateController controller)
    {
        int layerMask = 1 << 9;
        Physics.Raycast(controller.transform.position + (controller.transform.rotation * new Vector3(.5f,0,0)) * controller.transform.localScale.x, Quaternion.Euler(0, 20, 0) * controller.transform.forward, out RaycastHit hit, controller.enemy_stats.whisker_length, layerMask, QueryTriggerInteraction.Collide);
        Physics.Raycast(controller.transform.position + (controller.transform.rotation * new Vector3(-.5f,0,0)) * controller.transform.localScale.x, Quaternion.Euler(0, -20, 0) * controller.transform.forward, out RaycastHit hit1, controller.enemy_stats.whisker_length, layerMask, QueryTriggerInteraction.Collide);
        // Physics.Raycast(controller.transform.position, controller.transform.forward, out RaycastHit hit, controller.enemy_stats.whisker_length, LayerMask.GetMask("Obstacle"), QueryTriggerInteraction.Collide);
        if (hit.transform != null)
        {
            controller.obstacle_normal = hit.point + (controller.enemy_stats.whisker_length * new Vector3(hit.normal.x, controller.transform.position.y, hit.normal.z).normalized) ;
            return true;
        }
        if (hit1.transform != null)
        {
            controller.obstacle_normal = hit1.point + (controller.enemy_stats.whisker_length * new Vector3(hit1.normal.x,controller.transform.position.y,hit1.normal.z).normalized) ;
            return true;
        }
        return false;
    }
}

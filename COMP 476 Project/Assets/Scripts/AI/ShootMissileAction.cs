using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShootMissile", menuName = "AI/Actions/ShootMissile")]

public class ShootMissileAction : Action
{
    public override void Act(StateController controller)
    {

        EnemyStateController esc;
        if (!(controller.GetType() == typeof(EnemyStateController))) return;
        if (controller.target != null)
        {
            esc = controller as EnemyStateController;
            if (esc.enemy_stats.homing_missile_prefab != null && esc.current_state.StateName.Equals("Arrive") || esc.current_state.StateName.Equals("Seek"))
            {
                if (esc.shoot_flag)
                {
                    //SET TO PHOTONNETWORKINSTANTIATE
                    GameObject missile = Instantiate(esc.enemy_stats.homing_missile_prefab, esc.transform.position + (esc.transform.forward) * 2f, esc.transform.rotation) as GameObject;
                    EnemyStateController miss_script = missile.GetComponent<EnemyStateController>();
                    if (miss_script != null)
                    {
                        //miss_script.attack_target = esc.attack_target;
                        miss_script.target = esc.attack_target;
                        if (esc.attack_target == null) miss_script.target = esc.target;
                        miss_script.current_vel = 3 * esc.current_vel;
                    }
                    esc.shoot_flag = false;
                    esc.shoot_timer = esc.enemy_stats.homing_missile_interval;
                }
                /* else if (esc.attack_target != null && esc.current_state.StateName.Equals("Attack")) Look(esc,
                     (esc.attack_target.transform.position - esc.transform.position).normalized);
         */
            }
        }
    }
  
}

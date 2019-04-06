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
            if (esc.homing_missile_prefab != null && esc.current_state.StateName.Equals("Arrive") || esc.current_state.StateName.Equals("Seek"))
            {
               GameObject missile = Instantiate(esc.homing_missile_prefab,esc.transform.position+(esc.transform.forward)*2f,esc.transform.rotation) as GameObject;
                
            }
           /* else if (esc.attack_target != null && esc.current_state.StateName.Equals("Attack")) Look(esc,
                (esc.attack_target.transform.position - esc.transform.position).normalized);
    */   
    }
    }

  
}

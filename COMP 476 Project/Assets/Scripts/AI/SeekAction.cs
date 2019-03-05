using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Seek")]
public class SeekAction : Action
{
    Vector3 direction;
    public override void Act(StateController controller)
    {
        Seek(controller);
    }

    // Seek using Steering mode.
    private void Seek(StateController controller)
    {
        direction = controller.target.position - controller.transform.position;
        direction.Normalize();
    }
}

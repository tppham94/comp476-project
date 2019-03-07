using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.gray;

    public void UpdateState(StateController controller)
    {
        DoAction(controller);
        CheckTransition(controller);
    }
    
    private void DoAction(StateController controller)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransition(StateController controller)
    {
        for(int i = 0; i < transitions.Length; i++)
        {
            bool decision_success = transitions[i].decision.Decide(controller);
            if(decision_success)
            {
                controller.TransitionToState(transitions[i].true_state);
            } else
            {
                controller.TransitionToState(transitions[i].false_state);
            }
        }
    }
}

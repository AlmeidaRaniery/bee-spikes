using System;
using UnityEngine;
public class TitleScreenBehaviour : StateMachineBehaviour
{
     //Whenever we enter or exit the Title screen, call the appropriate events.
    //All callbacks to these events are defined on the FlowController class
    public event Action onEnteredTitle;
    public event Action onExitTitle;
    public event Action OnAnyTouch;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onEnteredTitle?.Invoke();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //If the player touches the screen or hits the "Jump" button, invoke an event
        if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
            OnAnyTouch?.Invoke();       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onExitTitle?.Invoke();
    }
}

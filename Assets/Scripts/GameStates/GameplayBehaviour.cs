using System;
using UnityEngine;

public class GameplayBehaviour : StateMachineBehaviour
{
    //Whenever we enter or exit the Gameplay screen, call the appropriate events.
    //All callbacks to these events are defined on the FlowController class

    public event Action onGameOver;
    public event Action onGameStarted;
    public event Action onAnyTouch;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onGameStarted?.Invoke();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if the player touches the screen or presses the "Jump" buton, invoke an event
        if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
            onAnyTouch?.Invoke();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onGameOver?.Invoke();
    }
}

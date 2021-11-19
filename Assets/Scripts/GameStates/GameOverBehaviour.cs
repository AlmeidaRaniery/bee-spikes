using System;
using UnityEngine;

public class GameOverBehaviour : StateMachineBehaviour
{
    //Whenever we enter or exit the Game Over screen, call the appropriate events.
    //All callbacks to these events are defined on the FlowController class
    public event Action onGameEnded;
    public event Action onRestart;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onGameEnded?.Invoke();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onRestart?.Invoke();
    }

}

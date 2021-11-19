using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class controls the flow between title/gameplay/game over screens by
// keeping track of the animator atached to this gameObject and assigning
// the relevant callbacks to each state's StateMachineBehaviour
public class FlowController : MonoBehaviour
{
    [Header("UI Scripts")]
    public StartMenuBehaviour startMenu;
    public GameUIBehaviour gameUI;
    public GameOverMenuBehaviour gameOverScreen;
    [Header("Gameplay Elements")]
    public PlayerBehaviour playerBehaviour;
    public SpikesController spikesController;
    public PickupBehaviour pickupBehaviour;


    private Animator stateMachine;
    private TitleScreenBehaviour titleScreenBehaviour;
    private GameplayBehaviour    gameplayBehaviour;
    private GameOverBehaviour    gameOverBehaviour;
    void Start() 
    {
        //Assign the Animator attached to this script's gameObject, then get the 
        //StateMachineBehaviour scripts associated with each game state (Title Screen, Gameplay, Game Over).

        stateMachine = GetComponent<Animator>();

        titleScreenBehaviour = stateMachine.GetBehaviour<TitleScreenBehaviour>();
        gameOverBehaviour    = stateMachine.GetBehaviour<GameOverBehaviour>();
        gameplayBehaviour    = stateMachine.GetBehaviour<GameplayBehaviour>();

        //Assign function callbacks to title screen events
        titleScreenBehaviour.OnAnyTouch     += StartGame;
        titleScreenBehaviour.onEnteredTitle += gameOverScreen.CloseMenu;
        titleScreenBehaviour.onEnteredTitle += startMenu.OpenMenu;
        titleScreenBehaviour.onEnteredTitle += playerBehaviour.ResetPlayer;
        titleScreenBehaviour.onEnteredTitle += spikesController.UnsetSpikes;

        //Assign function callbacks to gameplay events
        gameplayBehaviour.onGameStarted += playerBehaviour.StartPlayer;
        gameplayBehaviour.onGameStarted += gameUI.StartUI;
        gameplayBehaviour.onGameStarted += startMenu.CloseMenu;
        gameplayBehaviour.onGameStarted += spikesController.StartSpikes;

         //Assign function callbacks to game over events
        gameOverBehaviour.onGameEnded += gameOverScreen.OpenMenu;
        gameOverBehaviour.onGameEnded += gameUI.HideUI;
        gameOverBehaviour.onGameEnded += pickupBehaviour.HidePickup;
    }

    //StartGame, EndGame and Quit are public callbacks that will be called by UnityEvents 
    //whenever the game is supposed to change state.
    public void StartGame()
    {
        stateMachine.SetTrigger("gameStart");
    }

    public void EndGame()
    {
        stateMachine.SetTrigger("gameOver");
    }

    public void Quit()
    {
        stateMachine.SetTrigger("quitToMenu");
    }
}

using UnityEngine;
using System.Collections;
using LeeWayner.FSM;

public class GameStateController 
{
	private FSM gameFsm;

    private FSMState preStartState;
	private FSMState idleState;
	private FSMState playState;
    private FSMState pauseState;
	private FSMState gameOverState;

	private PreStartAction preStartAction;
	private IdleAction idleAction;
    private PlayAction playingAction;
    private PauseAction pauseAction;
	private GameOverAction gameOverAction;
    public GameState CurrentState { get { return (GameState)gameFsm.GetCurrentState(); } }
    public GameState PreviousState { get { return (GameState)gameFsm.GetPreviousState(); } }
    
    public GameStateController()
    {
        gameFsm = new FSM("GameFSM");
        preStartState = gameFsm.AddState((byte)GameState.PreStart);
		idleState = gameFsm.AddState((byte)GameState.Idle);
		playState = gameFsm.AddState((byte)GameState.Play);
        pauseState = gameFsm.AddState((byte)GameState.Pause);
		gameOverState = gameFsm.AddState((byte)GameState.GameOver);

		preStartAction = new PreStartAction(preStartState);
        playingAction = new PlayAction(playState);
        pauseAction = new PauseAction(pauseState);
		gameOverAction = new GameOverAction(gameOverState);
		idleAction = new IdleAction(idleState);

        preStartState.AddTransition((byte)FSMTransition.ToIdle, idleState);
		idleState.AddTransition((byte)FSMTransition.ToPlay, playState);
        playState.AddTransition((byte)FSMTransition.ToPause, pauseState);
		playState.AddTransition((byte)FSMTransition.ToGameOver, gameOverState);
		gameOverState.AddTransition((byte)FSMTransition.ToIdle, idleState);

		preStartAction.Init();
		idleAction.Init();
        playingAction.Init();
        pauseAction.Init();
		gameOverAction.Init();
    }
    
    public void Start()
    {
        gameFsm.Start((byte)GameState.PreStart);
    }

    public void Update()
    {
        gameFsm.Update();
    }

    public void ChangeState(GameState newState)
    {
#if UNITY_EDITOR
        //Debug.Log("Change to State: " + newState);
#endif
        switch (newState)
        {
            case GameState.PreStart:
                gameFsm.ChangeToState(preStartState);
                break;
			case GameState.Idle:
				gameFsm.ChangeToState(idleState);
				break;
            case GameState.Play:
                gameFsm.ChangeToState(playState);
                break;
            case GameState.Pause:
                gameFsm.ChangeToState(pauseState);
                break;
			case GameState.GameOver:
				gameFsm.ChangeToState(gameOverState);
				break;
		}
    }
}

public enum FSMTransition : byte
{
	None = 0,
	ToPlay = 1,
	ToPause = 2,
	ToGameOver = 3,
	ToIdle = 4
}
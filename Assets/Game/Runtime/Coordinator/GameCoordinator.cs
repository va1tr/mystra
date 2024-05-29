using UnityEngine;
using UnityEngine.SceneManagement;

using Golem;

namespace Mystra
{
	internal sealed class GameCoordinator : Singleton<GameCoordinator>
	{
        [SerializeField]
        private GameStateObject m_Menu;

        [SerializeField]
        private GameStateObject m_Overworld;

        [SerializeField]
        private GameStateObject m_Battle;

        private StateMachine<GameState> m_StateMachine = new StateMachine<GameState>();

        protected override void Awake()
        {
            base.Awake();

            CreateStartupGameStates();
        }

        private void CreateStartupGameStates()
        {
            var states = new IState<GameState>[]
            {
                new GameMenuState<GameState>(GameState.Menu, m_Menu),
                new GameOverworldState<GameState>(GameState.Overworld, m_Overworld),
                new GameBattleState<GameState>(GameState.Battle, m_Battle)
            };

            m_StateMachine.AddStatesToStateMachine(states);
        }

        private void Start()
        {
            SetStateMachineID();
            StartStateMachine();
        }

        private void SetStateMachineID()
        {
            m_StateMachine.SetCurrentStateID(GameState.Menu);
        }

        private void StartStateMachine()
        {
            m_StateMachine.Start();
        }

        internal void ChangeState(GameState stateToTransitionInto)
        {
            m_StateMachine.ChangeState(stateToTransitionInto);            
        }

        public void ChangeToOverworldState()
        {
            ChangeState(GameState.Overworld);
        }

        public void ChangeToBattleState()
        {
            ChangeState(GameState.Battle);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}

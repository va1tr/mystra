using System;
using System.Collections;

using UnityEngine;

using Eevee;
using Golem;
using Slowbro;

namespace Mystra
{
	internal sealed class BattleCoordinator : Singleton<BattleCoordinator>
	{
        [SerializeField]
        private BattleGraphicsInterface m_Interface;

        [SerializeField]
        private RandomEncounterSpawner m_Spawner;

        [SerializeField]
        private MoveRuntimeSet m_RuntimeSet;

        [SerializeField]
        private PlayerRuntimeSet m_PlayerSet;

        [SerializeField]
        private Combatant m_Player;

        [SerializeField]
        private Combatant m_Enemy;

		private readonly StateMachine<BattleState> m_StateMachine = new StateMachine<BattleState>();

        protected override void Awake()
        {
            base.Awake();

            CreateStartupBattleStates();
        }

        private void CreateStartupBattleStates()
        {
            var states = new IState<BattleState>[]
            {
                new BattleBeginState<BattleState>(BattleState.Begin, this),
                new BattleWaitState<BattleState>(BattleState.Wait, this),
                new BattleActionState<BattleState>(BattleState.Action, this),
                new BattleWonState<BattleState>(BattleState.Won, this),
                new BattleLostState<BattleState>(BattleState.Lost, this)
            };

            m_StateMachine.AddStatesToStateMachine(states);
        }

        private void OnEnable()
        {
            SetStateMachineID();
            StartStateMachine();
        }

        private void SetStateMachineID()
        {
            m_StateMachine.SetCurrentStateID(BattleState.Begin);
        }

        private void StartStateMachine()
        {
            m_StateMachine.Start();
        }

        internal void ChangeState(BattleState stateToTransitionInto)
        {
            m_StateMachine.ChangeState(stateToTransitionInto);
        }

        internal void Stop()
        {
            m_StateMachine.Stop();
        }

        internal void SpawnRandomEncounterFromSet()
        {
            m_Spawner.SetRandomEncounterFromSet();
        }

        internal void SetPlayer()
        {
            m_Player.psychic = m_PlayerSet.GetPlayerPsychic();
        }

        internal BattleGraphicsInterface GetGraphicsInterface()
        {
            return m_Interface;
        }

        internal Combatant GetPlayer()
        {
            return m_Player;
        }

        internal Combatant GetEnemy()
        {
            return m_Enemy;
        }

        internal MoveRuntimeSet GetMoveRuntimeSet()
        {
            return m_RuntimeSet;
        }

        internal PlayerRuntimeSet GetPlayerSet()
        {
            return m_PlayerSet;
        }
    }

    internal sealed class BattleBeginState<T> : State<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private readonly BattleCoordinator m_Coordinator;

        private readonly Combatant m_Player;
        private readonly Combatant m_Enemy;

        private readonly BattleGraphicsInterface m_Interface;

        internal BattleBeginState(T uniqueId, BattleCoordinator coordinator) : base(uniqueId)
        {
            m_Coordinator = coordinator;

            m_Player = m_Coordinator.GetPlayer();
            m_Enemy = m_Coordinator.GetEnemy();

            m_Interface = m_Coordinator.GetGraphicsInterface();
        }

        public override void Enter()
        {
            Debug.Log("entered battle begin state");

            m_Coordinator.SetPlayer();
            m_Coordinator.SpawnRandomEncounterFromSet();

            m_Interface.SetEnemyProperties(m_Enemy);
            m_Interface.SetPlayerProperties(m_Player);

            m_Coordinator.StartCoroutine(BattleBeginSequence());
        }

        private IEnumerator BattleBeginSequence()
        {
            yield return m_Enemy.image.material.Alpha(0f, 0.01f, EasingType.Linear);
            yield return m_Player.image.material.Alpha(0f, 0.01f, EasingType.Linear);

            yield return new Parallel(m_Enemy.image.material.Alpha(1f, 0.5f, EasingType.Linear),
                m_Player.image.material.Alpha(1f, 0.5f, EasingType.Linear));

            m_Coordinator.ChangeState(BattleState.Wait);
        }
    }

    internal sealed class BattleWaitState<T> : State<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private readonly BattleCoordinator m_Coordinator;

        private readonly Combatant m_Player;
        private readonly Combatant m_Enemy;

        private readonly MoveRuntimeSet m_RuntimeSet;
        private readonly BattleGraphicsInterface m_Interface;

        private IEnumerator m_IdleAnimationWhileWaitingAsync;

        internal BattleWaitState(T uniqueId, BattleCoordinator coordinator) : base(uniqueId)
        {
            m_Coordinator = coordinator;

            m_Player = m_Coordinator.GetPlayer();
            m_Enemy = m_Coordinator.GetEnemy();

            m_RuntimeSet = m_Coordinator.GetMoveRuntimeSet();
            m_Interface = m_Coordinator.GetGraphicsInterface();

            m_IdleAnimationWhileWaitingAsync = IdleAnimationSequenceWhileWaiting();
        }

        public override void Enter()
        {
            Debug.Log("entered battle wait state");

            m_Coordinator.StartCoroutine(BattleGraphicsInterface.ShowAsync<MovesMenu>());

            EventSystem.instance.AddListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);

            m_Coordinator.StartCoroutine(m_IdleAnimationWhileWaitingAsync);
        }

        private IEnumerator IdleAnimationSequenceWhileWaiting()
        {
            while (true)
            {
                yield return new Parallel(PlayerIdleSequence(),
                    EnemyIdleSequence());
            }
        }

        private IEnumerator PlayerIdleSequence()
        {
            yield return m_Player.image.Animate(m_Player.psychic.asset.sprites, 4f);
        }

        private IEnumerator EnemyIdleSequence()
        {
            yield return m_Enemy.image.Animate(m_Enemy.psychic.asset.sprites, 4f);
        }

        private void OnAbilityButtonClicked(AbilityButtonClickedEventArgs args)
        {
            m_RuntimeSet.AddFightMoveToRuntimeSet(m_Player, m_Enemy, args.ability);

            m_RuntimeSet.AddFightMoveToRuntimeSet(m_Enemy, m_Player, m_Enemy.psychic.GetRandomAbility());

            m_Coordinator.ChangeState(BattleState.Action);
        }

        public override void Exit()
        {
            m_Coordinator.StartCoroutine(BattleGraphicsInterface.HideAsync<MovesMenu>());
            m_Coordinator.StartCoroutine(BattleGraphicsInterface.HideAsync<AbilitiesMenu>());

            EventSystem.instance.RemoveListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);

            m_Coordinator.StopCoroutine(m_IdleAnimationWhileWaitingAsync);
        }
    }

    internal sealed class BattleActionState<T> : State<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private readonly BattleCoordinator m_Coordinator;

        private readonly MoveRuntimeSet m_RuntimeSet;
        private readonly BattleGraphicsInterface m_Interface;

        internal BattleActionState(T uniqueId, BattleCoordinator coordinator) : base(uniqueId)
        {
            m_Coordinator = coordinator;

            m_RuntimeSet = m_Coordinator.GetMoveRuntimeSet();
            m_Interface = m_Coordinator.GetGraphicsInterface();
        }

        public override void Enter()
        {
            Debug.Log("entered battle action state");
            m_RuntimeSet.Sort();

            m_Coordinator.StartCoroutine(RunMovesBasedOnPriority());
        }

        private System.Collections.IEnumerator RunMovesBasedOnPriority()
        {
            int count = m_RuntimeSet.Count();

            for (int i = 0; i < count; i++)
            {
                yield return m_RuntimeSet[i].Run();

                var target = m_RuntimeSet[i].target;

                if (target.psychic.isDead)
                {
                    switch (target.affinity)
                    {
                        case Affinity.Hostile:
                            m_Coordinator.ChangeState(BattleState.Won);
                            yield break;
                        case Affinity.Friendly:
                            m_Coordinator.ChangeState(BattleState.Lost);
                            yield break;
                    }
                }
            }

            m_Coordinator.ChangeState(BattleState.Wait);
        }

        public override void Exit()
        {
            m_Interface.CleanupTypewriterAndClearText();
            m_RuntimeSet.Clear();
        }
    }

    internal sealed class BattleWonState<T> : State<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private readonly BattleCoordinator m_Coordinator;

        private readonly Combatant m_Player;
        private readonly Combatant m_Enemy;

        private readonly BattleGraphicsInterface m_Interface;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);

        private const float kDelayForHalfSecond = 0.5f;

        internal BattleWonState(T uniqueId, BattleCoordinator coordinator) : base(uniqueId)
        {
            m_Coordinator = coordinator;

            m_Player = m_Coordinator.GetPlayer();
            m_Enemy = m_Coordinator.GetEnemy();

            m_Interface = m_Coordinator.GetGraphicsInterface();
        }

        public override void Enter()
        {
            Debug.Log("entered battle won state");

            m_Coordinator.StartCoroutine(BattleWonEndSequence());
        }

        private IEnumerator BattleWonEndSequence()
        {
            yield return BattleGraphicsInterface.HideAsync<AbilitiesMenu>();
            yield return BattleGraphicsInterface.HideAsync<MovesMenu>();

            yield return FadeOutEnemySprite();

            yield return CalculateExperienceGainAndLevelUp();

            GameCoordinator.instance.ChangeState(GameState.Overworld);
        }

        private IEnumerator FadeOutEnemySprite()
        {
            yield return m_Enemy.image.material.Alpha(0f, 0.5f, EasingType.Linear);
        }

        private IEnumerator CalculateExperienceGainAndLevelUp()
        {
            var player = m_Player.psychic;
            var enemy = m_Enemy.psychic;

            int exp = Mathf.FloorToInt(enemy.asset.experience * enemy.level / 7f * 1f / 1f * 1f * 1f * 1f);

            string message = string.Concat($"{player.name} gained {exp} EXP. Points!");

            yield return m_Interface.TypeTextCharByChar(message);

            yield return m_DelayForHalfSecond;

            int totalExp = player.experience + exp;

            while (player.experience < totalExp)
            {
                player.experience = Mathf.FloorToInt(Mathf.Min(totalExp, Mathf.Pow(player.level + 1, 3)));

                if (player.experience >= Mathf.Pow(player.level + 1, 3))
                {
                    player.LevelUp();

                    string levelUp = string.Concat($"{player.name} leveled up!\n {player.name} is Lv.{player.level}!");

                    yield return m_Interface.TypeTextCharByChar(levelUp);

                    yield return m_DelayForHalfSecond;
                }
            }
        }
    }

    internal sealed class BattleLostState<T> : State<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private readonly BattleCoordinator m_Coordinator;

        private readonly Combatant m_Player;
        private readonly Combatant m_Enemy;

        private readonly BattleGraphicsInterface m_Interface;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);

        private const float kDelayForHalfSecond = 0.5f;

        internal BattleLostState(T uniqueId, BattleCoordinator coordinator) : base(uniqueId)
        {
            m_Coordinator = coordinator;

            m_Player = m_Coordinator.GetPlayer();
            m_Enemy = m_Coordinator.GetEnemy();

            m_Interface = m_Coordinator.GetGraphicsInterface();
        }

        public override void Enter()
        {
            m_Coordinator.StartCoroutine(BattleLostSequence());
        }

        private IEnumerator BattleLostSequence()
        {
            yield return BattleGraphicsInterface.HideAsync<AbilitiesMenu>();
            yield return BattleGraphicsInterface.HideAsync<MovesMenu>();

            yield return FadeOutPlayerSprite();

            string message = string.Concat($"{m_Player.psychic.name} was defeated...");

            yield return m_Interface.TypeTextCharByChar(message);

            yield return m_DelayForHalfSecond;

            BattleCoordinator.instance.GetPlayerSet().Clear();
            GameCoordinator.instance.RestartGame();
        }

        private IEnumerator FadeOutPlayerSprite()
        {
            yield return m_Player.image.material.Alpha(0f, 0.5f, EasingType.Linear);
        }
    }
}

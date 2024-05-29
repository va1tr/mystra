using System;

using UnityEngine;

using Golem;

namespace Mystra
{
    internal sealed class GameBattleState<T> : State<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private readonly GameStateObject m_StateObject;

        internal GameBattleState(T uniqueId, GameStateObject stateObject) : base(uniqueId)
        {
            m_StateObject = stateObject;
        }

        public override void Enter()
        {
            m_StateObject.context.SetActive(true);

            AudioManager.instance.ChangeToBattleMusic();

            UnityEventSystemUtility.SetSelectedGameObject(m_StateObject.navigation);
        }

        public override void Exit()
        {
            BattleCoordinator.instance.Stop();

            m_StateObject.context.SetActive(false);

            AudioManager.instance.ChangeToOverworldMusic();
        }
    }
}

using System;

using UnityEngine;

using Golem;

namespace Mystra
{
    internal sealed class GameOverworldState<T> : State<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private readonly GameStateObject m_StateObject;

        private GameGraphicsInterface m_Interface;

        internal GameOverworldState(T uniqueId, GameStateObject stateObject) : base(uniqueId)
        {
            m_StateObject = stateObject;
        }

        public override void Enter()
        {
            m_StateObject.context.SetActive(true);

            UnityEventSystemUtility.SetSelectedGameObject(m_StateObject.navigation);
        }

        public override void Exit()
        {
            m_StateObject.context.SetActive(false);
        }
    }
}

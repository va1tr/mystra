using System;

using UnityEngine;
using Golem;

namespace Mystra
{
    internal sealed class GameMenuState<T> : State<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        private readonly GameStateObject m_StateObject;

        internal GameMenuState(T uniqueId, GameStateObject stateObject) : base(uniqueId)
        {
            m_StateObject = stateObject;
        }

        public override void Enter()
        {
            m_StateObject.context.SetActive(true);

            UnityEventSystemUtility.SetSelectedGameObject(m_StateObject.navigation);

            GameCoordinator.instance.StartCoroutine(GameGraphicsInterface.ShowAsync<TitleMenu>());
        }

        public override void Exit()
        {
            GameCoordinator.instance.StartCoroutine(GameGraphicsInterface.HideAsync<TitleMenu>());

            m_StateObject.context.SetActive(false);
        }
    }
}

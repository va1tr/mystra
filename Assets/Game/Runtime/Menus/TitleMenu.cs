using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Eevee;
using Slowbro;
using Voltorb;

namespace Mystra
{
	internal sealed class TitleMenu : Menu
	{
        [SerializeField]
        private Combatant m_Player;

        [SerializeField]
        private Combatant m_EnemyOne;

        [SerializeField]
        private Combatant m_EnemyTwo;

        [SerializeField]
        private Combatant m_EnemyThree;

        [SerializeField]
        private RandomEncounterRuntimeSet[] m_RuntimeSets;

        public override IEnumerator Show()
        {
            StartCoroutine(IdleAnimationSequenceWhileWaiting());

            yield return base.Show();
        }

        private IEnumerator IdleAnimationSequenceWhileWaiting()
        {
            while (true)
            {
                yield return new Parallel(PlayerIdleSequence(),
                    EnemyOneIdleSequence(), EnemyTwoIdleSequence(), EnemyThreeIdleSequence());
            }
        }

        private IEnumerator PlayerIdleSequence()
        {
            yield return m_Player.image.Animate(m_Player.psychic.asset.sprites, 4f);
        }

        private IEnumerator EnemyOneIdleSequence()
        {
            yield return m_EnemyOne.image.Animate(m_EnemyOne.psychic.asset.sprites, 4f);
        }

        private IEnumerator EnemyTwoIdleSequence()
        {
            yield return m_EnemyTwo.image.Animate(m_EnemyTwo.psychic.asset.sprites, 4f);
        }

        private IEnumerator EnemyThreeIdleSequence()
        {
            yield return m_EnemyThree.image.Animate(m_EnemyThree.psychic.asset.sprites, 4f);
        }

        public override IEnumerator Hide()
        {
            StopCoroutine(IdleAnimationSequenceWhileWaiting());

            yield return base.Hide();
        }

        public void OnStartButtonClicked(Text text)
        {
            StartCoroutine(FlashButtonOnPress(text));
        }

        private IEnumerator FlashButtonOnPress(Text text)
        {
            for (int i = 0; i < 6; i++)
            {
                yield return text.material.Flash(0f, 0.25f, EasingType.PingPong);
            }

            GameCoordinator.instance.ChangeState(GameState.Overworld);
        }
    }
}

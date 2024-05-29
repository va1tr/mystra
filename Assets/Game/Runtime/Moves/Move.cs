using System;
using System.Collections;

using UnityEngine;

using Eevee;

namespace Mystra
{
    internal abstract class Move : IComparable<Move>
    {
        internal Combatant instigator;
        internal Combatant target;

        protected const int kInstigatorHasPriority = -1;
        protected const int kInstigatorDoesNotHavePriority = 1;

        internal Move(Combatant instigator, Combatant target)
        {
            this.instigator = instigator;
            this.target = target;
        }

        internal abstract IEnumerator Run();

        public abstract int CompareTo(Move other);
    }

    internal sealed class Fight : Move
    {
        private readonly AbilitySpec m_AbilitySpec;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);
        private readonly WaitForSeconds m_DelayForOneSecond = new WaitForSeconds(kDelayForOneSecond);

        private const float kDelayForHalfSecond = 0.5f;
        private const float kDelayForOneSecond = 1f;

        internal Fight(Combatant instigator, Combatant target, AbilitySpec abilitySpec) : base(instigator, target)
        {
            m_AbilitySpec = abilitySpec;
        }

        internal override IEnumerator Run()
        {
            m_AbilitySpec.PreAbilityActivate(instigator, target, out SpecResult result);

            yield return TypeSpecResultMessagesCharByCharWithHalfSecondDelay(result);

            if (result.success)
            {
                yield return m_AbilitySpec.ActivateAbility(instigator, target);

                yield return m_DelayForHalfSecond;
            }

            m_AbilitySpec.PostAbilityActivate(instigator, target, out result);

            yield return TypeSpecResultMessagesCharByCharWithHalfSecondDelay(result);

            yield return m_DelayForHalfSecond;
        }

        private IEnumerator TypeSpecResultMessagesCharByCharWithHalfSecondDelay(SpecResult result)
        {
            var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messages.Length; i++)
            {
                yield return BattleCoordinator.instance.GetGraphicsInterface().TypeTextCharByChar(messages[i]);

                yield return m_DelayForHalfSecond;
            }
        }

        private IEnumerator TypeSpecResultMessagesCharByCharWithOneSecondDelay(SpecResult result)
        {
            var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messages.Length; i++)
            {
                yield return BattleCoordinator.instance.GetGraphicsInterface().TypeTextCharByChar(messages[i]);

                yield return m_DelayForOneSecond;
            }
        }

        public override int CompareTo(Move other)
        {
            if (instigator.psychic.speed.value > other.instigator.psychic.speed.value)
            {
                return kInstigatorHasPriority;
            }
            else if (instigator.psychic.speed.value == other.instigator.psychic.speed.value)
            {
                if (UnityEngine.Random.value > 0.5f)
                {
                    return kInstigatorHasPriority;
                }
                else
                {
                    return kInstigatorDoesNotHavePriority;
                }
            }
            else
            {
                return kInstigatorDoesNotHavePriority;
            }
        }
    }
}

using UnityEngine;

using Eevee;
using Slowbro;

namespace Mystra
{
    [CreateAssetMenu(fileName = "new-physical-ability", menuName = "ScriptableObjects/Mystra/Abilities/Physical", order = 150)]
    internal sealed class Physical : ScriptableAbility
    {
        public override AbilitySpec CreateAbilitySpec()
        {
            return new PhysicalAbilitySpec(this);
        }

        private sealed class PhysicalAbilitySpec : AbilitySpec
        {
            internal PhysicalAbilitySpec(ScriptableAbility asset) : base(asset)
            {

            }

            public override void PreAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PreAbilityActivate(instigator, target, out result);

                if (result.success)
                {
                    m_Effect.PreApplyEffectSpec(instigator, target, ref result);
                }
            }

            public override System.Collections.IEnumerator ActivateAbility(Combatant instigator, Combatant target)
            {
                var position = instigator.rectTransform.anchoredPosition;
                var offset = target.rectTransform.anchoredPosition;

                var normalized = (offset - position).normalized.x;

                yield return instigator.rectTransform.Translate(position, Vector3.left * 12f * normalized, 0.175f, Space.Self, EasingType.PingPong);

                for (int i = 0; i < 2; i++)
                {
                    yield return target.rectTransform.Translate(offset, Vector3.left * 2f, 0.05f, Space.Self, EasingType.PingPong);
                    yield return target.rectTransform.Translate(offset, Vector3.right * 2f, 0.05f, Space.Self, EasingType.PingPong);
                }

                yield return m_Effect.ApplyEffectSpec(instigator, target);
            }

            public override void PostAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PostAbilityActivate(instigator, target, out result);

                if (result.success)
                {
                    m_Effect.PostApplyEffectSpec(instigator, target, ref result);
                }
            }
        }
    }
}

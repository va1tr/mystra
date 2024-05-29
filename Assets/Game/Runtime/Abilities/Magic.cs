using UnityEngine;

using Eevee;
using Slowbro;

namespace Mystra
{
    [CreateAssetMenu(fileName = "magic", menuName = "ScriptableObjects/Mystra/Abilities/Magic", order = 150)]
    internal sealed class Magic : ScriptableAbility
	{
        [SerializeField]
        private Sprite[] m_Sprites;

        public override AbilitySpec CreateAbilitySpec()
        {
            return new MagicAbilitySpec(m_Sprites, this);
        }

        private sealed class MagicAbilitySpec : AbilitySpec
        {
            private readonly Sprite[] m_Sprites;

            internal MagicAbilitySpec(Sprite[] sprites, ScriptableAbility asset) : base(asset)
            {
                m_Sprites = sprites;
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
                var offset = target.rectTransform.anchoredPosition;

                yield return instigator.image.Animate(m_Sprites, 4);

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

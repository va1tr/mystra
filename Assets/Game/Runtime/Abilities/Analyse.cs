using UnityEngine;

using Eevee;
using Slowbro;

namespace Mystra
{
    [CreateAssetMenu(fileName = "analyse", menuName = "ScriptableObjects/Mystra/Abilities/Analyse", order = 150)]
    internal sealed class Analyse : ScriptableAbility
    {
        [SerializeField]
        private Sprite[] m_Sprites;

        public override AbilitySpec CreateAbilitySpec()
        {
            return new AnalyseAbilitySpec(m_Sprites, this);
        }

        private sealed class AnalyseAbilitySpec : AbilitySpec
        {
            private readonly Sprite[] m_Sprites;

            internal AnalyseAbilitySpec(Sprite[] sprites, ScriptableAbility asset) : base(asset)
            {
                m_Sprites = sprites;
            }

            public override void PreAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PreAbilityActivate(instigator, target, out result);

                if (result.success)
                {
                    result.success = false;

                    if (target.psychic.trait.TryUnlockingTrait(asset))
                    {
                        result.success = true;
                    }
                }
            }

            public override System.Collections.IEnumerator ActivateAbility(Combatant instigator, Combatant target)
            {
                yield return instigator.image.Animate(m_Sprites, 4);

                var position = target.rectTransform.anchoredPosition;

                for (int i = 0; i < 2; i++)
                {
                    yield return target.rectTransform.Translate(position, Vector3.left * 2f, 0.05f, Space.Self, EasingType.PingPong);
                    yield return target.rectTransform.Translate(position, Vector3.right * 2f, 0.05f, Space.Self, EasingType.PingPong);
                }

                yield return m_Effect.ApplyEffectSpec(instigator, target);
            }

            public override void PostAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PostAbilityActivate(instigator, target, out result);

                result.success = false;

                if (target.psychic.trait.isConditionMet)
                {
                    result.success = true;
                    result.message += string.Concat($"{instigator.psychic.name} discovered {target.psychic.name} trait {target.psychic.trait.name}!");

                    m_Effect.PostApplyEffectSpec(instigator, target, ref result);
                }
                else
                {
                    result.message += string.Concat($"{target.psychic.name} has no unlockable traits!");
                }
            }
        }
    }
}

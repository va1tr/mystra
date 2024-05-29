using UnityEngine;

using Eevee;
using Slowbro;

namespace Mystra
{
    [CreateAssetMenu(fileName = "new-status-effect", menuName = "ScriptableObjects/Mystra/Effects/Status", order = 150)]
    internal sealed class Status : ScriptableEffect
    {
        public override EffectSpec CreateEffectSpec(ScriptableAbility asset)
        {
            return new StatusEffectSpec(asset);
        }

        private sealed class StatusEffectSpec : EffectSpec
        {
            private string m_Message;

            internal StatusEffectSpec(ScriptableAbility asset) : base(asset)
            {

            }

            public override void PreApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
            {
                base.PreApplyEffectSpec(instigator, target, ref result);

                if (result.success)
                {
                    var modifiers = this.asset.container.modifiers;

                    m_Message = string.Empty;

                    for (int i = 0; i < modifiers.Length; i++)
                    {
                        var modifier = modifiers[i];

                        switch (modifier.target)
                        {
                            case EffectModifierType.Target:
                                ApplyEffectSpecToPsychic(target.psychic, modifier);
                                break;
                            case EffectModifierType.Self:
                                ApplyEffectSpecToPsychic(instigator.psychic, modifier);
                                break;
                        }
                    }
                }
            }

            private void ApplyEffectSpecToPsychic(PsychicSpec psychic, EffectModifiers modifiers)
            {
                if (psychic.TryGetStatByType(modifiers.stat, out Statistic statistic))
                {
                    int initialValue = statistic.stage;
                    statistic.stage = modifiers.multiplier;
                    int currentValue = statistic.stage;

                    if (initialValue > currentValue)
                    {
                        m_Message += string.Concat($"{psychic.name} {modifiers.stat} fell!");
                    }
                    else if (initialValue < currentValue)
                    {
                        m_Message += string.Concat($"{psychic.name} {modifiers.stat} fell!");
                    }
                    else if(modifiers.multiplier < 0 && initialValue == currentValue)
                    {
                        m_Message += string.Concat($"{psychic.name} {modifiers.stat} wont go any lower!");
                    }
                    else if (modifiers.multiplier > 0 && initialValue == currentValue)
                    {
                        m_Message += string.Concat($"{psychic.name} {modifiers.stat} wont go any higher!");
                    }
                }
            }

            public override System.Collections.IEnumerator ApplyEffectSpec(Combatant instigator, Combatant target)
            {
                var position = instigator.rectTransform.anchoredPosition;
                var offset = target.rectTransform.anchoredPosition;

                var normalized = (offset - position).normalized.x;

                for (int i = 0; i < 2; i++)
                {
                    yield return target.rectTransform.Translate(offset, Vector2.left * 4f * normalized, 0.5f, Space.Self, EasingType.PingPong);
                }
            }

            public override void PostApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
            {
                base.PostApplyEffectSpec(instigator, target, ref result);

                if (result.success)
                {
                    result.message += m_Message;
                }
            }
        }
    }
}

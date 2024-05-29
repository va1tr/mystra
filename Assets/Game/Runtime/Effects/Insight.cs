using UnityEngine;

using Eevee;

namespace Mystra
{
    [CreateAssetMenu(fileName = "new-insight-effect", menuName = "ScriptableObjects/Mystra/Effects/Insight", order = 150)]
    internal sealed class Insight : ScriptableEffect
    {
        public override EffectSpec CreateEffectSpec(ScriptableAbility asset)
        {
            return new InsightEffectSpec(asset);
        }

        private sealed class InsightEffectSpec : EffectSpec
        {
            internal InsightEffectSpec(ScriptableAbility asset) : base(asset)
            {

            }

            public override System.Collections.IEnumerator ApplyEffectSpec(Combatant instigator, Combatant target)
            {
                yield break; 
            }

            public override void PostApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
            {
                base.PostApplyEffectSpec(instigator, target, ref result);

                if (result.success)
                {
                    BattleCoordinator.instance.GetGraphicsInterface().SetEnemyProperties(target);
                }
            }
        }
    }
}

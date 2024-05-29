using UnityEngine;

using Eevee;
using Slowbro;

namespace Mystra
{
    [CreateAssetMenu(fileName = "new-damage-effect", menuName = "ScriptableObjects/Mystra/Effects/Damage", order = 150)]
    internal sealed class Damage : ScriptableEffect
    {
        public override EffectSpec CreateEffectSpec(ScriptableAbility asset)
        {
            return new DamageEffectSpec(asset);
        }

        private sealed class DamageEffectSpec : EffectSpec
        {
            private bool m_IsCriticalHit;

            internal DamageEffectSpec(ScriptableAbility asset) : base(asset)
            {

            }

            public override void PreApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
            {
                base.PreApplyEffectSpec(instigator, target, ref result);

                if (result.success)
                {
                    CalculateAndDamageTargetPsychic(instigator.psychic, target.psychic);
                }
            }

            private void CalculateAndDamageTargetPsychic(PsychicSpec instigator, PsychicSpec target)
            {
                float power = asset.container.power;
                float attack = instigator.attack.valueModified;
                float defence = target.defence.valueModified;

                Debug.Log($"{attack}, {defence}");

                int critical = ((1f / 4f * 100f) > (UnityEngine.Random.value * 100f)) ? 2 : 1;
                float random = UnityEngine.Random.Range(85f, 100f) / 100f;

                float damage = Mathf.Floor((((2f * instigator.level / 5f + 2f) * power * attack / defence / 50f) + 2f) * critical * random);

                Debug.Log($"CRIT: {critical}, RNG: {random}, DMG: {damage}");

                target.health.value = Mathf.Max(target.health.value - damage, 0f);

                CheckForCriticalHit(critical);
            }

            private void CheckForCriticalHit(int value)
            {
                m_IsCriticalHit = value > 1;
            }

            public override System.Collections.IEnumerator ApplyEffectSpec(Combatant instigator, Combatant target)
            {
                for (int i = 0; i < 4; i++)
                {
                    yield return target.image.material.Flash(0f, 0.1f, EasingType.PingPong);
                }

                switch (target.affinity)
                {
                    case Affinity.Friendly:
                        yield return BattleCoordinator.instance.GetGraphicsInterface().InterpolatePlayerHealthBar();
                        break;
                    case Affinity.Hostile:
                        yield return BattleCoordinator.instance.GetGraphicsInterface().InterpolateEnemyHealthBar();
                        break;
                }
            }

            public override void PostApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
            {
                base.PostApplyEffectSpec(instigator, target, ref result);

                if (m_IsCriticalHit)
                {
                    result.message += string.Concat($"A CRITICAL hit!\n");
                }
            }
        }
    }
}

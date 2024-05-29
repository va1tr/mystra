using UnityEngine;

namespace Eevee
{
	public abstract class EffectSpec
	{
        public readonly ScriptableAbility asset;

        protected readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);
        protected readonly WaitForSeconds m_DelayForOneSecond = new WaitForSeconds(kDelayForOneSecond);

        private const float kDelayForHalfSecond = 0.5f;
        private const float kDelayForOneSecond = 1f;

        public EffectSpec(ScriptableAbility asset)
        {
            this.asset = asset;
        }

        public virtual void PreApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
        {
            CanApplyEffectSpec(instigator, target, ref result);
        }

        public abstract System.Collections.IEnumerator ApplyEffectSpec(Combatant instigator, Combatant target);

        protected virtual bool CanApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
        {
            return CheckAbilityAccuracy(instigator.psychic, target.psychic, ref result) &&
                CheckAbilityIsNotBlockedByTrait(instigator.psychic, target.psychic, ref result);
        }

        public virtual void PostApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
        {

        }

        private bool CheckAbilityAccuracy(PsychicSpec instigator, PsychicSpec target, ref SpecResult result)
        {
            result.success = (Random.value * 100f) <= asset.container.accuracy;

            if (!result.success)
            {
                result.message += string.Concat($"{instigator.name}'s attack missed!\n");
            }

            return result.success;
        }

        private bool CheckAbilityIsNotBlockedByTrait(PsychicSpec instigator, PsychicSpec target, ref SpecResult result)
        {
            var blockedAbilities = target.trait.asset.blockAbilityActivation;

            for (int i = 0; i < blockedAbilities.Length; i++)
            {
                if (blockedAbilities[i].Equals(asset))
                {
                    result.success = false;
                    result.message += string.Concat($"{asset.name.ToUpper()} had no effect!\n");
                }
            }

            return result.success;
        }
    }
}

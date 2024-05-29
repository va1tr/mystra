namespace Eevee
{
    public abstract class AbilitySpec
    {
        public readonly ScriptableAbility asset;

        public readonly string name;

        protected readonly EffectSpec m_Effect;

        public AbilitySpec(ScriptableAbility asset)
        {
            this.asset = asset;

            this.name = asset.name.ToUpper();

            m_Effect = asset.effect.CreateEffectSpec(asset);
        }

        public virtual void PreAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
        {
            result = SpecResult.CreateSpecResult(string.Empty, true);

            if (CanActivateAbility(instigator, target, ref result))
            {
                result.message = string.Concat($"{instigator.psychic.name.ToUpper()} used {name}!\n");
            }
        }

        public abstract System.Collections.IEnumerator ActivateAbility(Combatant instigator, Combatant target);

        public virtual void PostAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
        {
            result = SpecResult.CreateSpecResult(string.Empty, true);
        }

        protected virtual bool CanActivateAbility(Combatant instigator, Combatant target, ref SpecResult result)
        {
            return true;
        }
    }

    public sealed class SpecResult
    {
        public string message;
        public bool success;

        public SpecResult(string message, bool success)
        {
            this.message = message;
            this.success = success;
        }

        public static SpecResult CreateSpecResult(string message, bool success)
        {
            return new SpecResult(message, success);
        }
    }
}

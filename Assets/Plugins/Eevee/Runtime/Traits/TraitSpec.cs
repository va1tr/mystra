using UnityEngine;

namespace Eevee
{
	public sealed class TraitSpec
	{
		public readonly ScriptableTrait asset;

        public readonly string name;

		public bool isConditionMet;
		public bool isUnlocked;

        public TraitSpec(ScriptableTrait asset)
		{
			this.asset = asset;

			this.name = asset.name.ToUpper();

			this.isConditionMet = false;
			this.isUnlocked = false;
		}

		public bool TryUnlockingTrait(ScriptableAbility ability)
		{
			isConditionMet = false;

			if (!isUnlocked)
			{
                var abilities = asset.unlockOnAbilityUsed;

                for (int i = 0; i < abilities.Length; i++)
                {
                    if (abilities[i].Equals(ability))
                    {
                        isUnlocked = true;
                        isConditionMet = true;
                        break;
                    }
                }
            }

			return isConditionMet;
		}

		
	}
}

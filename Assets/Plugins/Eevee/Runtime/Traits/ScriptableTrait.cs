using System;

using UnityEngine;

namespace Eevee
{
    [CreateAssetMenu(fileName = "new-trait", menuName = "ScriptableObjects/Eevee/Trait", order = 150)]
    public sealed class ScriptableTrait : ScriptableObject
	{
        [SerializeField, TextArea]
        public string description;

        [SerializeField]
        internal ScriptableAbility[] unlockOnAbilityUsed;

        [SerializeField]
        internal ScriptableAbility[] blockAbilityActivation;

        [SerializeField]
        internal ScriptableAbility[] allowAbilityActivation;

        public TraitSpec CreateTraitSpec()
		{
			return new TraitSpec(this);
		}
	}
}

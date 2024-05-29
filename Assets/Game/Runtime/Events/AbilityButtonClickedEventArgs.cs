using Eevee;

namespace Mystra
{
	internal readonly struct AbilityButtonClickedEventArgs
	{
		internal readonly AbilitySpec ability;

		private AbilityButtonClickedEventArgs(AbilitySpec ability)
		{
			this.ability = ability;
		}

		internal static AbilityButtonClickedEventArgs Create(AbilitySpec ability)
		{
			return new AbilityButtonClickedEventArgs(ability);
		}
	}
}

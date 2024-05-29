using UnityEngine;

using Eevee;

namespace Mystra
{
	internal sealed class Player : MonoBehaviour
	{
		[SerializeField]
		private ScriptablePsychic m_Asset;

		[SerializeField]
		private ScriptableAbility m_AnalyseAbility;

		internal PsychicSpec CreateStartupPlayer()
		{
			var psychic = new PsychicSpec(m_Asset);

			psychic.analyse = m_AnalyseAbility.CreateAbilitySpec();

			return psychic;
		}

    }
}

using UnityEngine;

using Eevee;

namespace Mystra
{
	internal sealed class SpawnEncounter : MonoBehaviour
	{
		[SerializeField]
		private Combatant m_Combatant;

		[SerializeField]
		private ScriptablePsychic m_Asset;

        private void Awake()
        {
			m_Combatant.psychic = new PsychicSpec(m_Asset);
        }
    }
}

using UnityEngine;

using Eevee;

namespace Mystra
{
	internal sealed class SpawnPlayerFromSet : MonoBehaviour
	{
		[SerializeField]
		private Combatant m_Player;

		[SerializeField]
		private PlayerRuntimeSet m_RuntimeSet;

        private void Awake()
        {
			m_Player.psychic = m_RuntimeSet.GetPlayerPsychic();
        }
    }
}

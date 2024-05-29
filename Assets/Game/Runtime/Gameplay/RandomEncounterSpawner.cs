using UnityEngine;

using Eevee;

namespace Mystra
{
	internal sealed class RandomEncounterSpawner : MonoBehaviour
	{
		[SerializeField]
		private RandomEncounterRuntimeSet m_RuntimeSet;

		[SerializeField]
		private Combatant m_Enemy;

        internal void SetRandomEncounterFromSet()
		{
            m_Enemy.psychic = RandomEncounterRuntimeSet.CreateYokaiFromSet(m_RuntimeSet.GetRandomEncounterFromSet());		
		}
    }
}

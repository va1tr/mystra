using Eevee;
using UnityEngine;

namespace Mystra
{
	internal sealed class AddPlayerToRuntimeSet : MonoBehaviour
	{
		[SerializeField]
		private PlayerRuntimeSet m_RuntimeSet;

        [SerializeField]
        private Player m_Player;

        private void Awake()
        {
            m_RuntimeSet.Add(m_Player.CreateStartupPlayer());
        }
    }
}

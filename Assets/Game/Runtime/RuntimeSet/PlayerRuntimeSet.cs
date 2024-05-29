using UnityEngine;

using Eevee;
using Golem;

namespace Mystra
{
    [CreateAssetMenu(fileName = "new-player-runtime-set", menuName = "ScriptableObjects/Mystra/RuntimeSet/Player", order = 150)]
    internal sealed class PlayerRuntimeSet : RuntimeSet<PsychicSpec>
	{
        internal PsychicSpec GetPlayerPsychic()
        {
            return m_Collection[0];
        }
	}
}

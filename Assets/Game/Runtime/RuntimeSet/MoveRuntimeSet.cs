using UnityEngine;

using Eevee;
using Golem;

namespace Mystra
{
    [CreateAssetMenu(fileName = "new-move-runtime-set", menuName = "ScriptableObjects/Mystra/RuntimeSet/Move", order = 150)]
    internal sealed class MoveRuntimeSet : RuntimeSet<Move>
	{
        internal void AddFightMoveToRuntimeSet(Combatant instigator, Combatant target, AbilitySpec ability)
        {
            Add(new Fight(instigator, target, ability));
        }

        internal void Sort()
        {
            m_Collection.Sort();
        }
	}
}

using System;

using UnityEngine;

using Eevee;
using Golem;

namespace Mystra
{
    [CreateAssetMenu(fileName = "new-encounter-runtime-set", menuName = "ScriptableObjects/Mystra/RuntimeSet/Encounter", order = 150)]
    internal sealed class RandomEncounterRuntimeSet : RuntimeSet<RandomEncounterSet>
	{
        internal static PsychicSpec CreateYokaiFromSet(RandomEncounterSet set)
        {
            return new PsychicSpec(set.asset, set.level);
        }

        internal RandomEncounterSet GetRandomEncounterFromSet()
        {
            #region Debug
#if UNITY_EDITOR
            VerifyTotalEncounterRateIsEqualToOneHundred();
#endif
            #endregion

            int seed = Mathf.RoundToInt(UnityEngine.Random.value * 100f);

            for (int i = 0; i < Count(); i++)
            {
                seed -= m_Collection[i].encounterRate;

                if (seed <= 0)
                {
                    return m_Collection[i];
                }
            }

            return m_Collection[0];
        }

        #region Debug
        private void VerifyTotalEncounterRateIsEqualToOneHundred()
        {
            int totalEncounterRate = 0;

            for (int i = 0; i < Count(); i++)
            {
                totalEncounterRate += m_Collection[i].encounterRate;
            }

            if (totalEncounterRate != 100)
            {
                string message = string.Concat($"The total weights for an encounter set must equal 100, " +
                    $"the total encounter weight is {totalEncounterRate}");
                throw new ArgumentOutOfRangeException(message);
            }
        }
        #endregion
    }

    [Serializable]
    internal sealed class RandomEncounterSet
    {
        [SerializeField]
        internal ScriptablePsychic asset;

        [SerializeField, Range(1, 100)]
        internal int level;

        [SerializeField, Range(1, 100)]
        internal int encounterRate;
    }
}

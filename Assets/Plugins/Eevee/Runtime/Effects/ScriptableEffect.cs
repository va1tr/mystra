using System;
using UnityEngine;

namespace Eevee
{
    public enum EffectType
    {
        Direct,
        Temporary
    }

    public enum EffectModifierType
    {
        Target,
        Self
    }

    public abstract class ScriptableEffect : ScriptableObject
    {
        public abstract EffectSpec CreateEffectSpec(ScriptableAbility asset);
    }

    [Serializable]
    public struct Container
    {
        [SerializeField]
        public EffectType type;

        [SerializeField, Range(0, 250)]
        public int power;

        [SerializeField, Range(0, 100)]
        public int accuracy;

        [SerializeField]
        public EffectModifiers[] modifiers;
    }

    [Serializable]
    public struct EffectModifiers
    {
        [SerializeField]
        public StatisticType stat;

        [SerializeField]
        public EffectModifierType target;

        [SerializeField, Range(-2, 2)]
        public int multiplier;
    }
}

using System;
using UnityEngine;

namespace Eevee
{
    [CreateAssetMenu(fileName = "new-psychic", menuName = "ScriptableObjects/Eevee/Psychic", order = 150)]
    public class ScriptablePsychic : ScriptableObject
	{
        [SerializeField]
        private Sprite[] m_Sprites;

        [SerializeField]
        private int m_Health;

        [SerializeField]
        private int m_Attack;

        [SerializeField]
        private int m_Defence;

        [SerializeField]
        private int m_Speed;

        [SerializeField]
        private int m_level;

        [SerializeField]
        private int m_Experience;

        [SerializeField]
        private LevelRequiredAbility[] m_Abilities;

        [SerializeField]
        private ScriptableTrait m_Trait;

        public Sprite[] sprites
        {
            get => m_Sprites;
        }

        public ScriptableTrait trait
        {
            get => m_Trait;
        }

        internal int health
        {
            get => m_Health;
        }

        internal int attack
        {
            get => m_Attack;
        }

        internal int defence
        {
            get => m_Defence;
        }

        internal int speed
        {
            get => m_Speed;
        }

        internal int level
        {
            get => m_level;
        }

        public int experience
        {
            get => m_Experience;
        }

        internal LevelRequiredAbility[] abilities
        {
            get => m_Abilities;
        }
    }

    [Serializable]
    public sealed class Trait
    {
        [SerializeField]
        public string description;

        [SerializeField]
        public bool isUnlocked;
    }
}

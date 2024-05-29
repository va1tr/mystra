using UnityEngine;

namespace Eevee
{
    public abstract class ScriptableAbility : ScriptableObject
    {
        [SerializeField, TextArea]
        private string m_Description;

        [SerializeField]
        private ScriptableEffect m_Effect;

        [SerializeField]
        private Container m_Container = new Container();

        public string description
        {
            get => m_Description;
        }

        public ScriptableEffect effect
        {
            get => m_Effect;
        }

        public Container container
        {
            get => m_Container;
        }

        public abstract AbilitySpec CreateAbilitySpec();
    }
}

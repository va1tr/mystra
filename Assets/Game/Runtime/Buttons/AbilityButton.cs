using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Eevee;
using Golem;
using Voltorb;

namespace Mystra
{
    internal sealed class AbilityButton : SelectableButton<AbilitySpec>
    {
        [SerializeField]
        private Typewriter m_Typewriter;

        [SerializeField]
        private Text m_AbilityName;

        private AbilitySpec m_Ability;
        private string m_Description;

        public override void BindProperties(AbilitySpec properties)
        {
            m_Ability = properties;

            this.interactable = true;

            m_AbilityName.text = m_Ability.name;

            var asset = m_Ability.asset;
            var power = asset.container.power;
            var accuracy = asset.container.accuracy;

            m_Description = string.Concat($"{asset.description}\n Power {power} Accuracy {accuracy}\n");
        }

        public override void BindPropertiesToNull()
        {
            m_Ability = null;

            this.interactable = false;

            m_AbilityName.text = string.Empty;
            m_Description = string.Empty;
        }

        public override void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
        {
            base.OnSelect(eventData);

            m_Typewriter.PrintCompletedText(m_Description);
        }

        public override void Select()
        {
            base.Select();

            EventSystem.instance.Invoke(AbilityButtonClickedEventArgs.Create(m_Ability));
        }

        public override void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            m_Typewriter.CleanupAndClearAllText();
        }
    }
}

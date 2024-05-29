using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Voltorb;

namespace Mystra
{
	internal sealed class MenuButton : SelectableButton
	{
        [SerializeField]
        private MenuButtonClickedEvent m_OnClick;

        [SerializeField]
        private Typewriter m_Typewriter;

        [SerializeField, TextArea]
        private string m_Description;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            m_Typewriter.PrintCompletedText(m_Description);
        }

        public override void Select()
        {
            m_OnClick?.Invoke();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            m_Typewriter.CleanupAndClearAllText();
        }

        [Serializable]
        private sealed class MenuButtonClickedEvent : UnityEvent
        {

        }
    }
}

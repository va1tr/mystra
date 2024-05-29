using UnityEngine;
using UnityEngine.EventSystems;

using Voltorb;

namespace Mystra
{
	internal sealed class VerticalNavigation : SelectableNavigation
	{
        public override void OnSubmit(BaseEventData eventData)
        {
            m_Selectables[m_SelectedIndex].Select();
        }

        protected override void OnMoveUp(AxisEventData eventData)
        {
            int lastSelectedIndex = m_SelectedIndex;
            int index = m_SelectedIndex;

            do
            {
                index = Mathf.Clamp(index - 1, 0, m_Selectables.Length - 1);

            } while (!IsTargetSelectableInteractable(index));

            if (lastSelectedIndex != index)
            {
                m_Selectables[lastSelectedIndex].OnDeselect(eventData);
                m_SelectedIndex = index;
                m_Selectables[m_SelectedIndex].OnSelect(eventData);
            }
        }

        protected override void OnMoveDown(AxisEventData eventData)
        {
            int lastSelectedIndex = m_SelectedIndex;
            int index = m_SelectedIndex;

            do
            {
                index = Mathf.Clamp(index + 1, 0, m_Selectables.Length - 1);

            } while (!IsTargetSelectableInteractable(index));

            if (lastSelectedIndex != index)
            {
                m_Selectables[lastSelectedIndex].OnDeselect(eventData);
                m_SelectedIndex = index;
                m_Selectables[m_SelectedIndex].OnSelect(eventData);
            }
        }

    }
}

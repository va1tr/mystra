using UnityEngine;
using UnityEngine.UI;

using Voltorb;

namespace Mystra
{
	internal sealed class MenuCursorFadeNavigation : MenuCursorNavigation
	{
        private Image image
        {
            get
            {
                if (m_Image == null)
                {
                    m_Image = GetComponentInChildren<Image>();
                }

                return m_Image;
            }
        }

        private Image m_Image;

        public override void OnSelect(GameObject selectedObject)
        {
            base.OnSelect(selectedObject);

            image.CrossFadeAlpha(0.5f, 0.25f, false);
        }

        public override void OnDeselect(GameObject lastSelectedObject)
        {
            base.OnDeselect(lastSelectedObject);

            image.CrossFadeAlpha(1f, 0f, false);
        }
    }
}

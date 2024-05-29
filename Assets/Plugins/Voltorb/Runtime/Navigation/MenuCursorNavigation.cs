using UnityEngine;
using UnityEngine.EventSystems;

namespace Voltorb
{
    [RequireComponent(typeof(RectTransform))]
    public class MenuCursorNavigation : MonoBehaviour
    {
        private RectTransform rectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = GetComponent<RectTransform>();
                }

                return m_RectTransform;
            }
        }

        private RectTransform m_RectTransform;

        public void Select()
        {

        }

        public virtual void OnSelect(GameObject selectedObject)
        {
            rectTransform.anchoredPosition = selectedObject.GetComponent<RectTransform>().anchoredPosition;
        }

        public virtual void OnDeselect(GameObject lastSelectedObject)
        {
           
        }
    }
}

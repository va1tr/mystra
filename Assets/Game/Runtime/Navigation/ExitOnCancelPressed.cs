using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Mystra
{
    internal sealed class ExitOnCancelPressed : MonoBehaviour, ICancelHandler
    {
        [SerializeField]
        private UnityEvent m_OnCancelPressed;

        public void OnCancel(BaseEventData eventData)
        {
            m_OnCancelPressed?.Invoke();
        }
    }
}

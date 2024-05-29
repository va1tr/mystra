using UnityEngine;

namespace Mystra
{
	internal sealed class NavigationUtility : MonoBehaviour
	{
        [SerializeField]
        private GameObject m_CurrentNavigationObject;

        internal void OnShow()
        {
            UnityEventSystemUtility.SetSelectedGameObject(m_CurrentNavigationObject);
        }
    }
}

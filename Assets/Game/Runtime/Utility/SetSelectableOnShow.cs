using UnityEngine;

using Voltorb;

namespace Mystra
{
	internal sealed class SetSelectableOnShow : MonoBehaviour
	{
		[SerializeField]
		private SelectableNavigation m_Navigation;

        private void OnEnable()
        {
            m_Navigation.SetSelectable(null, 0);
            
        }
    }
}

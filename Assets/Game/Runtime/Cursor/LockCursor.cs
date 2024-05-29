using UnityEngine;

namespace Mystra
{
	internal sealed class LockCursor : MonoBehaviour
	{
        private GameObject m_LastSelectedObject;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (UnityEventSystemUtility.current.currentSelectedGameObject == null)
            {
                UnityEventSystemUtility.current.SetSelectedGameObject(m_LastSelectedObject);
            }
            else
            {
                m_LastSelectedObject = UnityEventSystemUtility.current.currentSelectedGameObject;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Mystra
{
	internal sealed class SetTextOnEnable : MonoBehaviour
	{
		[SerializeField]
		private Text m_Text;

		[SerializeField]
		private string m_GameMode;

        private void OnEnable()
        {
			m_Text.text = string.Concat($"{m_GameMode} mode");
        }
    }
}

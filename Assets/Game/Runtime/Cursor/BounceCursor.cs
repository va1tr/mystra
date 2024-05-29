using System.Collections;

using UnityEngine;

using Slowbro;

namespace Mystra
{
    [RequireComponent(typeof(RectTransform))]
	internal sealed class BounceCursor : MonoBehaviour
	{
		private RectTransform m_RectTransform;

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

        private void OnEnable()
        {
			StartCoroutine(Animation());
        }

		private IEnumerator Animation()
		{
			while (true)
			{
				yield return rectTransform.Translate(rectTransform.anchoredPosition, Vector3.up, 0.75f, Space.Self, EasingType.PingPong);
			}
		}

        private void OnDisable()
        {
			StopCoroutine(Animation());
        }
    }
}

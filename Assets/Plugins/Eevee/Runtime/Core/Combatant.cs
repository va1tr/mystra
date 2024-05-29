using System;

using UnityEngine;
using UnityEngine.UI;

namespace Eevee
{
	[RequireComponent(typeof(RectTransform), typeof(Image))]
	public class Combatant : MonoBehaviour
	{
		[SerializeField]
		private Affinity m_Affinity = Affinity.Hostile;

		public Affinity affinity
		{
			get => m_Affinity;
			set => m_Affinity = value;
		}

		private RectTransform m_RectTransform;

		public RectTransform rectTransform
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

		private Image m_Image;

		public Image image
		{
			get
			{
				if (m_Image == null)
				{
					m_Image = GetComponent<Image>();
				}

				return m_Image;
			}
		}

        private PsychicSpec m_Psychic;

        public PsychicSpec psychic
        {
            get
            {
#if UNITY_EDITOR
                if (m_Psychic == null)
                {
                    string message = string.Concat($"Psychic for {name} has not yet been set");
                    throw new NullReferenceException(message);
                }
#endif
                return m_Psychic;
            }
            set
            {
                m_Psychic = value;
            }
        }
    }
}

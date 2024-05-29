using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Eevee;
using Voltorb;
using Slowbro;

namespace Mystra
{
	[RequireComponent(typeof(Slider))]
	internal sealed class Healthbar : Menu<PsychicSpec>
	{
		private PsychicSpec m_Psychic;

		private Slider m_Slider;

		private Slider slider
		{
			get
			{
				if (m_Slider == null)
				{
					m_Slider = GetComponent<Slider>();
				}

				return m_Slider;
			}
		}

        public override void SetProperties(PsychicSpec properties)
        {
			m_Psychic = properties;

			slider.minValue = 0f;
			slider.maxValue = properties.health.maxValue;
			slider.value = properties.health.value;
        }

        internal IEnumerator SetHealthBarValue()
        {
            int current = Mathf.FloorToInt(slider.value);
            int target = Mathf.FloorToInt(m_Psychic.health.value);
            int max = Mathf.FloorToInt(slider.maxValue);

            int difference = current - target;
            float duration = 0.5f + (difference / 16f);

			yield return slider.Interpolate(target, duration, EasingType.EaseOutSine);
        }
    }
}

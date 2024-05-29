using System.Collections;

using UnityEngine;

using Golem;

namespace Mystra
{
	internal sealed class AudioManager : Singleton<AudioManager>
	{
		[Header("Audio Source")]
		[SerializeField]
		private AudioSource m_SoundFX;

		[SerializeField]
		private AudioSource m_Music;

		[Header("Music")]
		[SerializeField]
		private AudioClip m_OverworldClip;

		[SerializeField]
		private AudioClip m_BattleClip;

		public void PlayBlip()
		{
			m_SoundFX.pitch = Random.Range(1.45f, 1.5f);
			m_SoundFX.Play();
		}

		public void ChangeToOverworldMusic()
		{
			StartCoroutine(FadeToClip(null));
		}

		public void ChangeToBattleMusic()
		{
			StartCoroutine(FadeToClip(m_BattleClip));
		}

		private IEnumerator FadeToClip(AudioClip clip)
		{
			while (m_Music.volume > 0f)
			{
				yield return m_Music.volume = Mathf.Max(m_Music.volume - 2f * Time.deltaTime, 0f);
            }

			if (clip != null)
			{
                m_Music.clip = clip;
            }

            while (m_Music.volume < 1f)
            {
                yield return m_Music.volume = Mathf.Min(m_Music.volume + 2f * Time.deltaTime, 1f);
            }

			m_Music.Play();
		}
	}
}

using System;

using UnityEngine;
using UnityEngine.UI;

namespace Slowbro
{
	public sealed class Animation : Tween<Image, Sprite>
	{
        private Sprite[] m_Sprites;

        private int m_NumberOfLoops;
        private int m_CurrentFrame;
        private int m_LoopDurationCounter;

        private float m_FrameRate;

        internal Animation(Func<Image, Sprite> getter, Action<Image, Sprite> setter) : base(getter, setter)
        {

        }

        public override void Initialise()
        {
            base.Initialise();

            m_CurrentFrame = 0;
            m_LoopDurationCounter = 0;

            m_Component.sprite = m_Sprites[m_CurrentFrame];
        }

        public override bool Update()
        {
            if (Time.time > m_TimeElapsed)
            {
                m_TimeElapsed = Time.time + (1f / m_FrameRate);

                m_CurrentFrame++;

                if (m_CurrentFrame >= m_Sprites.Length)
                {
                    m_LoopDurationCounter++;

                    if (m_NumberOfLoops > m_LoopDurationCounter)
                    {
                        m_CurrentFrame = 0;
                        m_Component.sprite = m_Sprites[m_CurrentFrame];

                        return true;
                    }

                    return false;
                }

                m_Component.sprite = m_Sprites[m_CurrentFrame];
            }

            return true;
        }

        public Animation SetSprites(params Sprite[] sprites)
        {
            m_Sprites = sprites;
            return this;
        }

        public Animation SetFrameRate(float frameRate)
        {
            m_FrameRate = frameRate;
            return this;
        }

        public Animation SetNumberOfLoops(int numberOfLoops)
        {
            m_NumberOfLoops = numberOfLoops;
            return this;
        }
    }
}

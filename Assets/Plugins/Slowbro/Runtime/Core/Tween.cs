using System;
using UnityEngine;

namespace Slowbro
{
    public class Tween<T1, T2> : Routine
    {
        internal readonly Func<T1, T2> getter;
        internal readonly Action<T1, T2> setter;

        protected Space m_RelativeTo;
        protected EasingType m_Easing;

        protected T1 m_Component;

        protected IInterpolator<T2> m_Interpolator;

        protected T2 m_StartValue;
        protected T2 m_EndValue;

        protected float m_TimeElapsed;
        protected float m_Duration;

        internal Tween(Func<T1, T2> getter, Action<T1, T2> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public override void Initialise()
        {
            m_TimeElapsed = 0f;
        }

        public override bool Update()
        {
            m_TimeElapsed += Time.deltaTime;

            setter(m_Component, m_Interpolator.Interpolate(m_StartValue, m_EndValue, Mathf.Min(m_TimeElapsed / m_Duration, 1.0f)));

            return IsCompleted();
        }

        public override bool IsCompleted()
        {
            return m_TimeElapsed < m_Duration;
        }

        internal virtual Tween<T1, T2> Initialise(T1 component)
        {
            m_Component = component;
            return this;
        }

        internal virtual Tween<T1, T2> SetStart(T2 start)
        {
            m_StartValue = start;
            return this;
        }

        internal virtual Tween<T1, T2> SetEnd(T2 end)
        {
            m_EndValue = end;
            return this;
        }

        internal virtual Tween<T1, T2> SetDuration(float duration)
        {
            m_Duration = duration;
            return this;
        }

        internal virtual Tween<T1, T2> SetSpace(Space relativeTo)
        {
            m_RelativeTo = relativeTo;
            return this;
        }

        internal virtual Tween<T1, T2> SetInterpolation(IInterpolator<T2> interpolator)
        {
            m_Interpolator = interpolator;
            return this;
        }
    }
}

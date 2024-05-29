using System;
using UnityEngine;

namespace Slowbro
{
    public class AnchoredPosition : Tween<RectTransform, Vector3>
    {
        internal AnchoredPosition(Func<RectTransform, Vector3> getter, Action<RectTransform, Vector3> setter) : base(getter, setter)
        {

        }

        public override void Initialise()
        {
            switch (m_RelativeTo)
            {
                case Space.Self:
                    m_EndValue += m_StartValue;
                    break;
                case Space.World:
                    break;
            }

            base.Initialise();
        }
    }
}

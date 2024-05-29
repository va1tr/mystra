using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Slowbro
{
    public interface IInterpolator<T>
    {
        public T Interpolate(T a, T b, float t);
    }

    public struct FloatInterpolator : IInterpolator<float>
    {
        internal readonly EasingType easing;

        internal FloatInterpolator(EasingType easing)
        {
            this.easing = easing;
        }

        public float Interpolate(float a, float b, float t)
        {
            return Mathf.LerpUnclamped(a, b, Easing.Resolve(easing, t));
        }
    }

    public struct IntInterpolator : IInterpolator<float>
    {
        internal readonly EasingType easing;

        internal IntInterpolator(EasingType easing)
        {
            this.easing = easing;
        }

        public float Interpolate(float a, float b, float t)
        {
            return Mathf.RoundToInt(Mathf.LerpUnclamped(a, b, Easing.Resolve(easing, t)));
        }
    }

    public struct Vector3IntInterpolator : IInterpolator<Vector3>
    {
        internal readonly EasingType easing;

        internal Vector3IntInterpolator(EasingType easing)
        {
            this.easing = easing;
        }

        public Vector3 Interpolate(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(Mathf.RoundToInt(Mathf.LerpUnclamped(a.x, b.x, Easing.Resolve(easing, t))),
                Mathf.RoundToInt(Mathf.LerpUnclamped(a.y, b.y, Easing.Resolve(easing, t))), Mathf.RoundToInt(Mathf.LerpUnclamped(a.z, b.z, Easing.Resolve(easing, t))));
        }
    }
}

using System.Collections;

using UnityEngine;

namespace Slowbro
{
    public class Parallel : Routine
    {
        private bool[] m_IsDone;

        public Parallel(params IEnumerator[] coroutines)
        {
            m_IsDone = new bool[coroutines.Length];

            for (int i = 0; i < coroutines.Length; i++)
            {
                EmptyBehaviour.instance.StartCoroutine(Wrapper(coroutines[i], i));
            }
        }

        public override void Initialise()
        {

        }

        public override bool Update()
        {
            return IsCompleted();
        }

        public override bool IsCompleted()
        {
            for (int i = 0; i < m_IsDone.Length; i++)
            {
                if (!m_IsDone[i])
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerator Wrapper(IEnumerator coroutine, int index)
        {
            m_IsDone[index] = false;

            yield return coroutine;

            m_IsDone[index] = true;
        }
    }
}

using UnityEngine;

namespace Slowbro
{
    public abstract class Routine : CustomYieldInstruction
    {
        public override bool keepWaiting => Update();

        public abstract void Initialise();

        public abstract bool Update();
        public abstract bool IsCompleted();

        public Routine Run()
        {
            Initialise();
            return this;
        }
    }
}

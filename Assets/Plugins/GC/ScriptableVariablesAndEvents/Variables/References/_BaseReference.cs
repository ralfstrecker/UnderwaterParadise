using System;
using GC.Events;

namespace GC.Variables
{
    [Serializable]
    public class BaseReference<T, TVariable, TGameEvent> where TVariable : BaseVariable<T, TGameEvent> where TGameEvent : BaseGameEvent<T>
    {
        public bool useConstant = true;
        public T constantValue;
        public TVariable variable;

        public T Value
        {
            get => useConstant ? constantValue : variable.Value;
            set
            {
                if (useConstant)
                {
                    constantValue = value;
                }
                else
                {
                    variable.Value = value;
                }
            }
        }
    }
}
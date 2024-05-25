using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic
{
    [Serializable]
    public sealed class AtomicBoolAspect : ValueAspect<AtomicBool>
    {
    }
    
    [Serializable]
    public sealed class AtomicFloatAspect : ValueAspect<AtomicFloat>
    {
    }
    
    [Serializable]
    public sealed class AtomicIntAspect : ValueAspect<AtomicInt>
    {
    }
    
    [Serializable]
    public sealed class AtomicQuartenionAspect : ValueAspect<AtomicQuaternion>
    {
    }

    [Serializable]
    public sealed class AtomicVector2Aspect : ValueAspect<AtomicVector2>
    {
    }
    
    [Serializable]
    public sealed class AtomicVector3Aspect : ValueAspect<AtomicVector3>
    {
    }
}
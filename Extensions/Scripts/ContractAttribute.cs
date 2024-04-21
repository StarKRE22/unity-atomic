using System;
// ReSharper disable UnusedParameter.Local

namespace Atomic.Extensions
{
    ///Experimental!
    ///Only for inspector!
    
    //TODO: реализовать статический анализатор для Rider 
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ContractAttribute : Attribute
    {
        public ContractAttribute(params Type[] type)
        {
        }
    }
}
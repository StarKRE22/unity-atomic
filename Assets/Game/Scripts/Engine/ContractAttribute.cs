using System;

namespace Game.Engine
{
    ///Experimental!
    ///Only for inspector!
    
    [Obsolete("УДАЛИТЬ!")]
    //TODO: реализовать статический анализатор для Rider 
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ContractAttribute : Attribute
    {
        public ContractAttribute(params Type[] type)
        {
        }
    }
}
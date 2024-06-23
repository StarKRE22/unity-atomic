using Atomic.Objects;

namespace Atomic.Contracts
{
    public sealed class ValueNameFormatter : SceneObject.IValueNameFormatter
    {
        public string GetName(int id)
        {
            ValueConfig config = ValueManager.GetValueConfig();
            if (config == null)
            {
                return id.ToString();
            }
            
            return config.GetFullItemNameById(id);
        }
    }
}
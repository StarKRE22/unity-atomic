using Atomic.Objects;

namespace Atomic.Contracts
{
    public sealed class TagNameFormatter : SceneObject.ITagNameFormatter
    {
        public string GetName(int id)
        {
            TagsConfig config = TagManager.GetTagConfig();
            if (config == null)
            {
                return id.ToString();
            }

            if (!config.TryFindNameById(id, out string name))
            {
                return id.ToString();
            }

            return $"{name} ({id})";
        }
    }
}
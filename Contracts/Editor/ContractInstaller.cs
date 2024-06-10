using Atomic.Objects;
using UnityEditor;

namespace Atomic.Contracts
{
    [InitializeOnLoad]
    public static class ContractInstaller
    {
        static ContractInstaller()
        {
            SceneObject.TagNameFormatter = new TagNameFormatter();
            SceneObject.ValueNameFormatter = new ValueNameFormatter();
        }
    }
}
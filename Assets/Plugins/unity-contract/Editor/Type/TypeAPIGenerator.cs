#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace Contracts
{
    internal static class TypeAPIGenerator
    {
        internal static void Generate(TypeCatalog catalog)
        {
            string selectedPath = $"{catalog.directoryPath}/{catalog.className}.cs";
            string @namespace = catalog.@namespace;
            
            using StreamWriter writer = new StreamWriter(selectedPath);
            writer.WriteLine("/**");
            writer.WriteLine("* Code generation. Don't modify! ");
            writer.WriteLine(" */");
            
            writer.WriteLine();
            
            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.WriteLine($"    public static class {catalog.className}");
            writer.WriteLine("    {");

            var keys = catalog.items;
            for (int i = 0, count = keys.Count; i < count; i++)
            {
                TypeCatalog.Item item = keys[i];
                writer.WriteLine($"        public const int {item.type} = {item.id};");
            }
            
            writer.WriteLine("    }");
            writer.WriteLine("}");
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif
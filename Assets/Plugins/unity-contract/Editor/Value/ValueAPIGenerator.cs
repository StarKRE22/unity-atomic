#if UNITY_EDITOR
using System.IO;
using UnityEditor;

namespace Contracts
{
    internal static class IndexAPIGenerator
    {
        internal static void Generate(ValueCatalog valueCatalog)
        {
            string suffix = valueCatalog.suffix;
            string @namespace = valueCatalog.@namespace;
            string directory = valueCatalog.directoryPath;

            foreach (ValueCatalog.Category category in valueCatalog.categories)
            {
                GenerateCategory(category, @namespace, suffix, directory);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void GenerateCategory(
            ValueCatalog.Category category,
            string @namespace,
            string suffix,
            string directoryPath
        )
        {
            string className = $"{category.name}{suffix}";

            using StreamWriter writer = new StreamWriter($"{directoryPath}/{className}.cs");
            writer.WriteLine("/**");
            writer.WriteLine("* Code generation. Don't modify! ");
            writer.WriteLine(" */");

            writer.WriteLine();

            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.WriteLine($"    public static class {className}");
            writer.WriteLine("    {");

            var items = category.indexes;
            for (int i = 0, count = items.Count; i < count; i++)
            {
                ValueCatalog.Item item = items[i];
                
                string keyType = string.IsNullOrEmpty(item.type) ? "" : $"// {item.type}";
                writer.WriteLine($"        public const int {item.name} = {item.id}; {keyType}");
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
        }
    }
}
#endif

// Не подходит, нужно импортить namespaces!!!
// if (!string.IsNullOrEmpty(item.type))
// {
//     writer.WriteLine($"        [Contract(typeof({item.type}))]");
// }
//
// writer.WriteLine($"        public const int {item.name} = {item.id};");
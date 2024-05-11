#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace Atomic.Objects
{
    //TODO: REFRESH SETTINGS
    internal static class TagAPIGenerator
    {
        internal static void Generate(TagCatalog catalog)
        {
            string directoryPath = catalog.directoryPath;
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string selectedPath = $"{directoryPath}/{catalog.className}.cs";
            string @namespace = catalog.@namespace;
            
            using StreamWriter writer = new StreamWriter(selectedPath);
            writer.WriteLine("/**");
            writer.WriteLine("* Code generation. Don't modify! ");
            writer.WriteLine("**/");
            
            writer.WriteLine();
            
            //Generate imports:
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using Atomic.Objects;");
            writer.WriteLine("using JetBrains.Annotations;");

            foreach (var import in catalog.imports)
            {
                writer.WriteLine($"using {import};");
            }

            writer.WriteLine();

            //Generate class:
            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.WriteLine($"    public static class {catalog.className}");
            writer.WriteLine("    {");

            //Generate keys:
            writer.WriteLine("        ///Keys");
            var items = catalog.items;
            for (int i = 0, count = items.Count; i < count; i++)
            {
                TagCatalog.Item item = items[i];
                writer.WriteLine($"        public const int {item.type} = {item.id};");
            }
            
            writer.WriteLine();
            writer.WriteLine();

            //Generate extensions:
            writer.WriteLine("        ///Extensions");
            for (int i = 0, count = items.Count; i < count; i++)
            {
                TagCatalog.Item item = items[i];
                writer.WriteLine($"        public static bool Has{item.type}Tag(this IAtomicObject obj) => obj.HasTag({item.type});");
                writer.WriteLine($"        public static bool Add{item.type}Tag(this IAtomicObject obj) => obj.AddTag({item.type});");
                writer.WriteLine($"        public static bool Del{item.type}Tag(this IAtomicObject obj) => obj.DelTag({item.type});");

                if (i < count - 1)
                {
                    writer.WriteLine();
                }
            }
            
            writer.WriteLine("    }");
            writer.WriteLine("}");
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif
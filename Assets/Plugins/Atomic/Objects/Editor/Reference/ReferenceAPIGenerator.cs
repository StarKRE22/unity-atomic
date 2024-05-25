#if UNITY_EDITOR
using System.IO;
using UnityEditor;

namespace Atomic.Objects
{
    //TODO: REFRESH SETTINGS
    internal static class ReferenceAPIGenerator
    {
        internal static void Generate(ValueCatalog valueCatalog)
        {
            string suffix = valueCatalog.suffix;
            string @namespace = valueCatalog.@namespace;
            string directoryPath = valueCatalog.directoryPath;
            string[] imports = valueCatalog.imports;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            foreach (ValueCatalog.Category category in valueCatalog.categories)
            {
                GenerateCategory(category, @namespace, suffix, directoryPath, imports);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void GenerateCategory(
            ValueCatalog.Category category,
            string @namespace,
            string suffix,
            string directoryPath,
            string[] imports
        )
        {
            string className = $"{category.name}{suffix}";

            using StreamWriter writer = new StreamWriter($"{directoryPath}/{className}.cs");
            writer.WriteLine("/**");
            writer.WriteLine("* Code generation. Don't modify! ");
            writer.WriteLine("**/");

            writer.WriteLine();

            //Generate imports:
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using Atomic.Objects;");
            writer.WriteLine("using JetBrains.Annotations;");
            writer.WriteLine("using System.Runtime.CompilerServices;");

            for (int i = 0, count = imports.Length; i < count; i++)
            {
                writer.WriteLine($"using {imports[i]};");
            }

            writer.WriteLine();

            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.WriteLine($"    public static class {className}");
            writer.WriteLine("    {");

            //Generate keys:
            writer.WriteLine("        ///Keys");
            var items = category.indexes;
            for (int i = 0, count = items.Count; i < count; i++)
            {
                ValueCatalog.Item item = items[i];

                string itemType = string.IsNullOrEmpty(item.type) ? "" : $"// {item.type}";
                writer.WriteLine($"        public const int {item.name} = {item.id}; {itemType}");

                // writer.WriteLine(string.IsNullOrEmpty(item.type)
                //     ? "        //Undefined type"
                //     : $"        [Contract(typeof({item.type}))]");

                // writer.WriteLine($"        public const int {item.name} = {item.id};");

                // if (i < count -1)
                // {
                //     writer.WriteLine();
                // }
            }
            
            writer.WriteLine();
            writer.WriteLine();
            
            //Generate extensions:
            writer.WriteLine("        ///Extensions");
            for (int i = 0, count = items.Count; i < count; i++)
            {
                ValueCatalog.Item item = items[i];
                if (string.IsNullOrEmpty(item.type))
                {
                    continue;
                }
                
                writer.WriteLine("        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static {item.type} Get{item.name}(this IAtomicObject obj) => obj.GetReference<{item.type}>({item.name});");
                writer.WriteLine();
                
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static bool TryGet{item.name}(this IAtomicObject obj, out {item.type} result) => obj.TryGetReference({item.name}, out result);");
                writer.WriteLine();

                //TODO: ТУТ ПРОВЕРИТЬ, ЕСЛИ ТИП is IBehaviour, то сделать и добавление Behaviour
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static bool Add{item.name}(this IAtomicObject obj, {item.type} reference) => obj.AddReference({item.name}, reference);");
                writer.WriteLine();

                //TODO: ТУТ ПРОВЕРИТЬ, ЕСЛИ ТИП is IBehaviour, то сделать и удаление Behaviour
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static bool Del{item.name}(this IAtomicObject obj) => obj.DelReference({item.name});");
                writer.WriteLine();
                
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static void Set{item.name}(this IAtomicObject obj, {item.type} reference) => obj.SetReference({item.name}, reference);");

                //TODO: ТУТ ПРОВЕРИТЬ, ЕСЛИ ТИП is IObservable, то Написать Subscribe & Unsubscribe

                if (i < count - 1)
                {
                    writer.WriteLine();
                }
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
        }
    }
}
#endif


// writer.WriteLine($"        public const int {item.name} = {item.id}; {keyType}");

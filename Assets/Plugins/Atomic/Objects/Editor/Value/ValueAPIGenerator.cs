#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Atomic.Objects
{
    //TODO: REFRESH SETTINGS
    [InitializeOnLoad]
    internal static class ValueAPIGenerator
    {
        private static readonly List<Type> allTypes;

        static ValueAPIGenerator()
        {
            allTypes = new List<Type>();
            
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                allTypes.AddRange(assembly.GetTypes().Where(it => typeof(ILogic).IsAssignableFrom(it)));
            }
        }
        
        internal static void Generate(ValueCatalog valueCatalog, int index)
        {
            string suffix = valueCatalog.suffix;
            string @namespace = valueCatalog.@namespace;
            string directoryPath = valueCatalog.directoryPath;
            string[] imports = valueCatalog.imports;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            GenerateCategory(valueCatalog.categories[index], @namespace, suffix, directoryPath, imports);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
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
                string itemName = item.name;
                string itemType = item.type;
                
                if (string.IsNullOrEmpty(itemType))
                {
                    GenerateObjectExtensions(writer, itemName);
                }
                else
                {
                    GenerateTypedExtensions(writer, itemType, itemName);
                }

                if (i < count - 1)
                {
                    writer.WriteLine();
                }
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
        }

        private static void GenerateObjectExtensions(StreamWriter writer, string itemName)
        {
            writer.WriteLine("        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine($"        public static object Get{itemName}(this IAtomicObject obj) => obj.GetValue({itemName});");
            writer.WriteLine();
            
            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine($"        public static bool TryGet{itemName}(this IAtomicObject obj, out object value) => obj.TryGetValue({itemName}, out value);");
            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine($"        public static bool Add{itemName}(this IAtomicObject obj, object value) => obj.AddValue({itemName}, value);");
            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine($"        public static bool Del{itemName}(this IAtomicObject obj) => obj.DelValue({itemName});");   
            writer.WriteLine();

            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine($"        public static void Set{itemName}(this IAtomicObject obj, object value) => obj.SetValue({itemName}, value);");
        }

        private static void GenerateTypedExtensions(StreamWriter writer, string itemType, string itemName)
        {
            writer.WriteLine("        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine($"        public static {itemType} Get{itemName}(this IAtomicObject obj) => obj.GetValue<{itemType}>({itemName});");
            writer.WriteLine();
            
            writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            writer.WriteLine($"        public static bool TryGet{itemName}(this IAtomicObject obj, out {itemType} value) => obj.TryGetValue({itemName}, out value);");
            writer.WriteLine();

            if (IsLogicType(itemType))
            {
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static void Add{itemName}(this IAtomicObject obj, {itemType} value) => obj.AddElement({itemName}, value);");
            }
            else
            {
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static bool Add{itemName}(this IAtomicObject obj, {itemType} value) => obj.AddValue({itemName}, value);");
            }
            
            writer.WriteLine();

            if (IsLogicType(itemType))
            {
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static void Del{itemName}(this IAtomicObject obj) => obj.DelElement({itemName});");
            }
            else
            {
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static bool Del{itemName}(this IAtomicObject obj) => obj.DelValue({itemName});");   
            }
            
            writer.WriteLine();

            if (IsLogicType(itemType))
            {
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static void Set{itemName}(this IAtomicObject obj, {itemType} value) => obj.SetElement({itemName}, value);");
            }
            else
            {
                writer.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine($"        public static void Set{itemName}(this IAtomicObject obj, {itemType} value) => obj.SetValue({itemName}, value);");
            }
        }

        private static bool IsLogicType(string itemType)
        {
            foreach (var type in allTypes)
            {
                if (type.FullName == itemType || type.Name == itemType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
#endif


// writer.WriteLine($"        public const int {item.name} = {item.id}; {keyType}");

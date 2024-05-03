using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

namespace AIModule.UnityEditor
{
    internal static class BlackboardAPIGenerator
    {
        internal static void Generate(IReadOnlyList<BlackboardConfig.Key> keys)
        {
            const string selectedPath = "Assets/Plugins/AI/Blackboards/Scripts/BlackboardAPI.cs";

            using StreamWriter writer = new StreamWriter(selectedPath);
            writer.WriteLine("/**");
            writer.WriteLine("* Code generation. Don't modify! ");
            writer.WriteLine(" */");
            writer.WriteLine("namespace AIModule");
            writer.WriteLine("{");
            writer.WriteLine("    public static class BlackboardAPI");
            writer.WriteLine("    {");

            for (int i = 1, count = keys.Count; i < count; i++)
            {
                BlackboardConfig.Key key = keys[i];
                string keyName = key.name;
                ushort keyID = key.id;
                string keyType = string.IsNullOrEmpty(key.type) ? "Undefined" : key.type;
                
                writer.WriteLine($"        public const ushort {keyName} = {keyID}; // {keyType}");
            }
            
            writer.WriteLine("    }");
            writer.WriteLine("}");
            
            AssetDatabase.Refresh();
        }

        // private static string FormatName(string keyName)
        // {
        //     var sb = new StringBuilder(); 
        //     
        //     for (int i = 0, count = keyName.Length; i < count; i++)
        //     {
        //         char symbol = keyName[i]; 
        //         if (char.IsWhiteSpace(symbol))
        //         {
        //             continue;
        //         }
        //         
        //         sb.Append()
        //     }
        // }
    }
}
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace AIModule.UnityEditor
{
    [UsedImplicitly]
    public sealed class BlackboardKeyAttributeDrawer : OdinAttributeDrawer<BlackboardKeyAttribute, ushort>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            IReadOnlyList<BlackboardConfig.Key> keys = BlackboardConfig.EditorInstance.Keys;
            if (keys.Count <= 0)
            {
                GUILayout.Label(label);
                return;
            }

            GUIHelper.PushLabelWidth(GUIHelper.BetterLabelWidth);

            int index = this.FindCurrentIndex(keys);
            
            index = EditorGUILayout.Popup(label, index, keys.Select(it => it.name).ToArray());
            this.ValueEntry.SmartValue = keys[index].id;

            GUIHelper.PopLabelWidth();
        }

        private int FindCurrentIndex(IReadOnlyList<BlackboardConfig.Key> keys)
        {
            ushort currentId = this.ValueEntry.SmartValue;

            for (int i = 0, count = keys.Count; i < count; i++)
            {
                var key = keys[i];
                if (key.id == currentId)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
#endif
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Atomic.Contracts
{
    internal sealed class ValueWindow : EditorWindow
    {
        private const string DEFAULT_ITEM_NAME = "Enter Name";
        private const string DEFAULT_ITEM_TYPE = "Enter Type";
        private const string DEFAULT_CATEGORY_NAME = "Enter Category";

        private ValueConfig config;

        private SerializedObject catalogSerialized;
        private SerializedProperty itemsSerialized;

        private Vector2 _tabScrollPosition;
        private Vector2 _contentScrollPosition;
        private int _categoryIndex;

        private string _newItemName = DEFAULT_ITEM_NAME;
        private string _newItemType = DEFAULT_ITEM_TYPE;
        private string _newCategoryName = DEFAULT_CATEGORY_NAME;

        private double currentTime;
        private bool stateChanged;

        private void OnEnable()
        {
            this.currentTime = EditorApplication.timeSinceStartup;
            this.DrawTitle();
        }

        private void DrawTitle()
        {
            this.titleContent = new GUIContent("Object Values");
        }

        private void OnInspectorUpdate()
        {
            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - this.currentTime > 1.5f)
            {
                this.UpdateCategory();
                this.currentTime = currentTime;
            }
        }

        private void OnLostFocus()
        {
            this.UpdateCategory();
            this.currentTime = EditorApplication.timeSinceStartup;
        }

        private void OnGUI()
        {
            this.config = ValueManager.GetValueConfig();

            GUILayout.Space(8);
            this.DrawHeader();

            GUILayout.Space(8);
            this.DrawHorizontalSeparator();

            if (this.config == null)
            {
                return;
            }

            if (_categoryIndex >= this.config.categories.Count)
            {
                _categoryIndex = 0;
            }

            GUILayout.Space(8);
            this.DrawErrors();

            // this.catalog.Sort();
            this.catalogSerialized = new SerializedObject(this.config);

            Rect rect = EditorGUILayout.BeginHorizontal();

            this.DrawCategoryTabs();
            this.DrawVerticalSeparator(rect);

            GUILayout.Space(15);
            this.DrawCategoryContent();

            EditorGUILayout.EndHorizontal();

            this.catalogSerialized.ApplyModifiedProperties();
        }

        private void DrawHeader()
        {
            Rect rect = EditorGUILayout.BeginHorizontal();

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20
            };

            rect.x += 5;
            GUI.Label(rect, " Object Values:", labelStyle);

            EditorGUILayout.Space(24);
            this.DrawCatalogButton();
            this.DrawCompileButton();

            EditorGUILayout.EndHorizontal();
        }

        private void DrawHorizontalSeparator()
        {
            //Draw separator line:
            const float separatorWidth = 1;
            Color separatorColor = Color.gray;

            Rect rect = EditorGUILayout.GetControlRect();
            Rect separatorRect = new Rect(rect.x, rect.y, rect.width, separatorWidth);
            EditorGUI.DrawRect(separatorRect, separatorColor);
        }

        private void DrawErrors()
        {
            if (this.config.HasDuplicatedId(out int id))
            {
                Color prevColor = GUI.color;
                GUI.color = Color.red;
                EditorGUILayout.HelpBox($"There us duplicate id! Please, fix id in Index Catalog: {id}!",
                    MessageType.Error);
                GUI.color = prevColor;
            }

            if (this.config.HasDuplicatedName(_categoryIndex, out string key))
            {
                Color prevColor = GUI.color;
                GUI.color = Color.red;
                EditorGUILayout.HelpBox($"There is duplicate name! Please, fix key: {key}!", MessageType.Warning);
                GUI.color = prevColor;
            }
        }

        private void DrawCategoryTabs()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField(" Categories:", new GUIStyle(GUI.skin.label) {fontSize = 16},
                GUILayout.Width(150), GUILayout.Height(20));

            //Draw title"
            GUILayout.Space(10);

            //Draw categories:
            _tabScrollPosition = EditorGUILayout.BeginScrollView(
                _tabScrollPosition, GUILayout.Width(205), GUILayout.ExpandHeight(true)
            );

            SerializedProperty categories = this.catalogSerialized.FindProperty("categories");
            int count = categories.arraySize;
            if (count == 0)
            {
                EditorGUILayout.HelpBox("There aren't categories yet! Please, create category!", MessageType.Warning);
            }

            for (int i = 0; i < count; i++)
            {
                SerializedProperty category = categories.GetArrayElementAtIndex(i);
                SerializedProperty categoryName = category.FindPropertyRelative("name");
                this.DrawCategoryTab(categoryName.stringValue, i);
            }
            
            EditorGUILayout.Space(10);
            // EditorGUILayout.LabelField("Create New", GUILayout.Width(200));
            this.DrawAddCategoryButton();

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void DrawAddCategoryButton()
        {
            Color prevColor = GUI.color;

            const string namePattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";

            if (!this.config.CategoryExists(_newCategoryName) && Regex.IsMatch(_newCategoryName, namePattern))
            {
                GUI.color = new Color(0f, 0.83f, 1f, 1);

                EditorGUILayout.BeginHorizontal(GUILayout.Width(200));

                //Draw name:
                _newCategoryName = EditorGUILayout.TextField(_newCategoryName, GUILayout.ExpandWidth(true));

                //Draw "add" button:
                if (GUILayout.Button("+", GUILayout.Width(50)))
                {
                    this.config.AddCategory(_newCategoryName);

                    _newCategoryName = DEFAULT_CATEGORY_NAME;
                    _categoryIndex = this.config.categories.Count - 1;
                    AssetDatabase.Refresh();
                    AssetDatabase.SaveAssets();
                }
                
                GUI.color = prevColor;

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.HelpBox("Create New Category", MessageType.Info);
            }
            else
            {
                GUI.color = Color.yellow;

                EditorGUILayout.BeginHorizontal(GUILayout.Width(200));

                //Draw name:
                _newCategoryName = EditorGUILayout.TextField(_newCategoryName, GUILayout.ExpandWidth(true));

                GUI.enabled = false;

                //Draw "add" button:
                GUILayout.Button("+", GUILayout.Width(50));
                GUI.enabled = true;

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.HelpBox("Invalid category name!", MessageType.Warning);
                GUI.color = prevColor;
            }

        }

        private void DrawCategoryTab(string categoryName, int categoryIndex)
        {
            if (GUILayout.Toggle(categoryIndex == _categoryIndex, categoryName, "Button", GUILayout.Width(200)))
            {
                _categoryIndex = categoryIndex;
            }
        }

        private void DrawVerticalSeparator(Rect rect)
        {
            //Draw separator line:
            const float separatorWidth = 1;
            Color separatorColor = Color.gray;

            Rect separatorRect = new Rect(210, rect.y, separatorWidth, rect.height);
            EditorGUI.DrawRect(separatorRect, separatorColor);
        }

        private void DrawCategoryContent()
        {
            if (this.config.categories.Count == 0 ||
                _categoryIndex < 0 ||
                _categoryIndex >= this.config.categories.Count)
            {
                return;
            }

            EditorGUILayout.BeginVertical();

            _contentScrollPosition = EditorGUILayout.BeginScrollView(
                _contentScrollPosition, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)
            );

            
            DrawFirstElement();
            this.DrawCustomElements();

            EditorGUILayout.Space(30);
            this.DrawAddElementButton();

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private static void DrawFirstElement()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            GUI.enabled = false;

            EditorGUILayout.LabelField("Index: 0", GUILayout.Width(55));
            EditorGUILayout.TextField(GUIContent.none, "RESERVED",
                GUILayout.ExpandWidth(true), GUILayout.MinWidth(100));

            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
        }

        private void DrawCustomElements()
        {
            
            
            SerializedProperty categories = this.catalogSerialized.FindProperty("categories");
            if (categories.arraySize == 0)
            {
                return;
            }

            SerializedProperty targetCategory = categories.GetArrayElementAtIndex(_categoryIndex);

            this.itemsSerialized = targetCategory.FindPropertyRelative("indexes");
            for (int i = 0, count = this.itemsSerialized.arraySize; i < count; i++)
            {
                this.DrawCustomElement(i);
            }
        }

        private void DrawCustomElement(int itemIndex)
        {
            SerializedProperty item = this.itemsSerialized.GetArrayElementAtIndex(itemIndex);
            SerializedProperty id = item.FindPropertyRelative(nameof(ValueConfig.Item.id));
            SerializedProperty name = item.FindPropertyRelative(nameof(ValueConfig.Item.name));
            SerializedProperty type = item.FindPropertyRelative(nameof(ValueConfig.Item.type));

            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            Color prevColor = GUI.color;

            //
            bool isUniqueueId = this.config.IsUniqueueId(id.intValue);
            if (!isUniqueueId)
            {
                GUI.color = Color.red;
            }

            //Draw index:
            EditorGUILayout.LabelField($"Index: {id.intValue}", GUILayout.Width(55));
            GUI.color = prevColor;

            const string namePattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
            bool isUniqueueName = this.config.IsUniqueueName(_categoryIndex, name.stringValue);
            if (!isUniqueueName || !Regex.IsMatch(name.stringValue, namePattern))
            {
                GUI.color = Color.red;
            }

            //Draw name:
            EditorGUILayout.PropertyField(name, GUIContent.none,
                GUILayout.ExpandWidth(true), GUILayout.MinWidth(125));

            GUI.color = prevColor;

            //Draw type:
            type.stringValue = EditorGUILayout.TextField(type.stringValue,
                GUILayout.ExpandWidth(true), GUILayout.MinWidth(100));

            //Draw "remove" button:
            GUI.color = Color.red;

            if (GUILayout.Button("-", GUILayout.Width(50)))
            {
                this.config.RemoveItemAt(_categoryIndex, itemIndex);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }

            GUI.color = prevColor;

            EditorGUILayout.EndHorizontal();
        }


        private void DrawAddElementButton()
        {
            int freeId = this.config.GetFreeId();

            Color prevColor = GUI.color;

            const string namePattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";

            if (Regex.IsMatch(_newItemName, namePattern) &&
                !this.config.NameExists(_categoryIndex, _newItemName))
            {
                GUI.color = new Color(0f, 0.83f, 1f, 1);

                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                //Draw index:
                EditorGUILayout.LabelField($"Index: {freeId}", GUILayout.Width(55));

                //Draw name:
                _newItemName = EditorGUILayout.TextField(GUIContent.none, _newItemName,
                    GUILayout.ExpandWidth(true), GUILayout.MinWidth(100));

                //Draw type:
                _newItemType = EditorGUILayout.TextField(GUIContent.none, _newItemType,
                    GUILayout.ExpandWidth(true), GUILayout.MinWidth(100));

                //Draw "add" button:
                if (GUILayout.Button("+", GUILayout.Width(50)))
                {
                    this.config.AddItem(_categoryIndex, freeId, _newItemName, _newItemType);

                    _newItemName = DEFAULT_ITEM_NAME;
                    _newItemType = DEFAULT_ITEM_TYPE;

                    AssetDatabase.Refresh();
                    AssetDatabase.SaveAssets();
                }

                GUI.color = prevColor;

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.HelpBox("Create New Value", MessageType.Info);
            }
            else
            {
                GUI.color = Color.yellow;

                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                //Draw index:
                EditorGUILayout.LabelField($"Index: {freeId}", GUILayout.Width(55));

                //Draw name:
                _newItemName = EditorGUILayout.TextField(GUIContent.none, _newItemName,
                    GUILayout.ExpandWidth(true), GUILayout.MinWidth(100));

                //Draw type:
                _newItemType = EditorGUILayout.TextField(GUIContent.none, _newItemType,
                    GUILayout.ExpandWidth(true), GUILayout.MinWidth(100));

                GUI.enabled = false;

                //Draw "add" button:
                GUILayout.Button("+", GUILayout.Width(50));
                GUI.enabled = true;

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.HelpBox($"Invalid item name: '{_newItemName}'", MessageType.Warning);
                GUI.color = prevColor;
            }

        }

        private void DrawCatalogButton()
        {
            if (this.config != null)
            {
                return;
            }

            Color prevColor = GUI.color;
            GUI.color = new Color(0f, 0.83f, 1f, 1);

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 15,
            };

            if (GUILayout.Button("Create catalog", buttonStyle, GUILayout.Height(30), GUILayout.MaxWidth(250)))
            {
                ValueManager.CreateValueConfig();
            }

            GUI.color = prevColor;
        }

        private void DrawCompileButton()
        {
            if (this.config == null)
            {
                return;
            }

            const string namePattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";

            if (this.config.HasDuplicatedId(out _) || 
                this.config.HasDuplicatedName(_categoryIndex, out _) ||
                !this.config.AllMatchesPattern(namePattern))
            {
                GUI.enabled = false;
            }

            Color prevColor = GUI.color;
            GUI.color = new Color(0f, 0.83f, 1f, 1);

            // Создаем новый стиль кнопки
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 15,
            };
            

            if (this.config.categories.Count == 0)
            {
                GUI.enabled = false;
                GUILayout.Button("Compile", buttonStyle, GUILayout.Height(30), GUILayout.MaxWidth(300));
                GUI.enabled = true;
            }
            else
            {
                if (_categoryIndex >= this.config.categories.Count)
                {
                    return;
                }
                
                ValueConfig.Category category = this.config.categories[_categoryIndex];
                string buttonName = $"Compile >>> {category.name}{this.config.suffix}.cs";
                if (GUILayout.Button(buttonName, buttonStyle, GUILayout.Height(30), GUILayout.MaxWidth(250)))
                {
                    ValueAPIGenerator.Generate(this.config, _categoryIndex);
                }
            }

            GUI.color = prevColor;
            GUI.enabled = true;
        }
        
        private void UpdateCategory()
        {
            if (this.config == null)
            {
                return;
            }
            
            if (this.config.categories.Count == 0)
            {
                return;
            }
            
            if (_categoryIndex >= this.config.categories.Count || _categoryIndex < 0)
            {
                return;
            }
                
            ValueAPIGenerator.Generate(this.config, _categoryIndex, false);
        }
    }
}
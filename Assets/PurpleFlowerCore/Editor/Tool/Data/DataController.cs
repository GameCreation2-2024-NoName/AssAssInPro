#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PurpleFlowerCore;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Pditine.Tool
{
    public class DataController : EditorWindow
    {
        private ScriptableObject _currentData;
        private List<ScriptableObject> _data;
        public List<ScriptableObject> Data 
        {
            get
            {
                if(_data == null)
                {
                    LoadData();
                }
                return _data;
            }
        }

        private GenericMenu _dataTree;

        private const string LogChannel = "综合数值控制器";
        private Vector2 _scrollPosition;
        
        
        [MenuItem("PFC/综合数值控制器")]
        public static void OpenWindow()
        {
            var win = GetWindow<DataController>("综合数值控制器");
            win.Show();
        }
        
        private void OnEnable()
        {
            LoadData();
        }
        
        private void OnValidate()
        {
            LoadData();
        }

        private void OnGUI()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            //Head();
            ShowData();
            EditorGUILayout.EndScrollView();
            // GUILayout.FlexibleSpace();
            // GUILayout.BeginHorizontal();
            // Refresh();
            // Apply();
            //GUILayout.EndHorizontal();
        }
        
        private void LoadData()
        {
            PFCLog.Info(LogChannel, "LoadData");
            _data = FindConfigurableObjects();
            _dataTree = new GenericMenu();
            foreach (var data in _data)
            {
                var attribute = data.GetType().GetCustomAttribute(typeof(ConfigurableAttribute)) as ConfigurableAttribute;
                if(attribute == null)
                {
                    PFCLog.Error(LogChannel, "数据没有ConfigurableAttribute");
                    continue;
                }

                string path = "";
                if(  attribute.MenuName!=string.Empty) 
                    path = attribute.MenuName[^1] == '/' ? attribute.MenuName : attribute.MenuName + "/";
                path += data.name;
                _dataTree.AddItem(new GUIContent(path), false, () => { _currentData = data; });
            }
        }
        
        // private void Head()
        // {
        //     var fontStyle = new GUIStyle();
        //     fontStyle.fontSize = 20;
        //     fontStyle.normal.textColor = Color.green;
        //     GUILayout.Label("综合数值控制器", fontStyle);
        // }

        private void ShowData()
        {
            EditorGUILayout.ObjectField("当前数据",_currentData,typeof(ScriptableObject),_currentData);
            var content = new GUIContent("选择数据");
            if (EditorGUILayout.DropdownButton(content,FocusType.Passive))
            {
                _dataTree.DropDown(new Rect(position.width-200,15,10,10));
            }
            ShowObject();
        }
        
        private void Refresh()
        {
            if(!GUILayout.Button("刷新",GUILayout.Height(30),GUILayout.Width(200))) return;
            LoadData();
        }
        
        private void Apply()
        {
            if(!GUILayout.Button("应用",GUILayout.Height(30),GUILayout.Width(200))) return;
        }
        
        private List<ScriptableObject> FindConfigurableObjects()
        {
            var configurableObjects = new List<ScriptableObject>();
            
            var allScriptableObjectTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(ScriptableObject)) && !type.IsAbstract);
            
            var configurableTypes = allScriptableObjectTypes
                .Where(type => type.GetCustomAttributes(typeof(ConfigurableAttribute), false).Length > 0);

            foreach (var type in configurableTypes)
            {
                var guids = AssetDatabase.FindAssets($"t:{type.Name}");
                foreach (var guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                    if (obj != null)
                    {
                        configurableObjects.Add(obj);
                    }
                }
            }
            return configurableObjects;
        }

        private void ShowObject()
        {

            ShowObject(_currentData);
        }

        private void ShowObject(ScriptableObject data, Type type = null)
        {
            var currentData = data;
            if (currentData == null) return;
            // if (type != null && type != typeof(ScriptableObject))
            // {
            //     ShowObject(data, type.BaseType);
            // }else if(currentData.GetType().BaseType != typeof(ScriptableObject))
            // {
            //     ShowObject(data, currentData.GetType().BaseType);
            // }
            if (type == null)
            {
                if(currentData.GetType().BaseType != typeof(ScriptableObject))
                    ShowObject(data, currentData.GetType().BaseType);
            }else if(type.BaseType != typeof(ScriptableObject))
            {
                ShowObject(data, type.BaseType);
            }
            
            Type targetType = type ?? currentData.GetType();
            
            FieldInfo[] fields =
                targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            EditorGUI.BeginChangeCheck();
            
            foreach (FieldInfo field in fields)
            {
                if (field.IsPublic || Attribute.IsDefined(field, typeof(SerializeField)))
                {
                    ShowField(field, currentData);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(currentData);
            }
            
        }

        private void ShowField(FieldInfo field, object target)
        {
            Type fieldType = field.FieldType;
            object value = field.GetValue(target);

            if (fieldType == typeof(int))
            {
                field.SetValue(target, EditorGUILayout.IntField(field.Name, (int)value));
            }
            else if (fieldType == typeof(float))
            {
                field.SetValue(target, EditorGUILayout.FloatField(field.Name, (float)value));
            }
            else if (fieldType == typeof(string))
            {
                field.SetValue(target, EditorGUILayout.TextField(field.Name, (string)value));
            }
            else if (fieldType == typeof(bool))
            {
                field.SetValue(target, EditorGUILayout.Toggle(field.Name, (bool)value));
            }
            else if (fieldType.IsEnum)
            {
                field.SetValue(target, EditorGUILayout.EnumPopup(field.Name, (Enum)value));
            }
            else if (typeof(UnityEventBase).IsAssignableFrom(fieldType))
            {
                ShowUnityEvent(field, target);
            }
            else if (fieldType.IsArray)
            {
                ShowArray(field, target);
            }
            else if (typeof(IList).IsAssignableFrom(fieldType))
            {
                ShowList(field, target);
            }
            else if (fieldType.IsClass || fieldType.IsValueType)
            {
                ShowClassOrStruct(field, target);
            }
            else
            {
                EditorGUILayout.LabelField(field.Name, $"Unsupported type: {fieldType}");
            }
        }

        private void ShowUnityEvent(FieldInfo field, object target)
        {
            SerializedObject serializedObject = new SerializedObject((UnityEngine.Object)target);
            SerializedProperty property = serializedObject.FindProperty(field.Name);
            if (property != null)
            {
                EditorGUILayout.PropertyField(property, new GUIContent(field.Name), true);
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                EditorGUILayout.LabelField(field.Name, "Could not find SerializedProperty for UnityEvent");
            }
        }

        private void ShowArray(FieldInfo field, object target)
        {
            Array array = (Array)field.GetValue(target);
            Type elementType = field.FieldType.GetElementType();
            int newSize = EditorGUILayout.IntField($"{field.Name} Size", array.Length);
            if (newSize != array.Length)
            {
                Array newArray = Array.CreateInstance(elementType, newSize);
                Array.Copy(array, newArray, Math.Min(array.Length, newArray.Length));
                array = newArray;
                field.SetValue(target, array);
            }

            for (int i = 0; i < array.Length; i++)
            {
                object element = array.GetValue(i);
                EditorGUILayout.LabelField($"   {field.Name}[{i}]");
                ShowField(elementType, element, newValue => array.SetValue(newValue, i));
            }
        }

        private void ShowList(FieldInfo field, object target)
        {
            try
            {
                IList list = (IList)field.GetValue(target);
                Type elementType = field.FieldType.GetGenericArguments()[0];
                int newSize = EditorGUILayout.IntField($"{field.Name} Size", list.Count);
                if (newSize != list.Count)
                {
                    while (newSize > list.Count)
                    {
                        if(elementType == typeof(string))
                            list.Add("");
                        else
                            list.Add(Activator.CreateInstance(elementType));
                    }
                    while (newSize < list.Count)
                    {
                        list.RemoveAt(list.Count - 1);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    object element = list[i];
                    EditorGUILayout.LabelField($"   {field.Name}[{i}]");
                    ShowField(elementType, element, newValue => list[i] = newValue);
                }
            }
            catch (Exception e)
            {
                EditorGUILayout.LabelField(field.Name, $"Error: {e.Message}");
            }
        }

        private void ShowClassOrStruct(FieldInfo field, object target)
        {
            object value = field.GetValue(target);
            if (value == null)
            {
                value = Activator.CreateInstance(field.FieldType);
                field.SetValue(target, value);
            }

            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField(field.Name, EditorStyles.boldLabel);
            FieldInfo[] subFields = field.FieldType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var subField in subFields)
            {
                ShowField(subField, value);
            }
            EditorGUI.indentLevel--;
        }

        private void ShowField(Type fieldType, object value, Action<object> setValue)
        {
            if (fieldType == typeof(int))
            {
                setValue(EditorGUILayout.IntField((int)value));
            }
            else if (fieldType == typeof(float))
            {
                setValue(EditorGUILayout.FloatField((float)value));
            }
            else if (fieldType == typeof(string))
            {
                setValue(EditorGUILayout.TextField((string)value));
            }
            else if (fieldType == typeof(bool))
            {
                setValue(EditorGUILayout.Toggle((bool)value));
            }
            else if (fieldType.IsEnum)
            {
                setValue(EditorGUILayout.EnumPopup((Enum)value));
            }
            else if (fieldType.IsArray)
            {
                ShowArray(fieldType, (Array)value, setValue);
            }
            else if (typeof(IList).IsAssignableFrom(fieldType))
            {
                ShowList(fieldType, (IList)value, setValue);
            }
            else if (fieldType.IsClass || fieldType.IsValueType)
            {
                ShowClassOrStruct(fieldType, value, setValue);
            }
            else
            {
                EditorGUILayout.LabelField($"Unsupported type: {fieldType}");
            }
        }

        private void ShowArray(Type elementType, Array array, Action<Array> setValue)
        {
            int newSize = EditorGUILayout.IntField("Size", array.Length);
            if (newSize != array.Length)
            {
                Array newArray = Array.CreateInstance(elementType, newSize);
                Array.Copy(array, newArray, Math.Min(array.Length, newArray.Length));
                array = newArray;
                setValue(array);
            }

            for (int i = 0; i < array.Length; i++)
            {
                object element = array.GetValue(i);
                ShowField(elementType, element, newValue => array.SetValue(newValue, i));
            }
        }

        private void ShowList(Type elementType, IList list, Action<IList> setValue)
        {
            int newSize = EditorGUILayout.IntField("Size", list.Count);
            if (newSize != list.Count)
            {
                while (newSize > list.Count)
                {
                    if(elementType == typeof(string))
                        list.Add("");
                    else
                        list.Add(Activator.CreateInstance(elementType));
                }
                while (newSize < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }
                setValue(list);
            }

            for (int i = 0; i < list.Count; i++)
            {
                object element = list[i];
                ShowField(elementType, element, newValue => list[i] = newValue);
            }
        }

        private void ShowClassOrStruct(Type fieldType, object value, Action<object> setValue)
        {
            if (value == null)
            {
                value = Activator.CreateInstance(fieldType);
                setValue(value);
            }

            EditorGUI.indentLevel++;
            FieldInfo[] subFields = fieldType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var subField in subFields)
            {
                ShowField(subField, value);
            }
            EditorGUI.indentLevel--;
        }
    }
}

#endif
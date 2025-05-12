using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BulletEffectBase), true)]
public class BulletEffectDrawer : PropertyDrawer
{
    private Dictionary<string, Type> effectTypes;

    public BulletEffectDrawer()
    {
        effectTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract && typeof(BulletEffectBase).IsAssignableFrom(type))
            .ToDictionary(type => type.Name, type => type);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.managedReferenceValue == null)
        {
            EditorGUI.LabelField(position, "Select effect type:");

            var dropdownPos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
            string[] typeNames = effectTypes.Keys.ToArray();
            int selected = -1;

            selected = EditorGUI.Popup(dropdownPos, -1, typeNames);

            if (selected >= 0)
            {
                var selectedType = effectTypes[typeNames[selected]];
                property.serializedObject.Update();
                property.managedReferenceValue = Activator.CreateInstance(selectedType);
                property.serializedObject.ApplyModifiedProperties();
            }

            return;
        }

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(position, property, true);

        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}

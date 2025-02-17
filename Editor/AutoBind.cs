using System;
using UnityEngine;

namespace AKRN_Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SelfAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class ChildAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class ParentAttribute : PropertyAttribute { }

    #if UNITY_EDITOR
    using UnityEditor;
    using System.Linq;

    [CustomPropertyDrawer(typeof(SelfAttribute))]
    [CustomPropertyDrawer(typeof(ChildAttribute))]
    [CustomPropertyDrawer(typeof(ParentAttribute))]
    public class ComponentReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue == null && !Application.isPlaying)
            {
                var target = property.serializedObject.targetObject as MonoBehaviour;

                if (target != null)
                {
                    var attributes = fieldInfo.GetCustomAttributes(false);
                    if (attributes.Any(a => a is SelfAttribute))
                    {
                        var component = target.GetComponent(fieldInfo.FieldType);
                        if (component != null)
                        {
                            property.objectReferenceValue = component;
                        }
                    }
                    else if (attributes.Any(a => a is ChildAttribute))
                    {
                        foreach (Transform child in target.transform)
                        {
                            var component = child.GetComponent(fieldInfo.FieldType);
                            if (component != null)
                            {
                                property.objectReferenceValue = component;
                                break;
                            }
                        }
                    }
                    else if (attributes.Any(a => a is ParentAttribute))
                    {
                        var parentTransform = target.transform.parent;
                        if (parentTransform != null)
                        {
                            var component = parentTransform.GetComponent(fieldInfo.FieldType);
                            if (component != null)
                            {
                                property.objectReferenceValue = component;
                            }
                        }
                    }

                    if (property.serializedObject.hasModifiedProperties)
                    {
                        property.serializedObject.ApplyModifiedProperties();
                    }
                }
            }
            EditorGUI.PropertyField(position, property, label);
        }
    }
    #endif
}

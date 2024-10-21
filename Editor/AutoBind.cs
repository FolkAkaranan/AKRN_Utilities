using System;
using UnityEngine;
using UnityEditor;

namespace AKRN_Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SelfAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class ChildAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class ParentAttribute : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(SelfAttribute))]
    [CustomPropertyDrawer(typeof(ChildAttribute))]
    [CustomPropertyDrawer(typeof(ParentAttribute))]
    public class ComponentReferenceDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue == null)
            {
                var target = property.serializedObject.targetObject as MonoBehaviour;

                if (target != null)
                {
                    if (attribute is SelfAttribute)
                    {
                        var component = target.GetComponent(fieldInfo.FieldType);
                        if (component != null)
                        {
                            property.objectReferenceValue = component;
                        }
                    }

                    else if (attribute is ChildAttribute)
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

                    else if (attribute is ParentAttribute)
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
                }
            }
            EditorGUI.PropertyField(position, property, label);
        }
    }
}

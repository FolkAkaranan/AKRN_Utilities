using System;
using UnityEngine;

namespace AKRN_Utilities
{
    public class ConditionAttribute : PropertyAttribute
    {
        public string conditionFieldName;
        public object expectedValue;

        public ConditionAttribute(string conditionFieldName, object expectedValue)
        {
            this.conditionFieldName = conditionFieldName;
            this.expectedValue = expectedValue;
        }
    }

    #if UNITY_EDITOR
    using UnityEditor;

    [CustomPropertyDrawer(typeof(ConditionAttribute))]
    public class ConditionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionAttribute conditionAttribute = (ConditionAttribute)attribute;
            SerializedProperty conditionField = property.serializedObject.FindProperty(conditionAttribute.conditionFieldName);

            if (conditionField == null)
            {
                EditorGUI.HelpBox(position, $"Cannot find field: {conditionAttribute.conditionFieldName}", MessageType.Error);
                return;
            }

            bool shouldShow = IsConditionMet(conditionField, conditionAttribute.expectedValue);

            if (shouldShow)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionAttribute conditionAttribute = (ConditionAttribute)attribute;
            SerializedProperty conditionField = property.serializedObject.FindProperty(conditionAttribute.conditionFieldName);

            if (conditionField != null && IsConditionMet(conditionField, conditionAttribute.expectedValue))
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }

            return 0;
        }

        private bool IsConditionMet(SerializedProperty conditionField, object expectedValue)
        {
            switch (conditionField.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    return conditionField.boolValue.Equals(expectedValue);
                case SerializedPropertyType.Integer:
                    return conditionField.intValue.Equals(Convert.ToInt32(expectedValue));
                case SerializedPropertyType.Float:
                    return conditionField.floatValue.Equals(Convert.ToSingle(expectedValue));
                case SerializedPropertyType.Enum:
                    return conditionField.enumValueIndex.Equals(Convert.ToInt32(expectedValue));
                case SerializedPropertyType.String:
                    return conditionField.stringValue.Equals(expectedValue.ToString());
                default:
                    return false;
            }
        }
    }
    #endif
}

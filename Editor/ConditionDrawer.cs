using UnityEngine;
using UnityEditor;

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

    [CustomPropertyDrawer(typeof(ConditionAttribute))]
    public class ConditionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionAttribute conditionAttribute = (ConditionAttribute)attribute;

            SerializedProperty conditionField = property.serializedObject.FindProperty(conditionAttribute.conditionFieldName);

            if (conditionField != null)
            {
                bool shouldShow = false;

                switch (conditionField.propertyType)
                {
                    case SerializedPropertyType.Boolean:
                        shouldShow = conditionField.boolValue.Equals(conditionAttribute.expectedValue);
                        break;
                    case SerializedPropertyType.Integer:
                        shouldShow = conditionField.intValue.Equals(conditionAttribute.expectedValue);
                        break;
                    case SerializedPropertyType.Float:
                        shouldShow = conditionField.floatValue.Equals(conditionAttribute.expectedValue);
                        break;
                    case SerializedPropertyType.Enum:
                        shouldShow = conditionField.enumValueIndex == (int)(conditionAttribute.expectedValue);
                        break;
                    case SerializedPropertyType.String:
                        shouldShow = conditionField.stringValue.Equals(conditionAttribute.expectedValue.ToString());
                        break;
                    default:
                        EditorGUI.HelpBox(position, "Condition field must be of type bool, int, float, string, or enum.", MessageType.Error);
                        return;
                }

                if (shouldShow)
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
            else
            {
                EditorGUI.HelpBox(position, $"Cannot find field: {conditionAttribute.conditionFieldName}", MessageType.Error);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionAttribute conditionAttribute = (ConditionAttribute)attribute;
            SerializedProperty conditionField = property.serializedObject.FindProperty(conditionAttribute.conditionFieldName);

            if (conditionField != null)
            {
                bool shouldShow = false;

                switch (conditionField.propertyType)
                {
                    case SerializedPropertyType.Boolean:
                        shouldShow = conditionField.boolValue.Equals(conditionAttribute.expectedValue);
                        break;
                    case SerializedPropertyType.Integer:
                        shouldShow = conditionField.intValue.Equals(conditionAttribute.expectedValue);
                        break;
                    case SerializedPropertyType.Float:
                        shouldShow = conditionField.floatValue.Equals(conditionAttribute.expectedValue);
                        break;
                    case SerializedPropertyType.Enum:
                        shouldShow = conditionField.enumValueIndex == (int)(conditionAttribute.expectedValue);
                        break;
                    case SerializedPropertyType.String:
                        shouldShow = conditionField.stringValue.Equals(conditionAttribute.expectedValue.ToString());
                        break;
                }

                return shouldShow ? EditorGUI.GetPropertyHeight(property, label) : 0;
            }

            return 0;
        }
    }
}

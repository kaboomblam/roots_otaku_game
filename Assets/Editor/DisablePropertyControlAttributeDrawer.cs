using OtakuGameJam.Attributes;
using UnityEditor;
using UnityEngine;

namespace OtakuGameJam.EditorExtensions
{
    [CustomPropertyDrawer(typeof(OtakuGameJam.Attributes.DisablePropertyControlAttribute))]
    public class DisablePropertyAttributeDrawer : PropertyDrawer
    {
        DisablePropertyControlAttribute _attribute;

        SerializedProperty _controllingBooleanProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _attribute = (DisablePropertyControlAttribute)attribute;
            _controllingBooleanProperty = property.serializedObject.FindProperty(_attribute.ControlBooleanPropertyName);
            if (_controllingBooleanProperty == null)
            {
                var noPropertyByNameText = $"Cannot find property name: \"{_attribute.ControlBooleanPropertyName}\"";

                EditorGUI.LabelField(position, label.text, noPropertyByNameText);
                return;
            }
            else if (_controllingBooleanProperty.propertyType != SerializedPropertyType.Boolean)
            {
                var notBooleanText = $"Property \"{_attribute.ControlBooleanPropertyName}\" is not a boolean";

                EditorGUI.LabelField(position, label.text, notBooleanText);
                return;
            }
            else
            {
                ToggleEnableProperty(_controllingBooleanProperty.boolValue, position, property, label);
            }
        }

        void ToggleEnableProperty(bool isEnabled, Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(isEnabled);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndDisabledGroup();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OtakuGameJam
{
    [CustomPropertyDrawer(typeof(OtakuGameJam.Attributes.DisablePropertyAttribute))]
    public class DisablePropertyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndDisabledGroup();
        }
    }
}

﻿using UnityEngine;
using UnityEditor;

namespace CreateThis.Factory.VR.UI.Button {
    [CustomEditor(typeof(KeyboardToggleButtonFactory))]
    [CanEditMultipleObjects]
    public class KeyboardToggleButtonFactoryEditor : ToggleButtonFactoryEditor {
        SerializedProperty keyboard;

        protected override void OnEnable() {
            base.OnEnable();
            keyboard = serializedObject.FindProperty("keyboard");
        }

        protected override void BuildGenerateButton() {
            if (GUILayout.Button("Generate")) {
                if (target.GetType() == typeof(KeyboardToggleButtonFactory)) {
                    KeyboardToggleButtonFactory buttonFactory = (KeyboardToggleButtonFactory)target;
                    buttonFactory.Generate();
                }
            }
        }

        protected override void AdditionalProperties() {
            base.AdditionalProperties();
            EditorGUILayout.PropertyField(keyboard);
        }
    }
}
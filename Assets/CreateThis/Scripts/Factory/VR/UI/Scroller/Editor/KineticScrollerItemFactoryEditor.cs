﻿using UnityEngine;
using UnityEditor;

namespace CreateThis.Factory.VR.UI.Scroller {
    [CustomEditor(typeof(KineticScrollerItemFactory))]
    [CanEditMultipleObjects]

    public class KineticScrollerItemFactoryEditor : Editor {
        SerializedProperty parent;
        SerializedProperty material;
        SerializedProperty highlight;
        SerializedProperty outline;
        SerializedProperty fontColor;
        SerializedProperty kineticScroller;

        protected void OnEnable() {
            parent = serializedObject.FindProperty("parent");
            material = serializedObject.FindProperty("material");
            highlight = serializedObject.FindProperty("highlight");
            outline = serializedObject.FindProperty("outline");
            fontColor = serializedObject.FindProperty("fontColor");
            kineticScroller = serializedObject.FindProperty("kineticScroller");
        }

        protected void BuildGenerateButton() {
            // Take out this if statement to set the value using setter when ever you change it in the inspector.
            // But then it gets called a couple of times when ever inspector updates
            // By having a button, you can control when the value goes through the setter and getter, your self.
            if (GUILayout.Button("Generate")) {
                if (target.GetType() == typeof(KineticScrollerItemFactory)) {
                    KineticScrollerItemFactory factory = (KineticScrollerItemFactory)target;
                    factory.Generate();
                }
            }
        }

        protected void AdditionalProperties() {
            // put your properties here in the override
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(parent);
            EditorGUILayout.PropertyField(material);
            EditorGUILayout.PropertyField(highlight);
            EditorGUILayout.PropertyField(outline);
            EditorGUILayout.PropertyField(fontColor);
            EditorGUILayout.PropertyField(kineticScroller);

            AdditionalProperties();

            serializedObject.ApplyModifiedProperties();

            BuildGenerateButton();
        }
    }
}

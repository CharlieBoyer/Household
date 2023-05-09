using UnityEditor;
using UnityEngine;

using Wave;

namespace Editor
{
    [CustomEditor(typeof(WaveObject))]
    public class WaveObjectEditor : UnityEditor.Editor
    {
        private SerializedProperty durationProp;
        private SerializedProperty weatherProp;
        private SerializedProperty foesTypesProp;
        private SerializedProperty foesNumbersProp;
        private SerializedProperty addModifierProp;
        private SerializedProperty modifierProp;

        private bool showFoesFoldout = true;

        private void OnEnable()
        {
            durationProp = serializedObject.FindProperty("duration");
            weatherProp = serializedObject.FindProperty("weather");
            foesTypesProp = serializedObject.FindProperty("foesTypes");
            foesNumbersProp = serializedObject.FindProperty("foesNumbers");
            addModifierProp = serializedObject.FindProperty("addModifier");
            modifierProp = serializedObject.FindProperty("modifier");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(durationProp);
            EditorGUILayout.PropertyField(weatherProp);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Wave Composition", EditorStyles.boldLabel);
            showFoesFoldout = EditorGUILayout.Foldout(showFoesFoldout,"Foes");
            if (showFoesFoldout)
            {
                EditorGUI.indentLevel++;

                int listSize = foesTypesProp.arraySize;

                for (int i = 0; i < listSize; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    // Draw the FoesType property field
                    SerializedProperty foesTypeProp = foesTypesProp.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(foesTypeProp, GUIContent.none, GUILayout.ExpandWidth(true));

                    // Draw the FoesNumber property field
                    SerializedProperty foesNumberProp = foesNumbersProp.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(foesNumberProp, GUIContent.none, GUILayout.Width(80f));

                    // Draw the Remove Foe Type button
                    if (GUILayout.Button("X", GUILayout.Width(20f)))
                    {
                        foesTypesProp.DeleteArrayElementAtIndex(i);
                        foesNumbersProp.DeleteArrayElementAtIndex(i);
                        listSize--;
                        i--;
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Foe Type"))
                {
                    foesTypesProp.arraySize++;
                    foesNumbersProp.arraySize++;
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(addModifierProp);
            if (addModifierProp.boolValue)
            {
                EditorGUILayout.PropertyField(modifierProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

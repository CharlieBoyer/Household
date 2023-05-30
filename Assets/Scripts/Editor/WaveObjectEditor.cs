using UnityEditor;
using UnityEngine;

using Wave;

namespace Editor
{
    [CustomEditor(typeof(WaveObject))]
    public class WaveObjectEditor : UnityEditor.Editor
    {
        private SerializedProperty _startTimeProp;
        private SerializedProperty _durationProp;
        private SerializedProperty _weatherProp;
        private SerializedProperty _foesTypesProp;
        private SerializedProperty _foesNumbersProp;
        private SerializedProperty _spawnRateProp;
        private SerializedProperty _addModifierProp;
        private SerializedProperty _modifierProp;

        private bool _showFoesFoldout = true;

        private void OnEnable()
        {
            _startTimeProp = serializedObject.FindProperty("startTime");
            _durationProp = serializedObject.FindProperty("duration");
            _weatherProp = serializedObject.FindProperty("weather");
            _foesTypesProp = serializedObject.FindProperty("foesTypes");
            _foesNumbersProp = serializedObject.FindProperty("foesNumbers");
            _spawnRateProp = serializedObject.FindProperty("spawnRate");
            _addModifierProp = serializedObject.FindProperty("addModifier");
            _modifierProp = serializedObject.FindProperty("modifier");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Wave Parameters", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_startTimeProp);
            EditorGUILayout.PropertyField(_durationProp);
            EditorGUILayout.PropertyField(_weatherProp);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Wave Composition", EditorStyles.boldLabel);
            _showFoesFoldout = EditorGUILayout.Foldout(_showFoesFoldout,"Foes");
            if (_showFoesFoldout)
            {
                EditorGUI.indentLevel++;

                int listSize = _foesTypesProp.arraySize;

                for (int i = 0; i < listSize; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    // Draw the FoesType property field
                    SerializedProperty foesTypeProp = _foesTypesProp.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(foesTypeProp, GUIContent.none, GUILayout.ExpandWidth(true));

                    // Draw the FoesNumber property field
                    SerializedProperty foesNumberProp = _foesNumbersProp.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(foesNumberProp, GUIContent.none, GUILayout.Width(80f));

                    // Draw the Remove Foe Type button
                    if (GUILayout.Button("X", GUILayout.Width(20f)))
                    {
                        _foesTypesProp.DeleteArrayElementAtIndex(i);
                        _foesNumbersProp.DeleteArrayElementAtIndex(i);
                        listSize--;
                        i--;
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Foe Type"))
                {
                    _foesTypesProp.arraySize++;
                    _foesNumbersProp.arraySize++;
                }

                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.PropertyField(_spawnRateProp);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_addModifierProp);
            if (_addModifierProp.boolValue)
            {
                EditorGUILayout.PropertyField(_modifierProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

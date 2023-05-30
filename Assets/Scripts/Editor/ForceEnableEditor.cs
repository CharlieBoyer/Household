using UnityEditor;
using Utility;

namespace Editor
{
    [CustomEditor(typeof(ForceEnable))]
    public class ForceEnableEditor: UnityEditor.Editor
    {
        private SerializedProperty _activeProp;
        private SerializedProperty _objectsToEnableProp;
        
        private void OnEnable()
        {
            _activeProp = serializedObject.FindProperty("activate");
            _objectsToEnableProp = serializedObject.FindProperty("objectsToEnable");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_activeProp);
            if (_activeProp.boolValue) {
                EditorGUILayout.HelpBox("Warning: Enforced object(s) activation", MessageType.Warning);
            }
            
            EditorGUI.BeginDisabledGroup(!_activeProp.boolValue);

            if (_activeProp.boolValue)
                EditorGUILayout.PropertyField(_objectsToEnableProp, true);
            else
                _objectsToEnableProp.ClearArray();

            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            SceneView.RepaintAll();
        }
    }
}

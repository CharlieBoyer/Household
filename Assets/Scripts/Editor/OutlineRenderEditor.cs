using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Utility;

namespace Editor
{
    [CustomEditor(typeof(OutlineRender))]
    public class OutlineRenderEditor : UnityEditor.Editor
    {
        private SerializedProperty _enableProp;
        private SerializedProperty _renderAllCollidersProp;
        private SerializedProperty _renderListProp;

        private void OnEnable()
        {
            _enableProp = serializedObject.FindProperty("enable");
            _renderAllCollidersProp = serializedObject.FindProperty("renderAllColliders");
            _renderListProp = serializedObject.FindProperty("renderList");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_enableProp);
            if (!_enableProp.boolValue) {
                EditorGUILayout.HelpBox("Outline rendering is disabled", MessageType.Info);
            }
            else {
                EditorGUILayout.HelpBox("Warning: OutlineRender utility enabled (DEV ONLY)", MessageType.Warning);
            }
 
            EditorGUI.BeginDisabledGroup(!_enableProp.boolValue);

            EditorGUILayout.PropertyField(_renderAllCollidersProp);

            if (!_renderAllCollidersProp.boolValue)
            {
                EditorGUILayout.PropertyField(_renderListProp, true);
            }
            else
            {
                _renderListProp.ClearArray();
            }

            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            UnityEditor.SceneView.RepaintAll();
        }

        private void OnSceneGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            if (_enableProp.boolValue)
            {
                if (!_renderAllCollidersProp.boolValue)
                {
                    for (int i = 0; i < _renderListProp.arraySize; i++)
                    {
                        SerializedProperty itemProp = _renderListProp.GetArrayElementAtIndex(i);
                        GameObject obj = itemProp.objectReferenceValue as GameObject;
                        if (obj == null)
                        {
                            continue;
                        }

                        Renderer componentRenderer = obj.GetComponent<Renderer>();
                        if (componentRenderer != null)
                        {
                            Bounds bounds = componentRenderer.bounds;
                            Handles.DrawWireCube(bounds.center, bounds.size);
                        }
                        else
                        {
                            Collider componentCollider = obj.GetComponent<Collider>();
                            if (componentCollider != null)
                            {
                                Bounds bounds = componentCollider.bounds;
                                Handles.DrawWireCube(bounds.center, bounds.size);
                            }
                        }
                    }
                }
                else
                {
                    List<GameObject> allObjects = new List<GameObject>(FindObjectsOfType<Collider>().Select(c => c.gameObject).ToArray());
                    foreach (GameObject obj in allObjects)
                    {
                        Renderer componentRenderer = obj.GetComponent<Renderer>();
                        if (componentRenderer != null)
                        {
                            Bounds bounds = componentRenderer.bounds;
                            Handles.DrawWireCube(bounds.center, bounds.size);
                        }
                        else
                        {
                            Collider componentCollider = obj.GetComponent<Collider>();
                            if (componentCollider != null)
                            {
                                Bounds bounds = componentCollider.bounds;
                                Handles.DrawWireCube(bounds.center, bounds.size);
                            }
                        }
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Outline rendering is disabled", MessageType.Info);
            }

            if (EditorGUI.EndChangeCheck())
            {
                UnityEditor.SceneView.RepaintAll();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

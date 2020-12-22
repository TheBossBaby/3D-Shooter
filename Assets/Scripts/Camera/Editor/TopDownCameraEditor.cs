using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine.UIElements;

namespace PM.Camera
{
    [CustomEditor(typeof(TopDownCameraController))]
    public class TopDownCameraEditor : Editor
    {
        private TopDownCameraController _targetCamera;

        private void OnEnable()
        {
            _targetCamera = (TopDownCameraController)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        private void OnSceneGUI()
        {
            if (!_targetCamera.m_target)
            {
                return;
            }
            Transform camTarget = _targetCamera.m_target;
            
            Handles.color = new Color(1, 0, 0, 0.15f);
            Handles.DrawSolidDisc(_targetCamera.m_target.position, Vector3.up, _targetCamera.m_distance);
            
            Handles.color = new Color(1, 1, 0, 0.75f);
            Handles.DrawSolidDisc(_targetCamera.m_target.position, Vector3.up, _targetCamera.m_distance);

            Handles.color = new Color(1, 0, 0, 0.5f);
            _targetCamera.m_distance = Handles.ScaleSlider(_targetCamera.m_distance, camTarget.position, -camTarget.forward, Quaternion.identity, _targetCamera.m_distance,1f);
            _targetCamera.m_distance = Mathf.Clamp(_targetCamera.m_distance, 5f, float.MaxValue);
            
            Handles.color = new Color(0, 0, 1, 0.5f);
            _targetCamera.m_height = Handles.ScaleSlider(_targetCamera.m_height, camTarget.position, Vector3.up, Quaternion.identity, _targetCamera.m_height, 1f);
            _targetCamera.m_height = Mathf.Clamp(_targetCamera.m_height, 5f, float.MaxValue);

            GUIStyle lableStyle = new GUIStyle();
            lableStyle.fontSize = 15;
            lableStyle.normal.textColor = Color.white;
            lableStyle.alignment = TextAnchor.UpperCenter;

            Handles.Label(camTarget.position + (-camTarget.forward * _targetCamera.m_distance), "Distance", lableStyle);
            Handles.Label(camTarget.position + (Vector3.up * _targetCamera.m_height), "Height", lableStyle);
        }
    }
}
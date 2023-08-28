using UnityEditor;
using UnityEngine;

namespace FluentFuzzy.Unity.Editor
{
    [CustomEditor(typeof(Curve))]
    public class CurveEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            base.OnInspectorGUI();
            GUILayout.EndHorizontal();
        }
    }
}
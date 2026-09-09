using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

#if UNITY_EDITOR
[CustomEditor(typeof(CubeBehavior)), CanEditMultipleObjects]
public class CubeBehaviorEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
    
        if (GUILayout.Button("Select all cubes")) {
            var allCubeBehavior = GameObject.FindObjectsOfType<CubeBehavior>();
            var allCubeBehaviorObjects = allCubeBehavior
                .Select(cube => cube.gameObject)
                .ToArray();
            Selection.objects = allCubeBehaviorObjects;
        }
    }
}
#endif
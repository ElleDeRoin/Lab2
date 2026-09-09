using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

// This Editor script adds selection and clear buttons to all GameObjects that have the SphereBehavior script attached to them
#if UNITY_EDITOR
[CustomEditor(typeof(SphereBehavior)), CanEditMultipleObjects]
public class SphereBehaviorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using(new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all Spheres"))
            {
                var allSphereBehavior = GameObject.FindObjectsOfType<SphereBehavior>();
                var allSphereGameObjects = allSphereBehavior
                    .Select(sphere => sphere.gameObject)
                    .ToArray();
                Selection.objects = allSphereGameObjects;
            }
            // draws clear selection button
            if (GUILayout.Button("Clear Selection"))
            {
                Selection.objects = new Object[]
                {
                (target as SphereBehavior).gameObject
                };
            }
        }        
    }

}
#endif
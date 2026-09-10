using UnityEngine;
using UnityEditor; // needed to customizer unity editor
using System.Linq; // needed for methods like Select and ToArray

//file only gets compiled when inside unity editor
#if UNITY_EDITOR
// customizes how CubeBehavior looks in inspector
[CustomEditor(typeof(CubeBehavior)), CanEditMultipleObjects]
public class CubeBehaviorEditor : Editor
{
    public override void OnInspectorGUI() {
        //draws default inspector fields first
        base.OnInspectorGUI();

        // creates a button labeled select all cubes
        // code inside runs on frame button is clicked
        if (GUILayout.Button("Select all cubes")) {
            // find objects that have script attached
            var allCubeBehavior = GameObject.FindObjectsOfType<CubeBehavior>();
            // selects game objects those scripts are attatched to
            var allCubeBehaviorObjects = allCubeBehavior
                .Select(cube => cube.gameObject)
                .ToArray();
            // highlights game objects in scene
            Selection.objects = allCubeBehaviorObjects;
        }

        //creates second button labeled clear selection
        if (GUILayout.Button("Clear Selection")) {
            //deselects all objects other than one currently looking at
            Selection.objects = new Object[] {
                (target as CubeBehavior).gameObject
            };
        }
    }

}
#endif
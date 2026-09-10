using UnityEngine;
using UnityEditor; // needed to customizer unity editor
using System.Linq; // needed for methods like Select and ToArray

//file only gets compiled when inside unity editor
#if UNITY_EDITOR
// customizes how CubeBehavior looks in inspector
[CustomEditor(typeof(CubeBehavior)), CanEditMultipleObjects]
public class CubeBehaviorEditor : Editor 
{
<<<<<<< HEAD
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
=======
    // Static variable keeps track of the toggle state across selection changes
    private static bool _cubesEnabled = true; 

    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();

        // Keep GUI Color only on correct Inspector
        Color originalColor = GUI.backgroundColor; 

        using (new EditorGUILayout.HorizontalScope()) 
        {
            // Selects all cube gameobjects in the scene
            if (GUILayout.Button("Select all")) 
            {
                var allCubeBehavior = Object.FindObjectsByType<CubeBehavior>(FindObjectsSortMode.None);
                var allCubeGameObjects = allCubeBehavior
                    .Select(cube => cube.gameObject)
                    .ToArray();
                Selection.objects = allCubeGameObjects;
            }

            // Resets selection to just the current target
            if (GUILayout.Button("Reset Selection")) 
            {
                Selection.objects = new Object[] { ((CubeBehavior)target).gameObject };
            }

            // Toggles active state for all cubes
            GUI.backgroundColor = _cubesEnabled ? Color.green : Color.red;

            if (GUILayout.Button("Toggle All Cubess")) 
            {
                // Invert the tracking state
                _cubesEnabled = !_cubesEnabled; 

                var allCubes = Object.FindObjectsByType<CubeBehavior>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                foreach (var cube in allCubes) 
                {
                    cube.gameObject.SetActive(_cubesEnabled);
                    
                    // Mark the object as dirty so Unity knows a change happened
                    EditorUtility.SetDirty(cube.gameObject);
                }
            }
        }

        // Restore original background color for the rest of the UI
        GUI.backgroundColor = originalColor; 
>>>>>>> ElleBranch
    }

}
#endif

using UnityEngine;
using UnityEditor;
using System.Linq;

#if UNITY_EDITOR
[CustomEditor(typeof(CubeBehavior)), CanEditMultipleObjects]
public class CubeBehaviorEditor : Editor 
{
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
    }
}
#endif

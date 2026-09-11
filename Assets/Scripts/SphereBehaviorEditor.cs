using UnityEngine;
using UnityEditor;
using System.Linq;

#if UNITY_EDITOR
[CustomEditor(typeof(SphereBehavior)), CanEditMultipleObjects]
public class SphereBehaviorEditor : Editor 
{
    // Static variable keeps track of the toggle state across selection changes
    private static bool _spheresEnabled = true; 

    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();

        // Keep GUI Color only on correct Inspector
        Color originalColor = GUI.backgroundColor; 

        using (new EditorGUILayout.HorizontalScope()) 
        {
            // Selects all sphere gameobjects in the scene
            if (GUILayout.Button("Select all")) 
            {
                var allSphereBehavior = Object.FindObjectsByType<SphereBehavior>(FindObjectsSortMode.None);
                var allSphereGameObjects = allSphereBehavior
                    .Select(sphere => sphere.gameObject)
                    .ToArray();
                Selection.objects = allSphereGameObjects;
            }

            // Resets selection to just the current target
            if (GUILayout.Button("Reset Selection")) 
            {
                Selection.objects = new Object[] { ((SphereBehavior)target).gameObject };
            }

            // Toggles active state for all spheres
            GUI.backgroundColor = _spheresEnabled ? Color.green : Color.red;

            if (GUILayout.Button("Toggle All Spheres")) 
            {
                // Invert the tracking state
                _spheresEnabled = !_spheresEnabled; 

                var allSpheres = Object.FindObjectsByType<SphereBehavior>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                foreach (var sphere in allSpheres) 
                {
                    sphere.gameObject.SetActive(_spheresEnabled);
                    
                    // Mark the object as dirty so Unity knows a change happened
                    EditorUtility.SetDirty(sphere.gameObject);
                }
            }
        }

        // Restore original background color for the rest of the UI
        GUI.backgroundColor = originalColor; 
    }
}
#endif

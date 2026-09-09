using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//This script allows the user to change the size of a cube or sphere and will give them a warning if the size is too big or small
public class SizeConfig : MonoBehaviour 
{
    //Logic for changing the scale
    [SerializeField] private float size = 1f;

    public float Size => size;

    private void OnValidate() 
    {
        transform.localScale = new Vector3(size, size, size);
    }
}

//Gives a warning if the cube is too big or if the sphere is too small by looking at their meshfilters and current size in the inspector
#if UNITY_EDITOR
[CustomEditor(typeof(SizeConfig))]
public class SizeConfigEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        DrawDefaultInspector();

        SizeConfig script = (SizeConfig)target;
        float size = script.Size;

        MeshFilter meshFilter = script.GetComponent<MeshFilter>();
        string meshName = (meshFilter != null && meshFilter.sharedMesh != null) 
            ? meshFilter.sharedMesh.name.ToLower() 
            : "";

        bool isCube = meshName.Contains("cube");
        bool isSphere = meshName.Contains("sphere");

        if (isCube && size > 2) 
        {
            EditorGUILayout.HelpBox("The cubes' sizes cannot be bigger than 2", MessageType.Warning);
        }

        if (isSphere && size < 1) 
        {
            EditorGUILayout.HelpBox("The spheres' radius cannot be smaller than 1!", MessageType.Warning);
        }
    }
}
#endif
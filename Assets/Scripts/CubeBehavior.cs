using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    public float size = 1f;

    private void Update() {
        transform.localScale = Vector3.one * size;
    }
}

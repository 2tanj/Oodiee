using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleSpawner))]
public class ObstacleSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spawn obstacles"))
            ObstacleSpawner.Instance.SpawnObstacles();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[ExecuteInEditMode]
public class TestingObstacles : MonoBehaviour
{
    private Bounds _bounds;

    [SerializeField]
    private float _circleRadius = .5f;

    void Start() =>
        _bounds = GetComponent<MeshFilter>().sharedMesh.bounds;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 start = default, end;
        start.z = _bounds.center.z + _bounds.extents.z;
        start.x = _bounds.center.x;
        start.y = _bounds.center.y;

        end = start;
        end.z = _bounds.center.z - _bounds.extents.z;


        Gizmos.DrawWireSphere(transform.TransformPoint(start), _circleRadius);
        Gizmos.DrawWireSphere(transform.TransformPoint(end), _circleRadius);
    }

    [ContextMenu("Generate start and end")]
    private void Testing()
    {
        // getting start/end pos
        Vector3 start = default, end;
        start.z = _bounds.center.z + _bounds.extents.z;
        start.x = _bounds.center.x;
        start.y = _bounds.center.y;

        end = start;
        end.z = _bounds.center.z - _bounds.extents.z;

        start = transform.TransformPoint(start);
        end   = transform.TransformPoint(end);

        // spawning start/end
        var obj = new GameObject();

        var startObj = Instantiate(obj, start, transform.rotation, transform);
        startObj.name = "Start";

        var endObj = Instantiate(obj, end, transform.rotation, transform);
        endObj.name = "End";
        endObj.transform.Rotate(180, 0, 0);

        DestroyImmediate(obj);

        // creating prefab
        if (!Directory.Exists("Assets/_Prefabs/TestingTubes"))
            AssetDatabase.CreateFolder("Assets/_Prefabs", "TestingTubes");
        if (Directory.GetFiles("Assets/_Prefabs/TestingTubes", name + ".prefab").Length > 0)
        {
            Debug.LogError($"{name}.prefab ALREADY EXISTS, PREFAB GENERATION STOPPED!");
            DestroyImmediate(startObj);
            DestroyImmediate(endObj);
            return;
        }
        
        string localPath = "Assets/_Prefabs/TestingTubes/" + name + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(gameObject, localPath);

        DestroyImmediate(startObj);
        DestroyImmediate(endObj);

        Debug.Log($"Succesfully created a prefab with a generated start/end: [{localPath}]");
    }
}

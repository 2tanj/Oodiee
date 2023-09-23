using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[ExecuteInEditMode]
public class TestingObstacles : MonoBehaviour
{
    private const float TUBE_RADIUS = 0.06590513f;
    
    private Bounds _bounds;

    [SerializeField]
    private float _circleRadius = .02f;

    void Start() =>
        _bounds = GetComponent<MeshFilter>().sharedMesh.bounds;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 start = default;
        List<Vector3> ends;
        YShapedStartEnd(out start, out ends);
        
        Gizmos.DrawWireSphere(start, _circleRadius);

        if (ends is null || ends.Count < 1)
            return;
        ends.ForEach(e => Gizmos.DrawWireSphere(e, _circleRadius));
    }

    [ContextMenu("Generate start and end")]
    private void Testing()
    {
        // getting start/end pos
        Vector3 start = default;
        List<Vector3> ends;
        YShapedStartEnd(out start, out ends);

        // spawning start/end
        var obj = new GameObject();

        var startObj = Instantiate(obj, start, transform.rotation, transform);
        startObj.name = "Start";

        List<GameObject> endsObj = new List<GameObject>();
        for (int i = 0; i < ends.Count; i++)
        {
            var endObj = Instantiate(obj, ends[i], transform.rotation, transform);
            endObj.name = "End" + (i + 1);
            endObj.transform.Rotate(180, 0, 0);

            endsObj.Add(endObj);
        }

        DestroyImmediate(obj);

        // creating prefab
        if (!Directory.Exists("Assets/_Prefabs/TestingTubes"))
            AssetDatabase.CreateFolder("Assets/_Prefabs", "TestingTubes");
        if (Directory.GetFiles("Assets/_Prefabs/TestingTubes", name + ".prefab").Length > 0)
        {
            Debug.LogError($"{name}.prefab ALREADY EXISTS, PREFAB GENERATION STOPPED!");
            DestroyImmediate(startObj);
            endsObj.ForEach(obj => DestroyImmediate(obj));
            return;
        }
        
        string localPath = "Assets/_Prefabs/TestingTubes/" + name + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(gameObject, localPath);

        DestroyImmediate(startObj);
        endsObj.ForEach(obj => DestroyImmediate(obj));

        Debug.Log($"Succesfully created a prefab with a generated start/end: [{localPath}]");
    }

    private void DefaultStartEnd(out Vector3 start, out List<Vector3> ends)
    {
        start.z = _bounds.center.z + _bounds.extents.z;
        start.x = _bounds.center.x;
        start.y = _bounds.center.y;

        var end = start;
        end.z = _bounds.center.z - _bounds.extents.z;

        start = transform.TransformPoint(start);
        end = transform.TransformPoint(end);

        ends = new List<Vector3>() { end };
    }

    private void YShapedStartEnd(out Vector3 start, out List<Vector3> ends)
    {
        start.z = _bounds.center.z + _bounds.extents.z;
        start.x = _bounds.center.x;
        start.y = _bounds.center.y;

        var end1 = start;
        end1.z = _bounds.center.z - _bounds.extents.z;
        end1.x = _bounds.center.x + _bounds.extents.x - TUBE_RADIUS;
        
        var end2 = start;
        end2.z = _bounds.center.z - _bounds.extents.z;
        end2.x = _bounds.center.x - _bounds.extents.x + TUBE_RADIUS;

        start = transform.TransformPoint(start);
        end1 = transform.TransformPoint(end1);
        end2 = transform.TransformPoint(end2);

        ends = new List<Vector3>() { end1, end2 };
    }
}

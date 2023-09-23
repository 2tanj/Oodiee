using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[ExecuteInEditMode]
public class ObstaclePrefabGenerator : MonoBehaviour
{
    // TODO:
    // ADD LIST OF FUNCTIONS(for types of tubes)
    // YOU SELECT THE ONE YOU WANT IN INSPECTOR
    // AND USE THAT ONE WHEN GENERATING PREFAB

    // maybe remvoe ObstaclePrefabGenerator component from final prefab

    private const float TUBE_RADIUS = 0.06590513f;
    
    private Bounds _bounds;

    [SerializeField]
    private float _circleRadius = .02f;

    [SerializeField]
    private string _folderName = "TestingTubes";

    void Start() =>
        _bounds = GetComponent<MeshFilter>().sharedMesh.bounds;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 start = default;
        List<Vector3> ends;
        DefaultStartEnd(out start, out ends);
        
        Gizmos.DrawWireSphere(start, _circleRadius);

        if (ends is null || ends.Count < 1)
            return;
        ends.ForEach(e => Gizmos.DrawWireSphere(e, _circleRadius));
    }

    [ContextMenu("Generate prefab with a start and end ")]
    private void GeneratePrefab()
    {
        // setting up the object
        transform.position   = Vector3.zero;
        transform.rotation   = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.AddComponent<Obstacle>();

        // getting start/end pos
        Vector3 start = default;
        List<Vector3> ends;
        DefaultStartEnd(out start, out ends);

        // spawning start/end
        var obj = new GameObject(); // instancing an empty game object to use when instanciating, we destroy it when done

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
        if (!Directory.Exists("Assets/_Prefabs/" + _folderName))
            AssetDatabase.CreateFolder("Assets/_Prefabs", _folderName);

        if (Directory.GetFiles("Assets/_Prefabs/" + _folderName, name + ".prefab").Length > 0)
        {
            Debug.LogError($"{name}.prefab ALREADY EXISTS, PREFAB GENERATION STOPPED!");
            DestroyImmediate(startObj);
            endsObj.ForEach(obj => DestroyImmediate(obj));

            return;
        }
        
        string localPath = $"Assets/_Prefabs/{_folderName}/" + name + ".prefab";
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

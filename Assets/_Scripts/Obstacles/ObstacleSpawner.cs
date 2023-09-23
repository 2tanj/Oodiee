using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    [SerializeField]
    List<Obstacle> _obstacles;
    private Obstacle _lastObstacle = null;

    [SerializeField, Range(1, 100)]
    private uint _amountToSpawn = 10;

    private void Awake() => Instance = this;

    [ContextMenu("Create obstacles")]
    public void SpawnObstacles()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
            DestroyImmediate(transform.GetChild(0).gameObject);

        for (int i = 0; i < _amountToSpawn; i++)
        {
            if (i == 0)
                _lastObstacle = Instantiate(_obstacles[0], Vector3.zero, Quaternion.identity, transform);
            else
                SpawnObstacle(_obstacles[Random.Range(0, _obstacles.Count)]);
        }
    }

    private void SpawnObstacle(Obstacle obstacle)
    {
        int childNum = _lastObstacle.name == "SplitTube(Clone)" ? Random.Range(1, 3) : 1;
        var spawnPos = _lastObstacle.transform.GetChild(childNum).transform.position;

        var obj = Instantiate(obstacle, Vector3.zero, Quaternion.identity, transform);

        var newStartPost = obj.transform.GetChild(0).transform.position;
        obj.transform.position -= (newStartPost - spawnPos);

        _lastObstacle = obj;
    }
}

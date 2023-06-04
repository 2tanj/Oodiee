using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Obstacle> _obstacles;

    [SerializeField]
    private float _distance = 22.22f, _amountToSpawn = 30, _distanceToDespawn, _spawnDistanceIncrement = 500;
    private float _distanceToSpawn = 0;

    [SerializeField, Range(0,1)]
    private float _powerUpChance = .3f;

    Vector3 nextPos;

    private void Start()
    {
        nextPos =  new Vector3(0, 14.5f, 0);

        SpawnObstacles();
    }

    private void Update()
    {
        if (PlayerController.Instance.transform.position.y >= _distanceToSpawn)
        {
            SpawnObstacles();
            _distanceToSpawn += _spawnDistanceIncrement;
        }
    }

    private void SpawnObstacles()
    {
        for (int i = 0; i < _amountToSpawn; i++)
        {
            var obj = Instantiate(_obstacles[Random.Range(0, _obstacles.Count - 1)], nextPos, Quaternion.identity, transform);
            obj.transform.Rotate(-90, 0, 0);
            obj.transform.localScale = new Vector3(100, 100, 100);

            nextPos.y += _distance;
        }

        Debug.Log("Obstacle count: " + transform.childCount);
    }
}

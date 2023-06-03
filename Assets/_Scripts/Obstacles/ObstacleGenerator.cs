using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefab;

    [SerializeField]
    private float _distance = 22.22f, _amountToSpawn = 30, _distanceToDespawn, _spawnDistanceIncrement = 500;
    private float _distanceToSpawn = 0;

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
            var obj = Instantiate(_prefab[Random.Range(0, _prefab.Length)], nextPos, Quaternion.identity, transform);
            obj.transform.Rotate(-90, 0, 0);
            obj.transform.localScale = new Vector3(100, 100, 100);
            nextPos.y += _distance;
        }


        Debug.Log("Obstacle count: " + transform.childCount);
    }
}

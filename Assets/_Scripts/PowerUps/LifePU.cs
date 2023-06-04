using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePU : MonoBehaviour, IPowerUp
{
    public static LifePU Instance;

    [SerializeField]
    private GameObject _fracturedPrefab, _heartMesh;

    public bool HasBonusLife { get; set; } = false;

    //private bool _hasBeenUsed = false;

    private MeshRenderer _mesh;
    private SphereCollider _collider;

    void Awake() =>
        Instance = this;

    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyHeart();
        }
    }

    public void PickUp()
    {
        if (PlayerController.Instance.HasRevived)
            return;

        HasBonusLife = true;
        PlayerController.Instance.HasRevived = true;

        _mesh.enabled = false;
        _collider.enabled = false;

        _heartMesh.SetActive(true);
    }

    public void DestroyHeart()
    {
        HasBonusLife = false;

        var instance = Instantiate(_fracturedPrefab, _heartMesh.transform.position, Quaternion.identity/*_heartMesh.transform.rotation*/, PlayerController.Instance.transform);
        Destroy(_heartMesh.gameObject);
        foreach (var r in instance.GetComponentsInChildren<Rigidbody>())
        {
            var force = (r.transform.position - transform.position).normalized * 750f;
            r.AddForce(force);
        }

        //Destroy(instance);
    }
}
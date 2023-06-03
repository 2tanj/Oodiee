using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPU : MonoBehaviour, IPowerUp
{
    public static ShieldPU Instance;
    public bool IsShielded { get; private set; }

    [SerializeField]
    private float _duration;

    private MeshRenderer _mesh;
    private SphereCollider _collider;

    void Awake() =>
        Instance = this;

    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _collider = GetComponent<SphereCollider>();
    }

    public void PickUp()
    {
        IsShielded = true;
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        _mesh.enabled = false;
        _collider.enabled = false;
        
        yield return new WaitForSeconds(_duration);
        IsShielded = false;

        Destroy(gameObject);
    }
}

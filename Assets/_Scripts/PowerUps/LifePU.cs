using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePU : IAudioPlayer, IPowerUp
{
    public static LifePU Instance;

    [SerializeField]
    private GameObject _fracturedPrefab;

    [SerializeField]
    private AudioClip _sound, _shatterSound;

    public static bool HasBonusLife { get; set; } = false;

    //private bool _hasBeenUsed = false;

    private MeshRenderer _mesh;
    private SphereCollider _collider;

    void Awake()
    {
        Instance = this;
        SetupAudio();
    }

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

        PlaySound(_sound);

        HasBonusLife = true;
        PlayerController.Instance.HasRevived = true;

        _mesh.enabled = false;
        _collider.enabled = false;

        PlayerController.Instance.HeartMesh.SetActive(true);
    }

    public void DestroyHeart()
    {
        HasBonusLife = false;

        //var instance = Instantiate(_fracturedPrefab, _heartMesh.transform.position, Quaternion.identity/*_heartMesh.transform.rotation*/, PlayerController.Instance.transform);
        PlayerController.Instance.HeartMesh.SetActive(false);
        PlaySound(_shatterSound);
        //foreach (var r in instance.GetComponentsInChildren<MeshCollider>())
        //{
        //    var force = (r.transform.position - transform.position).normalized * 750f;
        //    r.GetComponent<Rigidbody>().AddForce(force);
        //}

        //StartCoroutine(DestroyShatter(instance));
    }

    private IEnumerator DestroyShatter(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(obj.gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SizeReductionPU : MonoBehaviour, IPowerUp
{
    [SerializeField]
    private float _duration, _reductionAmount, _numOfBoops = 3;

    private MeshRenderer _mesh;
    private SphereCollider _collider;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _collider = GetComponent<SphereCollider>();
    }

    public void PickUp()
    {
        PlayerController.Instance.transform.DOScale(PlayerController.Instance.transform.localScale / _reductionAmount, .2f);
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        _mesh.enabled = false;
        _collider.enabled = false;
        yield return new WaitForSeconds(_duration);

        var sequence = DOTween.Sequence();

        for (int i = 0; i < _numOfBoops; i++)
        {
            sequence.Append(PlayerController.Instance.transform.DOPunchScale(
            punch: new Vector3(_reductionAmount * .1f, _reductionAmount * .1f, _reductionAmount * .1f),
            duration: .3f,
            vibrato: 5,
            elasticity: 1.2f));
        }

        //PlayerController.Instance.transform.DOScale(PlayerController.Instance.transform.localScale * _reductionAmount, .2f);
        sequence.OnComplete(() => PlayerController.Instance.transform.DOScale(PlayerController.Instance.transform.localScale * _reductionAmount, .2f));

        Destroy(gameObject);
    }
}

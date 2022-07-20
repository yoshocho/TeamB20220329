using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorGImmick : GimmickBase
{
    [SerializeField]
    Transform _targetTrans = default;
    [SerializeField]
    float _speed = 2.0f;
    Vector3 _defaultPos = default;
    Vector3 _targetPos = default;
    [SerializeField]
    float MoveHeight ;

    private void Start()
    {
        _defaultPos = transform.position;
        _targetPos = _targetTrans.position;
    }
    public override void GimmicOn()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(MoveHeight, _speed));
        seq.AppendInterval(1.0f);
        seq.Play();
        Debug.Log("GimmicOn");


    }

    public override void GimmicEnd()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(_defaultPos, _speed));
        Debug.Log("GimmicEnd");

    }
}

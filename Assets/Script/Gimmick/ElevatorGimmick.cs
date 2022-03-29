using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorGimmick : GimmickBase
{
    [SerializeField]
    Transform _targetTrans = default;
    [SerializeField]
    float _speed = 2.0f;
    Vector3 _defaultPos = default;
    Vector3 _targetPos = default;
    private void Start()
    {
        _defaultPos = transform.position;
        _targetPos = _targetTrans.position;
    }
    public override void GimmicOn()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(_targetPos, _speed));
        seq.AppendInterval(1.0f);
        seq.Play();
    }

    public override void GimmicEnd()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(_defaultPos,_speed));
    }

    public override void Gimmic(bool flag)
    {
        Sequence seq = DOTween.Sequence();
        if (flag)
        {
            seq.Append(transform.DOMove(_targetPos, _speed))
                .OnComplete(() => seq.AppendInterval(2f));
            seq.Play();
            return;
        }
        seq.Append(transform.DOMove(_defaultPos, _speed));
    }
}

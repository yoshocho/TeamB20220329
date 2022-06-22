using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GimmickButton : MonoBehaviour
{
    [SerializeField, Header("起動させたいギミック")]
    GimmickBase _gimmick = default;
    [SerializeField,Header("ギミックが戻るまでの待ち時間")]
    float _waitTime = 0.0f;
    [SerializeField]
    float _downSpeed = 1.0f;
    [SerializeField]
    float _downPos = -0.3f;

    float _defaultPos = 0f;

    Transform _targetChara;

    private void Start()
    {
        _defaultPos = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "GimmickObj")
        {
            collision.gameObject.transform.SetParent(transform);
            transform.DOMoveY(_downPos, _downSpeed).OnComplete(() => _gimmick.GimmicOn());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "GimmickObj")
        {
            _targetChara = collision.transform;
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(_waitTime);
        _targetChara.SetParent(null);
        transform.DOMoveY(_defaultPos, _downSpeed).OnComplete(() => _gimmick.GimmicEnd());
    }
}

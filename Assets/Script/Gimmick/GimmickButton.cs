using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameUtility.Sound;

public class GimmickButton : MonoBehaviour
{
    [SerializeField, Header("起動させたいギミック")]
    GimmickBase _gimmick = default;
    [SerializeField, Header("ギミックが戻るまでの待ち時間")]
    float _waitTime = 0.0f;
    [SerializeField]
    float _downSpeed = 1.0f;
    [SerializeField]
    CollisionEvent _collisionEvent;
    [SerializeField]
    Transform _downPos;
    float _defaultPos = 0f;
    [SerializeField]
    SEData[] _sEDatas;
    SizeCtrl _player;

    private void Start()
    {
        _defaultPos = transform.position.y;
        _collisionEvent
            .SetCollisionEnterEv(CollisionEnterEvent)
            .SetCollisionExitEv(CollisionExitEvent);
    }

    public void CollisionEnterEvent(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "GimmickObj")
        {
            _player = collision.gameObject.GetComponent<SizeCtrl>();
            if (!_player.CanPush) return;

            collision.gameObject.transform.SetParent(transform);
            transform.DOMoveY(_downPos.position.y, _downSpeed).OnComplete(() => _gimmick.GimmicOn());
            SoundManager.PlaySE(_sEDatas[0]);
        }
    }

    public void CollisionExitEvent(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "GimmickObj")
        {
            collision.gameObject.transform.SetParent(null);
            StartCoroutine(Timer());
            SoundManager.PlaySE(_sEDatas[1]);
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(_waitTime);
        transform.DOMoveY(_defaultPos, _downSpeed).OnComplete(() => _gimmick.GimmicEnd());
    }
}

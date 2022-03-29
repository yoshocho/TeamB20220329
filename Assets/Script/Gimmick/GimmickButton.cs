using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GimmickButton : MonoBehaviour
{
    [SerializeField, Header("起動させたいギミック")]
    GimmickBase _gimmick = default;

    [SerializeField]
    float _downSpeed = 1.0f;
    [SerializeField]
    float _downPos = -0.3f;

    float _defaultPos = 0f;

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
            collision.transform.SetParent(null);
            transform.DOMoveY(_defaultPos, _downSpeed).OnComplete(() => _gimmick.GimmicEnd());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _gimmick.Gimmic(true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _gimmick.Gimmic(false);
        }
    }
}

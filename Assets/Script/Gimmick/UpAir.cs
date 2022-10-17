using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpAir : GimmickBase
{
    [SerializeField]
    float _upSpeed = 2.0f;
    [SerializeField]
    Transform _targetPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IRBCtrl rb;
        if (collision.TryGetComponent(out rb))
        {
            rb.RB.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IRBCtrl rb;
        if (collision.TryGetComponent(out rb))
        {
            Debug.Log("aru");
            rb.RB.AddForce(Vector2.up * _upSpeed, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IRBCtrl rb;
        if (collision.TryGetComponent(out rb))
        {
            Debug.Log("aru");
            rb.RB.velocity = new Vector2(rb.RB.velocity.x, 0.0f);
            rb.RB.AddForce(Vector2.up * 0.3f, ForceMode2D.Impulse);
        }
    }
}

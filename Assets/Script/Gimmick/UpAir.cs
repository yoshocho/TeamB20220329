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

    private void OnTriggerStay2D(Collider2D collision)
    {
        IRBCtrl rb;
        Debug.Log("UpAir");
        if (collision.TryGetComponent(out rb))
        {
            Debug.Log("aru");
            rb.RB.AddForce(Vector2.up * _upSpeed, ForceMode2D.Impulse);
            //rb.RB.DOMoveY(_targetPos.position.y,1f,true);
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
